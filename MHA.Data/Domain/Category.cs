using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Data.Domain
{
    [Table("Catagory")]
    public class Category:BaseEntity
    {
      
        [Required]
        public string CategoryName { get; set; }

        public string URL{ get; set; }

        public int ParentId { get; set; }


        public virtual ICollection<News> News { get; set; }
    }
}
