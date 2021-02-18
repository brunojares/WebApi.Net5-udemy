﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace S6A0702.Moldel.Entities
{
    [Table("books")]
    public class Book : EntityBase
    {
        [Column("author")]
        public string Author { get; set; }
        [Column("launch_date")]
        public DateTime? LaunchDate { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("title")]
        public string Title { get; set; }
    }
}
