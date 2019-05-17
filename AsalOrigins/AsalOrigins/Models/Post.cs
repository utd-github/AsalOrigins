using System;
using SQLite;


namespace AsalOrigins.Models
{
    class Post
    {
        [PrimaryKey]
        public String ID { get; set; }
        public String Title { get; set; }
        public String Author { get; set; }
        public String Intro { get; set; }
        public String Date { get; set; }
        public String Body { get; set; }
    }
}
