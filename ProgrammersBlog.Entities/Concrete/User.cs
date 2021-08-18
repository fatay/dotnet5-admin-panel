﻿using Microsoft.AspNetCore.Identity;
using ProgrammersBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Concrete
{
    // Entity Framework Identity Core ve Identity Store NuGet paketleri yüklendi.
    public class User:IdentityUser<int>
    {
        public string Picture { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
