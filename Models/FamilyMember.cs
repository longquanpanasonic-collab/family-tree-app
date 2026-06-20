using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FamilyTreeApp.Models
{
    public class FamilyMember
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Họ tên không được trống")]
        [StringLength(100)]
        public string FullName { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [Required(ErrorMessage = "Giới tính không được trống")]
        public string Gender { get; set; } // Male, Female
        
        [Phone]
        public string PhoneNumber { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        
        [StringLength(200)]
        public string Address { get; set; }
        
        [StringLength(100)]
        public string Occupation { get; set; }
        
        public string ProfileImage { get; set; }
        public string Bio { get; set; }
        
        // Quan hệ gia đình
        public int? FatherId { get; set; }
        public int? MotherId { get; set; }
        public int? SpouseId { get; set; }
        
        // Foreign Key cho User
        public int UserId { get; set; }
        
        // Navigation Properties
        public virtual FamilyMember Father { get; set; }
        public virtual FamilyMember Mother { get; set; }
        public virtual FamilyMember Spouse { get; set; }
        
        public virtual ICollection<FamilyMember> Children { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        [NotMapped]
        public int Age
        {
            get
            {
                return DateTime.Now.Year - DateOfBirth.Year;
            }
        }
    }
}