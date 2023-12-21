﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boardgames.Data.Models
{
    public class Seller
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(10)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(2)]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Website { get; set; }

        public virtual ICollection<BoardgameSeller> BoardgamesSellers { get; set; }=new HashSet<BoardgameSeller>();
    }
}
