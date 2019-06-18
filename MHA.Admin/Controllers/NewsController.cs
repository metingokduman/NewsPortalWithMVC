using MHA.Admin.CustomFilter;
using MHA.Core.Repository.Contract;
using MHA.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MHA.Admin.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IImageRepository _imageRepository;
        
       
        public NewsController(INewsRepository newsRepository,ICategoryRepository categoryRepository,IAppUserRepository appUserRepository, IImageRepository imageRepository)
        {
            _newsRepository = newsRepository;
            _categoryRepository = categoryRepository;
            _appUserRepository = appUserRepository;
            _imageRepository = imageRepository;
        }
        // GET: News
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [LoginFilter]
        public ActionResult Add()
        {
            var categoryList = _categoryRepository.GetMany(x => x.ParentId == 0);
            ViewBag.Category = categoryList;
            return View();
        }
        [HttpPost]
        [LoginFilter]
        public ActionResult Add(News news,HttpPostedFileBase ImageVit,IEnumerable<HttpPostedFileBase> ImageList)
        {
            var sessionControl = HttpContext.Session["AppUserEmail"];
            if (news==null)
            {
                throw new NullReferenceException();
            }
            else
            {
                if (ImageVit!=null)
                {
                    string fileName = Guid.NewGuid().ToString().Replace("-", "");
                    string extensionn = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string fullPath = "/Content/External/NewsPic/" + fileName + extensionn;
                    Request.Files[0].SaveAs(Server.MapPath(fullPath));
                    news.Image = fullPath;
                }
                AppUser appUser = _appUserRepository.GetByEmail(sessionControl.ToString());
                news.UserId = appUser.Id;


                _newsRepository.Insert(news);
                _newsRepository.Save();

                string ListOfPic= System.IO.Path.GetExtension(Request.Files[1].FileName);
                if (ImageList!=null)
                {
                    foreach (var pic in ImageList)
                    {
                        string fileName = Guid.NewGuid().ToString().Replace("-", "");
                        string extensionn = System.IO.Path.GetExtension(Request.Files[0].FileName);
                        string fullPath = "/Content/External/NewsPic/" + fileName + extensionn;
                        pic.SaveAs(Server.MapPath(fullPath));

                        Image image = new Image
                        {
                            NewsImage = fullPath,
                            CreatedTime = DateTime.Now,
                            IsActived = true,
                            OnModifiedTime = DateTime.Now
                            
                        };
                        image.NewsId = news.Id;
                        _imageRepository.Insert(image);
                        _imageRepository.Save();


                    }
                }
               

                
            }
            return View();
        }


        public void SetCatergoryList(object category=null)
        {
            var categoryList = _categoryRepository.GetMany(x => x.ParentId == 0);
            ViewBag.Category = categoryList;
        }

        //public int GetNewsLastId()
        //{
        //    var news=_newsRepository.Get(x=>x.)
        //}
    }
}