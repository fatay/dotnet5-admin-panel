using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ProgrammersBlog.Entities.Concrete
{
    // Entity Framework Identity Core ve Identity Store NuGet paketleri yüklendi.
    public class User : IdentityUser<int>
    {
        public string Picture { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
