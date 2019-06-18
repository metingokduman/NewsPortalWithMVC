using MHA.Core.Repository.Contract;
using MHA.Data.DataContext;
using MHA.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace MHA.Core.Repository.Contrete
{
    public class ImageRepository:IImageRepository
    {
        private readonly NewsContext _context = new NewsContext();

        public IEnumerable<Image> GetAll()
        {
            return _context.Image.Select(i => i);
        }

        public Image GetById(int id)
        {
            return _context.Image.FirstOrDefault(i => i.Id == id);
        }

        public Image Get(Expression<Func<Image, bool>> expression)
        {
            return _context.Image.FirstOrDefault(expression);
        }

        
        public IQueryable<Image> GetMany(Expression<Func<Image, bool>> expression)
        {
            return _context.Image.Where(expression);
        }

        public void Insert(Image obj)
        {
            _context.Image.Add(obj);
        }
        public void Update(Image obj)
        {
            _context.Image.AddOrUpdate();
        }
        public int Count()
        {
            return _context.Image.Count();
        }

        public void Delete(int id)
        {
            var Image = GetById(id);
            if (Image != null)
                _context.Image.Remove(Image);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
