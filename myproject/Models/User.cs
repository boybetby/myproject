using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myproject.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Please enter the correct password")]
        public string Confirm { get; set; }
        [Required]
        public string FullName { get; set; }
        public virtual ICollection<Post> PostsLike { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string Address { get; set; }

        public User()
        {

        }

        public User(int id, string username, string password, string confirm, string fullName, ICollection<Post> postsLike, string email, string phoneNumber, string province, string district, string ward, string address)
        {
            Id = id;
            Username = username;
            Password = password;
            Confirm = confirm;
            FullName = fullName;
            PostsLike = postsLike;
            Email = email;
            PhoneNumber = phoneNumber;
            Province = province;
            District = district;
            Ward = ward;
            Address = address;
        }
    }
}