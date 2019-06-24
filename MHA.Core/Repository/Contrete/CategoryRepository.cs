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
    public class CategoryRepository:ICategoryRepository
    {
            private readonly NewsContext _context = new NewsContext();

            public IEnumerable<Category> GetAll()
            {
                return _context.Category.Select(i => i);
            }

            public Category GetById(int id)
            {
                return _context.Category.FirstOrDefault(i => i.Id == id);
            }

            public Category Get(Expression<Func<Category, bool>> expression)
            {
                return _context.Category.FirstOrDefault(expression);
            }


            public IQueryable<Category> GetMany(Expression<Func<Category, bool>> expression)
            {
                return _context.Category.Where(expression);
            }

            public void Insert(Category obj)
            {
                _context.Category.Add(obj);
            }
            public void Update(Category obj)
            {
                _context.Category.AddOrUpdate();
            }
            public int Count()
            {
                return _context.Category.Count();
            }

            public void Delete(int id)
            {
                var Category = GetById(id);
                if (Category != null)
                    _context.Category.Remove(Category);
            }

            public void Save()
            {
                _context.SaveChanges();
            }

        public Category GetFirst()
        {
            return _context.Category.First();
        }
    }
}
