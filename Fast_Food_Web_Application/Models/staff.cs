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
    
    public partial class staff
    {
        public staff()
        {
            this.admin_staff = new HashSet<admin_staff>();
        }
    
        public int Staff_id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Staff_Type { get; set; }
        public string Gender { get; set; }
        public string staff_Image { get; set; }
        public System.DateTime date_time { get; set; }
    
        public virtual ICollection<admin_staff> admin_staff { get; set; }
    }
}
