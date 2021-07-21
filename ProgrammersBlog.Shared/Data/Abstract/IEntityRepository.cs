using ProgrammersBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Data.Abstract
{
    // T => class olmalı, instantiate(new) edilebilmeli ve IEntity türünden olmalıdır.
    public interface IEntityRepository<T> where T:class,IEntity,new()    
    {
        /*  GENERIC REPOSITORY DYNAMIC ARCHITECTURE 
        --------------------------------------------------------------------------------------
            Generic Repository Pattern, genel mimari itibariyle tüm classlarda bulunacak
            ana metodların, türden bağımsız olarak şablona oturtulmasını sağlamayı amaçlar.

            => Buradaki lamba expressionlara filtre veya yaygın adıyla predicate deniyor.
            => "params" ifadesi JavaScript 'teki spread operatörüne karşılık gelmektedir. Bu
                sayede örn. bir kullanıcıya ait birden fazla türde veri (Article,Comment....)
                anda çekilebilir oluyor.
            => Asenkron mimariyi kullanıyoruz çünkü bir metod diğerini beklemeli. Bu nedenle
               metodların sonuna "Async" postfix ifadesi eklenmelidir.    -- Best Practices --
        --------------------------------------------------------------------------------------
         */

        // Bir filtre ile veriyi getirip verinin birden fazla propertysini almak için GetAsync() metodunu kullanacağız.
        Task<T> GetAsync(Expression<Func<T,bool>> predicate,params Expression<Func<T,object>>[] includeProperties);

        // Tüm verileri bir liste şeklinde getirmek için GetAllAsync() metodunu kullanacağız.
        // Parametre boş olabileceğinden yani filtresiz hepsini çekmek isteyebileceğimizden predicate=null olmalı.
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate=null, params Expression<Func<T, object>>[] includeProperties);

        // Genel DB Metodları
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        // Any-IsExist?
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        // Counting
        Task<int> CountAsync(Expression<Func<T,bool>> predicate);
    }
}


/*
 *  ÖRNEK KULLANIM: GetSync()
 *  -------------------------
 *  ID 'si 10 olan makaleyi getirmek istiyorsak: 
 *  repository.Get(k=>k.Id==1); 
 *  
 *  Adı "Fatih" olan kullanıcıyı getirmek istiyorsak:
 *  repository.Get(k=>k.FirstName=="Fatih");
 */

/*
 *  ÖRNEK KULLANIM: GetAllAsync()
 *  -----------------------------
 *  ID 'si 1 olan makaleye ait tüm yorumları getirmek istiyorsak:
 *  repository.GetAllAsync(y=>y.ArticleId==1);
 * 
 *  C# Kategorisine ait tüm makaleleri getirmek istiyorsak:
 *  repository.GetAllAsync(m=>m.Category.Name=="C#", m=>m.Category);
 */

/*
 * DİĞER GENERIC KULLANIMLAR: Add(), Update(), Delete()
 * ----------------------------------------------------
 * Kategori eklemek için:
 * _categoryRepository.AddAsync(Category category);
 * 
 * Kullanıcı güncellemek için:
 * _userRepository.UpdateAsync(User user);
 * 
 * Yorum silmek için:
 * _commentRepository.DeleteAsync(Comment comment);
 */

/* ANY: Böyle bir varlık var mı?: ÖRNEK KULLANIMLAR
 * ------------------------------------------------
 * Alper isimli bir kullanıcı var mı?:
 * _userRepository.AnyAsync(u=>u.FirstName=="Alper");
 * true veya false dönecektir.
 */

/* COUNT: Kaç adet entity mevcut?
 * ---------------------------------------------
 * Tüm makalelerin sayısı:
 * _articleRepository.CountAsync();
 * 
 * Id 'si 20 olan makalenin yorumlarının sayısı:
 * commentRepository.CountAsync(c=>c.ArticleId==20);
 */