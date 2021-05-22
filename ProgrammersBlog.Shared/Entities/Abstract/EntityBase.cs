using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Entities.Abstract
{
    public abstract class EntityBase
    {
        public virtual int Id { get; set; }
        public virtual bool IsDeleted { get; set; } = false; // Henüz silinmemiş bir entity
        public virtual bool isActive { get; set; } = true; // Durum.Aktif.Value => entity aktif
        public virtual DateTime CreatedDate { get; set; } = DateTime.Now;  //override edilebilir şekilde oluşturuyoruz (virtual).
        public virtual DateTime ModifiedDate { get; set; } = DateTime.Now; //override edilebilir şekilde oluşturuyoruz (virtual).
        public virtual string CreatedByName { get; set; }
        public virtual string ModifiedByName { get; set; }
    }


}
