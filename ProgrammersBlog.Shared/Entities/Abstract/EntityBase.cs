using System;

namespace ProgrammersBlog.Shared.Entities.Abstract
{
    public abstract class EntityBase
    {
        public virtual int Id { get; set; }
        public virtual bool IsDeleted { get; set; } = false; // Entity silinmiş mi?  Default olarak hayır.
        public virtual bool IsActive { get; set; } = true; // Durum.Aktif.Value => Entity aktif mi? Default olarak evet.
        public virtual DateTime CreatedDate { get; set; } = DateTime.Now;  // Override edilebilir şekilde oluşturuyoruz (virtual).
        public virtual DateTime ModifiedDate { get; set; } = DateTime.Now; // Override edilebilir şekilde oluşturuyoruz (virtual).
        public virtual string CreatedByName { get; set; } = "Admin";
        public virtual string ModifiedByName { get; set; } = "Admin";
        public virtual string Note { get; set; }
    }


}
