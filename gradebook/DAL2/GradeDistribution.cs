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
    
    public partial class GradeDistribution
    {
        public GradeDistribution()
        {
            this.Assignments = new HashSet<Assignment>();
        }
    
        public int GradeID { get; set; }
        public string Category { get; set; }
        public decimal Weight { get; set; }
        public int CourseID { get; set; }
    
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual Course Course { get; set; }
    }
}
