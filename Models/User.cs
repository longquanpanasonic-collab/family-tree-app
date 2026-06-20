using System;
using System.Collections.Generic;

namespace FamilyTreeApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        
        // Role: Admin, User
        public string Role { get; set; }
        
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        // Quan hệ
        public virtual ICollection<FamilyMember> FamilyMembers { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
