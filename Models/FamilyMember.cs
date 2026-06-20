using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FamilyTreeApp.Models
{
    public class FamilyMember
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public string Gender { get; set; } // Nam, Nữ

        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Occupation { get; set; }

        public string ProfileImage { get; set; }

        // Quan hệ gia đình
        public int? FatherId { get; set; }
        public int? MotherId { get; set; }
        public int? SpouseId { get; set; }

        // Navigation Properties
        public virtual FamilyMember Father { get; set; }
        public virtual FamilyMember Mother { get; set; }
        public virtual FamilyMember Spouse { get; set; }
        public virtual ICollection<FamilyMember> Children { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedDate { get; set; }

        public FamilyMember()
        {
            Children = new List<FamilyMember>();
            CreatedDate = DateTime.Now;
        }
    }
}
