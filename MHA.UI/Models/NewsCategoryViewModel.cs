using MHA.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MHA.UI.Models
{
    public class NewsCategoryViewModel
    {
        public virtual List<News> News { get; set; }
        public virtual List<News> NewsDistinct { get; set; }
        public virtual List<Category> Category { get; set; }

    }
}