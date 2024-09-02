﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Shared.DTO.Banner
{
    public class CreateBannerDto
    {
        [Required]
        public string Title { get; set; }
        public string Desc { get; set; }
        [Required]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        [Required]
        public BannerPosition Position { get; set; }

    }
    public class BannerDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string filePath { get; set; }
        public BannerPosition Position { get; set; }
    }
    public enum BannerPosition
    {
        Top,
        Right,
        Left
    }
}