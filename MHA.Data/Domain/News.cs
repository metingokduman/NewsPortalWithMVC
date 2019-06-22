using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Data.Domain
{

    public class News:BaseEntity
    {

        public string NewsHeader { get; set; }

        public string NewsSmallContent { get; set; }

        public string NewsContent { get; set; }

        public int ReadedCount { get; set; }

        public int UserId { get; set; }

        public virtual AppUser AppUser { get; set; }

        public string Image { get; set; }

        public virtual ICollection<Image>  NewsImage{ get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        
        public virtual ICollection<Tag> Tag { get; set; }
    }
}
