using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook_Bot_.Model
{
    public class Posts
    {
        public Posts()
        {
            PhotosFullName = new List<string>();
        }
        public string FolderPath { get; set; }
        public bool Random { get; set; }
        public List<string> PhotosFullName { get; set; }
        public int Interval { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }
    }
}
