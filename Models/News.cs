using System;
using System.ComponentModel.DataAnnotations;

namespace FamilyTreeApp.Models
{
    public class News
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [StringLength(200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        public string Content { get; set; }

        public string FeaturedImage { get; set; }

        [Required]
        public string Category { get; set; } // Tin tức, Sự kiện, Chia sẻ

        [Required]
        public bool IsFeatured { get; set; } // Tin nổi bật

        public string Author { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedDate { get; set; }

        public int ViewCount { get; set; }

        public News()
        {
            CreatedDate = DateTime.Now;
            ViewCount = 0;
            IsFeatured = false;
        }
    }
}
