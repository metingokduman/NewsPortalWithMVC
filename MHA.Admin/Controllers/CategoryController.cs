using MHA.Core.Repository.Contrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MHA.Core.Repository.Contract;
using MHA.Data.Domain;
using MHA.Admin.Class;
using PagedList;
using MHA.Admin.CustomFilter;

namespace MHA.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        // GET: Category
        [HttpGet]
        public ActionResult Index(int Page=1)
        {
            var catagoryList = _categoryRepository.GetAll().OrderByDescending(x=>x.Id).ToPagedList(Page, 3);
            return View(catagoryList);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                throw new Exception("category Not Found");
            }
            SetCategoryList();
            return View(category);
        }
        // [HttpPost]//burada bir sıkıntı var post etmiyor
        //public JsonResult Edit(Category returnEditCategory)
        //{
        //        var category = _categoryRepository.GetById(returnEditCategory.Id);
        //        category.IsActived = returnEditCategory.IsActived;
        //        category.CategoryName = returnEditCategory.CategoryName;
        //        category.ParentId = returnEditCategory.ParentId;
        //        category.URL = returnEditCategory.URL;
        //        _categoryRepository.Update(category);
        //        _categoryRepository.Save();
        //        return Json(new ResultJson { Success = true, Message = "Process of Editing is Success" });
           
        //}


        [HttpPost]//burada bir sıkıntı var post etmiyor
        public ActionResult Edit(Category returnEditCategory)
        {
            var category = _categoryRepository.GetById(returnEditCategory.Id);
            category.IsActived = returnEditCategory.IsActived;
            category.CategoryName = returnEditCategory.CategoryName;
            category.ParentId = returnEditCategory.ParentId;
            category.URL = returnEditCategory.URL;
            _categoryRepository.Update(category);
            _categoryRepository.Save();
            return RedirectToAction("Index","Category", new ResultJson { Success = true, Message = "Process of Editing is Success" });

        }


        [HttpGet]
        public ActionResult Add()
        {
            SetCategoryList();
            return View();
        }


        [HttpPost]
        public JsonResult Add(Category category)
        {
            try
            {
                _categoryRepository.Insert(category);
                _categoryRepository.Save();
                return Json(new ResultJson { Success = true, Message = "Process of Adding Category is success!!" });
            }
            catch (Exception ex)
            {
                //You can do Logging here
                return Json(new ResultJson { Success = false, Message = "Error Adding Category" });
            }
    
        }

        //public ActionResult Delete(int id)
        //{
        //    var category = _categoryRepository.GetById(id);
        //    if (category==null)
        //    {
        //        throw new Exception("category Not Found");
        //    }
        //    else
        //    {
        //        _categoryRepository.Delete(id);
        //        _categoryRepository.Save();
        //    }
        //    return RedirectToAction("Index","Category");
        //}
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                return Json(new ResultJson { Success = false, Message = "Error Deleting Category" });
            }
            else
            {
                _categoryRepository.Delete(id);
                _categoryRepository.Save();
            }
            return Json(new ResultJson {Success=true,Message= "Process of Deleting Category is success!!" });
        }
        public void SetCategoryList()
        {
            var catagoryList = _categoryRepository.GetMany(x => x.ParentId == 0).ToList();
            ViewBag.Category = catagoryList;
        }
    }
}