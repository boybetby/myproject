using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myproject.Models
{
    public class Guide
    {
        public int Id { get; set; }
        public string GuideTitle { get; set; }
        public string GuideDescription { get; set; }
        public string Guide_Light { get; set; }
        public string Guide_Water { get; set; }
        public string Guide_Petfriendly { get; set; }
        public string Guide_Level { get; set; }
        public string Guide_Tip { get; set; }
        public string clade_hashtag { get; set; }
        public string family_hasttag { get; set; }
        public string difficulty_hashtag { get; set; }
    }
}