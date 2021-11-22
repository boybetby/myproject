using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myproject.Models
{
    public enum Category
    {
        product, furniture, workshop
    }


    public enum Clade
    {
        Tracheophytes, Angiosperms, Eudicots, Rosids, Monocots, Commelinids
    }

    public enum Family
    {
        Strelitziaceae, Moraceae, Malvaceae, Asparagaceae
    }

    public enum Difficulty
    {
        easy, standard, expert
    }

    public class Product
    {
        public int productID { get; set; }
        [Required]
        public string productname { get; set; }
        public string description { get; set; }
        [Required]
        public long price { get; set; }
        [Required]
        public Category? category { get; set; }
        [Required(ErrorMessage = "Please choose a image to upload.")]
        [DataType(DataType.Upload)]
        public string image { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public string clade_hashtag { get; set; }       
        public string family_hasttag { get; set; }
        public string difficulty_hashtag { get; set; }
    }

}