using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Data.Domain
{
    public class Slider:BaseEntity
    {
        public string SliderHeader { get; set; }
        public string SliderURL { get; set; }
        public string SliderContent { get; set; }
        public string SliderImage { get; set; }
    }
}
