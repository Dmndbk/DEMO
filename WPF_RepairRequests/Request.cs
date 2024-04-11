//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPF_RepairRequests
{
    using System;
    using System.Collections.Generic;
    
    public partial class Request
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Nullable<int> Client_Id { get; set; }
        public Nullable<int> Status_Id { get; set; }
        public Nullable<int> Employee_Id { get; set; }
        public Nullable<int> Equipment_Id { get; set; }
        public Nullable<int> Defect_Id { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> Proirity_Id { get; set; }
        public Nullable<System.DateTime> CloseDate { get; set; }
        public string Cost { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual Status Status { get; set; }
        public virtual TypeOfDefect TypeOfDefect { get; set; }
    }
}
