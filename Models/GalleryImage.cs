using System;
using System.ComponentModel.DataAnnotations;

namespace FamilyTreeApp.Models
{
    public class GalleryImage
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ảnh")]
        public string ImagePath { get; set; }

        public string Description { get; set; }

        public int? FamilyMemberId { get; set; }

        public string Category { get; set; } // Gia đình, Sự kiện, Kỷ niệm

        [DataType(DataType.DateTime)]
        public DateTime UploadedDate { get; set; }

        public virtual FamilyMember FamilyMember { get; set; }

        public GalleryImage()
        {
            UploadedDate = DateTime.Now;
        }
    }
}
