using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Data.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        private DateTime DatePrivate = DateTime.Now;
        private bool Active = true;
        public DateTime CreatedTime {
            get { return DatePrivate; }
            set { DatePrivate = value; }
        } 
        public DateTime OnModifiedTime {
            get { return DatePrivate; }
            set { DatePrivate = value; }
        }
        public bool IsActived {
            get { return Active; }
            set { Active = value; }
        }
    }
}
