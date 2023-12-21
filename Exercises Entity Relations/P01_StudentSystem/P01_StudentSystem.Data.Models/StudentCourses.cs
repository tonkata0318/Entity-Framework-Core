using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data.Models
{
    
    public class StudentCourses
    {

        public int CourseId { get; set; }
        
        public virtual Course Courses { get; set; }

        public int StudentId { get; set; }
        
        public virtual Student Students { get; set; }
    }
}
