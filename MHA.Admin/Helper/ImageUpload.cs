using MHA.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MHA.Admin.Helper
{
    public static class ImageUpload
    {
        public static  string SliderImage (HttpPostedFileBase SliderImage,Slider slider)
        {
            string File = Guid.NewGuid().ToString().Replace("-", "");
            string[] path = SliderImage.ContentType.Split('/');
            string fullPath = "/External/Slider/" + File + "." + path[1];
            SliderImage.SaveAs(HttpContext.Current.Server.MapPath(fullPath));
            slider.SliderImage = fullPath;
            return slider.SliderImage;
        }
    }
}