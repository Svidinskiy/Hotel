//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hotel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Housemaid
    {
        public Housemaid()
        {
            this.Rooms = new HashSet<Rooms>();
        }
    
        public int ID_housemaid { get; set; }
        public string FIO { get; set; }
        public string Telephone { get; set; }
    
        public virtual ICollection<Rooms> Rooms { get; set; }
    }
}
