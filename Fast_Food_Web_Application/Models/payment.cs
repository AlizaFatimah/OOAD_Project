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
    
    public partial class payment
    {
        public payment()
        {
            this.Customer_payment = new HashSet<Customer_payment>();
        }
    
        public int payment_id { get; set; }
        public string NameOnCard { get; set; }
        public long CraditCardNumber { get; set; }
        public string EXPMonth { get; set; }
        public int CVV { get; set; }
        public long EXPYear { get; set; }
        public System.DateTime Date_Time { get; set; }
    
        public virtual ICollection<Customer_payment> Customer_payment { get; set; }
    }
}