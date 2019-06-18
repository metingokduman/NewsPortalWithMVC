using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Data.Domain
{
    [Table("Role")]
    public class Role:BaseEntity
    {
       

        [Display(Name ="Role Name :")]
        public string RoleName { get; set; }
    }
}
