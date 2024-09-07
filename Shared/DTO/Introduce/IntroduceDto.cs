﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTO.Introduce
{
    public class IntroduceDto
    {
        public Guid id { get; set; }
        public string Titlte { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public bool IsHot { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}