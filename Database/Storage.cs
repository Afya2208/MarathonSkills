//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MarathonSkills.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Storage
    {
        public byte ItemId { get; set; }
        public int ItemAmount { get; set; }
    
        public virtual Item Item { get; set; }
    }
}