using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data.Models
{
    public class Homework
    {
        [Key]
        public int HomeWorkId { get; set; }

        [Required]
        [Unicode(false)]
        public string Content { get; set; }

        [Required]
        [EnumDataType(typeof(EnumsforHomework))]
        public string ContentType { get; set; }

        [Required]
        public int SubmissionTime { get; set; }

        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public virtual Course Courses { get; set; }

        public int StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public virtual Student Students { get; set; }

    }
}
