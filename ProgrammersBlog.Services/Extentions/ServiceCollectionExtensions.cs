using Microsoft.Extensions.DependencyInjection;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete;
using ProgrammersBlog.Data.Concrete.EntityFramework.Contexts;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection) {
            serviceCollection.AddDbContext<ProgrammersBlogContext>();
            serviceCollection.AddIdentity<User,Role>(options => {
                // Identity - User Password Options
                options.Password.RequireDigit = false; // Password should contain digit.
                options.Password.RequiredLength = 5;   // Password length.
                options.Password.RequireNonAlphanumeric = false; // Password should contain the Non-AlphaNumeric characters.
                options.Password.RequiredUniqueChars = 0;  // Number of different Non-AlphaNumeric characters.
                options.Password.RequireLowercase = false; // Password should contain the lowerCase characters.
                options.Password.RequireUppercase = false; // Password should contain the upperCase characters.
                // Identity - Username, EMail Options
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"; // Allowed username characters list.
                options.User.RequireUniqueEmail = true; // EMail should be unique.
            }).AddEntityFrameworkStores<ProgrammersBlogContext>();
            serviceCollection.AddScoped<IUnitOfWork,UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();
            return serviceCollection;
        }
    }
}
