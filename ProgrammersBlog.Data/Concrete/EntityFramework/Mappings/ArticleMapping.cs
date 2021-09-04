using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class ArticleMapping : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Article>
    {
        // Builder fonksiyonunda lamda expression kullanıyoruz a => a.property
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            // Id
            builder.HasKey(a => a.Id);                                  // Article bir primary keye sahiptir ve bu "Id" dir.
            builder.Property(a => a.Id).ValueGeneratedOnAdd();            // AutoIncrementation yapılır.
            // Title
            builder.Property(a => a.Title).HasMaxLength(100);           // Maksimum 100 karakterden oluşmalı.
            builder.Property(a => a.Title).IsRequired(true);            // Zorunlu alandır.
            // Content
            builder.Property(a => a.Content).HasColumnType("NVARCHAR(MAX)"); // İçerik alabileceği max değeri almalı.
            builder.Property(a => a.Content).IsRequired(true);          // Zorunlu alandır.
            // Dates
            builder.Property(a => a.Date).IsRequired(true);             // Zorunlu alandır.
            builder.Property(a => a.CreatedDate).IsRequired(true);      // Zorunlu alandır.
            builder.Property(a => a.ModifiedDate).IsRequired(true);     // Zorunlu alandır.
            // Seo Author
            builder.Property(a => a.SeoAuthor).HasMaxLength(50);        // Maksimum 50 karakterden oluşmalı.
            builder.Property(a => a.SeoAuthor).IsRequired(true);        // Zorunlu alandır.
            // Seo Description
            builder.Property(a => a.SeoDescription).HasMaxLength(150);  // Maksimum 150 karakterden oluşmalı.
            builder.Property(a => a.SeoDescription).IsRequired(true);   // Zorunlu alandır.
            // Seo Tags
            builder.Property(a => a.SeoTags).HasMaxLength(60);          // Maksimum 60 karakterden oluşmalı.
            builder.Property(a => a.SeoTags).IsRequired(true);          // Zorunlu alandır.
            // Views Count (Görüntülenme Sayısı)
            builder.Property(a => a.ViewsCount).IsRequired(true);       // Zorunlu alandır.
            // Comment Count (Yorum Sayısı)
            builder.Property(a => a.CommentCount).IsRequired(true);     // Zorunlu alandır.
            // Thumbnail Resim Ayarları
            builder.Property(a => a.Thumbnail).HasMaxLength(250);       // Maksimum 250 karakterden oluşmalı.
            builder.Property(a => a.Thumbnail).IsRequired(true);        // Zorunlu alandır.
            // Created By Name
            builder.Property(a => a.CreatedByName).HasMaxLength(50);    // Maksimum 50 karakterden oluşmalı.
            builder.Property(a => a.CreatedByName).IsRequired(true);    // Zorunlu alandır.
            // Modified By Name
            builder.Property(a => a.ModifiedByName).HasMaxLength(50);   // Maksimum 50 karakterden oluşmalı.
            builder.Property(a => a.ModifiedByName).IsRequired(true);   // Zorunlu alandır.
            // Control Builders
            builder.Property(a => a.IsActive).IsRequired(true);         // Zorunlu alandır.
            builder.Property(a => a.IsDeleted).IsRequired(true);        // Zorunlu alandır.
            // Note
            builder.Property(a => a.Note).IsRequired(true);             // Zorunlu alandır.

            /***************************************** DATABASE RELEATIONSHIPS ****************************************/
            // !!! Foreign key ile dışarıdan Id aldığımızda releationship eklememiz gerekiyor.
            // Article ile Category arasında 1 - n ilişki olmalıdır [Bir kategorinin birden fazla makalesi olabilir].
            builder.HasOne<Category>(a => a.Category).WithMany(c => c.Articles).HasForeignKey(a => a.CategoryId);
            // Article ile User arasında 1 - n ilişki olmalıdır. [Bir kullanıcının birden fazla makalesi olabilir].
            builder.HasOne<User>(a => a.User).WithMany(u => u.Articles).HasForeignKey(a => a.UserId);
            /**********************************************************************************************************/

            // Tabloya dönüştür
            builder.ToTable("Articles");

            // Initialize işlemleri : HasData() metodu veritabanında kayıt yoksa eklenmesini sağlar.
            //builder.HasData(
            //new Article
            //{
            //    Id = 1,
            //    CategoryId = 1,
            //    UserId = 1,
            //    Title = "C# 9.0 ve .NET5 Yenilikleri",
            //    Content = @" Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
            //                 Suspendisse sagittis blandit faucibus. Praesent mollis posuere vulputate. 
            //                 Pellentesque mollis risus varius nisl facilisis venenatis. 
            //                 Sed varius ante lorem, tristique varius mauris mollis eget. 
            //                 Ut dictum velit ut iaculis placerat. Donec dignissim tortor non orci dapibus lobortis. 
            //                 Proin vitae convallis metus, ut fermentum lacus. Etiam condimentum tristique finibus. 
            //                 Fusce convallis, ligula eget cursus imperdiet, turpis ante iaculis turpis, at fermentum purus risus non ante. 
            //                 Nunc sagittis nulla mattis metus interdum tempor.",
            //    Thumbnail = "default.jpg",
            //    SeoDescription = "C# 9.0 ve .NET5 Yenilikleri",
            //    SeoTags = "C#, C# 9.0, .NET5, .NET Core, .NET Framework",
            //    SeoAuthor = "Fatih Aydin",
            //    Date = DateTime.Now,
            //    IsActive = true,
            //    IsDeleted = false,
            //    CreatedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedByName = "InitialModify",
            //    ModifiedDate = DateTime.Now,
            //    Note = "C# 9.0 ve .NET5 Yenilikleri",
            //    ViewsCount = 80,
            //    CommentCount = 1
            //},
            //new Article
            //{
            //    Id = 2,
            //    CategoryId = 2,
            //    UserId = 1,
            //    Title = "C++ 11 ve 19 Yenilikleri",
            //    Content = @" Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
            //                 Suspendisse sagittis blandit faucibus. Praesent mollis posuere vulputate. 
            //                 Pellentesque mollis risus varius nisl facilisis venenatis. 
            //                 Sed varius ante lorem, tristique varius mauris mollis eget. 
            //                 Ut dictum velit ut iaculis placerat. Donec dignissim tortor non orci dapibus lobortis. 
            //                 Proin vitae convallis metus, ut fermentum lacus. Etiam condimentum tristique finibus. 
            //                 Fusce convallis, ligula eget cursus imperdiet, turpis ante iaculis turpis, at fermentum purus risus non ante. 
            //                 Nunc sagittis nulla mattis metus interdum tempor.",
            //    Thumbnail = "default.jpg",
            //    SeoDescription = "C++ 11 ve 19 Yenilikleri",
            //    SeoTags = "C++, Object Oriented Programming, C++ 11, QT",
            //    SeoAuthor = "Fatih Aydin",
            //    Date = DateTime.Now,
            //    IsActive = true,
            //    IsDeleted = false,
            //    CreatedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedByName = "InitialModify",
            //    ModifiedDate = DateTime.Now,
            //    Note = "C++ 11 ve 19 Yenilikleri",
            //    ViewsCount = 100,
            //    CommentCount = 1
            //},
            //new Article
            //{
            //    Id = 3,
            //    CategoryId = 3,
            //    UserId = 1,
            //    Title = "JavaScipt ES6 Nedir?",
            //    Content = @" Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
            //                 Suspendisse sagittis blandit faucibus. Praesent mollis posuere vulputate. 
            //                 Pellentesque mollis risus varius nisl facilisis venenatis. 
            //                 Sed varius ante lorem, tristique varius mauris mollis eget. 
            //                 Ut dictum velit ut iaculis placerat. Donec dignissim tortor non orci dapibus lobortis. 
            //                 Proin vitae convallis metus, ut fermentum lacus. Etiam condimentum tristique finibus. 
            //                 Fusce convallis, ligula eget cursus imperdiet, turpis ante iaculis turpis, at fermentum purus risus non ante. 
            //                 Nunc sagittis nulla mattis metus interdum tempor.",
            //    Thumbnail = "default.jpg",
            //    SeoDescription = "ECMASCRIPT ve JavaScript ES6",
            //    SeoTags = "JavScript, ES6, ECMAScript",
            //    SeoAuthor = "Fatih Aydin",
            //    Date = DateTime.Now,
            //    IsActive = true,
            //    IsDeleted = false,
            //    CreatedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedByName = "InitialModify",
            //    ModifiedDate = DateTime.Now,
            //    Note = "ECMASCRIPT ve JavaScript ES6",
            //    ViewsCount = 2940,
            //    CommentCount = 1
            //}); 
        }
    }
}
