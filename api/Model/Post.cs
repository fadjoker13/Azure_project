﻿using System.ComponentModel.DataAnnotations;

namespace Api.Model
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public TypePost Type { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UserId { get; set; }
    }
}
