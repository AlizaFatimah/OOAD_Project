//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fast_Food_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer_payment
    {
        public int Customer_payment1 { get; set; }
        public Nullable<int> cust_id { get; set; }
        public Nullable<int> payment_id { get; set; }
        public System.DateTime Date_Time { get; set; }
    
        public virtual customer customer { get; set; }
        public virtual payment payment { get; set; }
    }
}