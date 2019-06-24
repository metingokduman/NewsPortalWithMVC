using MHA.Core.Repository.Contract;
using MHA.Data.DataContext;
using MHA.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;//add or update

namespace MHA.Core.Repository.Contrete
{
    public class NewsRepository : INewsRepository
    {
        private readonly NewsContext _context = new NewsContext();

        public IEnumerable<News> GetAll()
        {
            return _context.News.Select(i => i);
        }

        public News GetById(int id)
        {
            return _context.News.FirstOrDefault(i => i.Id==id);
        }

        public News Get(Expression<Func<News, bool>> expression)
        {
            return _context.News.FirstOrDefault(expression);
        }

        
       

        public IQueryable<News> GetMany(Expression<Func<News, bool>> expression)
        {
            return _context.News.Where(expression);
        }

        public void Insert(News obj)
        {
            _context.News.Add(obj);
        }
        public void Update(News obj)
        {
            _context.News.AddOrUpdate();
        }
        public int Count()
        {
            return _context.News.Count();
        }

        public void Delete(int id)
        {
            var news = GetById(id);
            if (news != null)
                _context.News.Remove(news);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IQueryable<News> GetManyDistinct()
        {
            return _context.News.OrderBy(x => x.CategoryId).Distinct();
        }
    }
}
