//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace gradebook.DAL2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public Student()
        {
            this.Undertakes = new HashSet<Undertake>();
            this.Courses = new HashSet<Course>();
        }
    
        public int StudentID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    
        public virtual ICollection<Undertake> Undertakes { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
