using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Data.Domain
{
    public class Image:BaseEntity
    {
     
        public string NewsImage { get; set; }

        [ForeignKey("News")]
        public int NewsId { get; set; }

        public virtual News News { get; set; }
    }
}
