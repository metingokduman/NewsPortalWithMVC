using MHA.Admin.CustomFilter;
using MHA.Core.Repository.Contract;
using MHA.Data.Domain;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
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
        [LoginFilter]
        public ActionResult Index(int page=1)
        {
            var NewsCategory = _newsRepository.GetAll();
            return View(NewsCategory.OrderByDescending(x=>x.Id).ToPagedList(page,20));
        }
        [HttpGet]
        [LoginFilter]
        public ActionResult Add()
        {
           
            var categoryList2 = _categoryRepository.GetMany(x => x.ParentId == 0).ToList();
            ViewBag.Category2 = categoryList2;
        
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
            TempData["Information"] = "Process of Adding News is Success";
            return RedirectToAction("Index","News");
        }

        
        [LoginFilter]
        public ActionResult Delete(int id)
        {
            News _dbnews = _newsRepository.GetById(id);
            List<Image> _dbimageList = _imageRepository.GetMany(x=>x.NewsId== _dbnews.Id).ToList();
            if (_dbnews == null)
            {
                throw new Exception("haber bulunamadı");
            }
            else
            {
                _newsRepository.Delete(id);
                _newsRepository.Save();
                string file_name = _dbnews.Image;
                string path = Server.MapPath(file_name);
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    file.Delete();
                }
                if (_dbimageList==null)
                {
                    throw new Exception("resim bulunamadı");
                }
                else
                {
                    foreach (var item in _dbimageList)
                    {
                        _imageRepository.Delete(item.Id);
                        _newsRepository.Save();
                        string file_namelist = item.NewsImage;
                        string path2 = Server.MapPath(file_namelist);
                        FileInfo file2 = new FileInfo(path2);
                        if (file2.Exists)
                        {
                            file2.Delete();
                        }
                    }
                }
               
               
                

            }
            TempData["informaiton"] = "haber başarı ile silindi";
            return RedirectToAction("Index","News");
        }

        [LoginFilter]
        public ActionResult Check(int id)
        {
            News returnedNews = _newsRepository.GetById(id);
            if (returnedNews==null)
            {
                throw new Exception("haber bulunamadı");
            }
            else
            {
                if (returnedNews.IsActived==true)
                {
                    returnedNews.IsActived = false;
                    _newsRepository.Update(returnedNews);
                    _newsRepository.Save();
                    TempData["informaiton"] = "işleminiz başarı ile gerçekleşti";
                    return RedirectToAction("Index", "News");
                }
                else
                {
                    returnedNews.IsActived = true;
                    _newsRepository.Update(returnedNews);
                    _newsRepository.Save();
                    TempData["informaiton"] = "işleminiz başarı ile gerçekleşti";
                    return RedirectToAction("Index", "News");
                }
            }
           
        }

        [LoginFilter]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            News news = _newsRepository.GetById(id);
            if (news==null)
            {
                throw new Exception("haber bulunamadı");
            }
            else
            {
                SetCatergoryList();
                return View(news);
            }
            
        }
        [LoginFilter]
        [HttpPost]
        public ActionResult Edit(News news, HttpPostedFileBase ImageVit, IEnumerable<HttpPostedFileBase> ImageList)
        {
            News _dbnews = _newsRepository.GetById(news.Id);
            List<Image> _dbimageList = _imageRepository.GetMany(x => x.NewsId == _dbnews.Id).ToList();
            if (_dbnews==null)
            {
                throw new Exception("haber bulunamadı");

            }
            else
            {
                _dbnews.Category = news.Category;
                _dbnews.CreatedTime = news.CreatedTime;
                _dbnews.OnModifiedTime = DateTime.Now;
                _dbnews.NewsContent = news.NewsContent;
                _dbnews.NewsSmallContent = news.NewsSmallContent;
                _dbnews.NewsHeader = news.NewsHeader;
                _dbnews.ReadedCount = news.ReadedCount;
                _dbnews.UserId = news.UserId;
                _dbnews.CategoryId = news.CategoryId;
                _dbnews.IsActived = news.IsActived;
                if (ImageVit!=null)
                {
                    string file_name = _dbnews.Image;
                    string path = Server.MapPath(file_name);
                    FileInfo file = new FileInfo(path);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    string fileName = Guid.NewGuid().ToString().Replace("-", "");
                    string extensionn = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string fullPath = "/Content/External/NewsPic/" + fileName + extensionn;
                    Request.Files[0].SaveAs(Server.MapPath(fullPath));
                    _dbnews.Image = fullPath;
                    _newsRepository.Update(_dbnews);
                    _newsRepository.Save();

                }
                if (_dbimageList != null)
                {
                    foreach (var item in _dbimageList)
                    {
                        string file_name2 = item.NewsImage;
                        string path2 = Server.MapPath(file_name2);
                        FileInfo file2 = new FileInfo(path2);
                        if (file2.Exists)
                        {
                            file2.Delete();
                            _imageRepository.Delete(item.Id);
                            _imageRepository.Save();
                        }
                       
                    }


                }
             
                if (ImageList!=null)
                {
                    
                    foreach (var pic in ImageList)
                    {
                        string fileName2 = Guid.NewGuid().ToString().Replace("-", "");
                        string extensionn2 = System.IO.Path.GetExtension(Request.Files[0].FileName);
                        string fullPath2 = "/Content/External/NewsPic/" + fileName2 + extensionn2;
                        pic.SaveAs(Server.MapPath(fullPath2));
                       
                            Image ımage = new Image();
                            ımage.NewsImage = fullPath2;
                            ımage.CreatedTime = DateTime.Now;
                            ımage.OnModifiedTime = DateTime.Now;
                            ımage.IsActived = true;
                            ımage.NewsId = _dbnews.Id;
                                _imageRepository.Insert(ımage);
                                _imageRepository.Save();
                               
                            
                    
                    }
                }



            }
            TempData["informaiton"] = "Başarı ile update edildi";
            return RedirectToAction("Index","News");
        }

        public void SetCatergoryList(object category=null)
        {
            var categoryList = _categoryRepository.GetMany(x => x.ParentId == 0).ToList();
            ViewBag.Category = categoryList;
        }

        //public int GetNewsLastId()
        //{
        //    var news=_newsRepository.Get(x=>x.)
        //}
    }
}