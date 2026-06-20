using System;
using System.ComponentModel.DataAnnotations;

namespace FamilyTreeApp.Models
{
    public class News
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Tiêu đề không được trống")]
        [StringLength(200)]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Nội dung không được trống")]
        public string Content { get; set; }
        
        [StringLength(500)]
        public string Summary { get; set; }
        
        public string FeaturedImage { get; set; }
        
        [Required]
        public string Category { get; set; } // Sự kiện, Thông báo, Khác
        
        public bool IsFeatured { get; set; }
        public int Views { get; set; }
        
        // Foreign Key
        public int UserId { get; set; }
        
        // Navigation
        public virtual User User { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}