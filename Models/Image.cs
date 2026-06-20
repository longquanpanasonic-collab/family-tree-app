using System;
using System.ComponentModel.DataAnnotations;

namespace FamilyTreeApp.Models
{
    public class Image
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(500)]
        public string ImagePath { get; set; }
        
        [StringLength(200)]
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public int? FamilyMemberId { get; set; }
        
        public virtual FamilyMember FamilyMember { get; set; }
        
        public DateTime UploadedDate { get; set; }
    }
}