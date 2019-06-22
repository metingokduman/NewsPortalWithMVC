using MHA.Admin.Class;
using MHA.Admin.CustomFilter;
using MHA.Core.Repository.Contract;
using MHA.Data.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using MHA.Admin.Helper;

namespace MHA.Admin.Controllers
{
    public class SliderController : Controller
    {
        private readonly ISliderRepository _sliderRepository;
        public SliderController(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }
        // GET: Slider
        public ActionResult Index(int Sayfa=1)
        {
            var SliderList=_sliderRepository.GetAll().OrderByDescending(x=>x.Id).ToPagedList(Sayfa,10);
            return View(SliderList);
        }
        [HttpGet]
        [LoginFilter]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [LoginFilter]
        public JsonResult Add(Slider slider,HttpPostedFileBase SliderImage)
        {
            if (slider!=null)
            {
                if (SliderImage.ContentLength>0)
                {
                    string file = Guid.NewGuid().ToString().Replace("-", "");
                    string path = Path.GetExtension(Request.Files[0].FileName);
                    string imagePath = "/External/Slider/" + file + path;
                    SliderImage.SaveAs(Server.MapPath(imagePath));
                    slider.SliderImage = imagePath;
                    
                    _sliderRepository.Insert(slider);
                    try
                    {
                        _sliderRepository.Save();
                    }
                    catch (Exception)
                    {

                        return Json(new ResultJson { Success = true, Message = "Process of adding Slider is  success!!" });
                    }
                }
                
            }
            return Json(new ResultJson { Success = false, Message = "Process of adding Slider is not success!!" });

        }
        [HttpGet]
        [LoginFilter]
        public ActionResult Edit(int id)
        {
            var SliderExist = _sliderRepository.GetById(id);
            if (SliderExist!=null)
            {
                return View(SliderExist);
            }
            return RedirectToAction("Index", "Slider");
        }
        [HttpPost]
        [LoginFilter]
        public ActionResult Edit(Slider slider,HttpPostedFileBase SliderImage)
        {
            if (slider!=null)
            {
                Slider slider_ = _sliderRepository.GetById(slider.Id);
                slider_.SliderHeader = slider.SliderHeader;
                slider_.SliderContent = slider.SliderContent;
                slider_.IsActived = slider.IsActived;
                slider_.SliderURL = slider.SliderURL;
                if (SliderImage!=null && SliderImage.ContentLength>0)
                {
                    if (slider_.SliderImage!=null)
                    {
                        string url = slider_.SliderImage;
                        string imagePath = Server.MapPath(url);
                        FileInfo files = new FileInfo(imagePath);
                        if (files.Exists)
                        {
                            files.Delete();
                        }
                    }
                    ImageUpload.SliderImage(SliderImage, slider);
                    slider_.SliderImage = slider.SliderImage;
                }
                try
                {
                    _sliderRepository.Update(slider_);
                    _sliderRepository.Save();
                    return Json(new ResultJson { Success = true, Message = "Process of Updating Slider is success!!" });
                }
                catch (Exception)
                {

                    return Json(new ResultJson { Success = false, Message = "Process of Updating Slider is not success!!" });
                }
            }
            return RedirectToAction("Index", "Slider");
        }

        public JsonResult Delete(Slider slider)
        {
            Slider slider_ = _sliderRepository.GetById(slider.Id);
            if (slider_==null)
            {
                return Json(new ResultJson { Success = false, Message = "Process of Deleting Slider is not success!!" });
                
            }
            else
            {
                string imageSlider = slider_.SliderImage;
                string imagePath = Server.MapPath(imageSlider);
                FileInfo fileInfo = new FileInfo(imagePath);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                    _sliderRepository.Delete(slider_.Id);
                    _sliderRepository.Save();
                    
                }
                return Json(new ResultJson { Success = true, Message = "Process of Deleting Slider is  success!!" });
            }
        }
    }
}