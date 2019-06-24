using MHA.Core.Repository.Contract;
using MHA.Data.Domain;
using MHA.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MHA.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsRepository _newsRepository;
        private readonly ICategoryRepository _categoryRepository;
        public HomeController(INewsRepository newsRepository, ICategoryRepository categoryRepository)
        {
            _newsRepository = newsRepository;
            _categoryRepository = categoryRepository;
        }
        List<News> news;
        List<News> newsDistinct;
        List<Category> category;
        NewsCategoryViewModel newsCategoryViewModel;
        // GET: Home
        [HttpGet]
        public ActionResult Index(int? Id)
        {
            if (Id==null)
            {
                Category _category = _categoryRepository.GetFirst();
                news = _newsRepository.GetMany(x => x.CategoryId == _category.Id).ToList();
            }
            else
            {
                news = _newsRepository.GetMany(x => x.CategoryId == Id).ToList();
            }
            newsDistinct = _newsRepository.GetManyDistinct().ToList();
            category = _categoryRepository.GetAll().ToList();

            newsCategoryViewModel = new NewsCategoryViewModel
            {
                NewsDistinct=newsDistinct,
                News = news,
                Category = category
            };

            return View(newsCategoryViewModel);
        }

        [HttpGet]
        public ActionResult NewsFilter(int Id)
        {
         

            return RedirectToAction("Index","Home",new {Id });
        }
    }
}