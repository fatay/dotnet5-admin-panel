using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Data.Abstract;

namespace ProgrammersBlog.Data.Abstract
{
    /* 
       IEntity generic repositorysini(IEntityRepository) implements ederek 
       "Generic Repository Pattern" uyguladık. Bu sayede;

       => Tüm obje türlerini destekleyen dinamik bir yapı kurmuş olduk <T>. 
       => Add(), Update(), Delete(), Get(), GetAll(), Any(), Count() gibi tüm
          Data Access Layerlarda kullanılacak olan metodları ortak bir imzada
          ve şablonda toplayarak yeniden tanımlama zahmetinden kurtulmuş olduk.
    */
    public interface IArticleRepository : IEntityRepository<Article>
    {

    }
}
