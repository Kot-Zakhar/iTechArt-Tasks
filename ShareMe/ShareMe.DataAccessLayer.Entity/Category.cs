﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShareMe.DataAccessLayer.Entity
{
    public class Category : Entity
    {
        [ForeignKey("ParentId")]
        public Category Parent { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }
    }
}
