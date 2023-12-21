using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [MaxLength(80)]
        [Unicode(true)]
        [Required]
        public string Name { get; set; }

        [Unicode(true)]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public double Price { get; set; }


        public ICollection<Homework> Homeworks { get; set; }

        public ICollection<Resource> Resources { get; set; }

        public ICollection<StudentCourses> StudentsCourses { get; set; }
    }
}
