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
    public class AppUserRepository:IAppUserRepository
    {
        private readonly NewsContext _context = new NewsContext();

        public IEnumerable<AppUser> GetAll()
        {
            return _context.AppUser.Select(i => i);
        }

        public AppUser GetById(int id)
        {
            return _context.AppUser.FirstOrDefault(i => i.Id == id);
        }
       

        public AppUser Get(Expression<Func<AppUser, bool>> expression)
        {
            return _context.AppUser.FirstOrDefault(expression);
        }


        public IQueryable<AppUser> GetMany(Expression<Func<AppUser, bool>> expression)
        {
            return _context.AppUser.Where(expression);
        }

        public void Insert(AppUser obj)
        {
            _context.AppUser.Add(obj);
        }
        public void Update(AppUser obj)
        {
            _context.AppUser.AddOrUpdate();
        }
        public int Count()
        {
            return _context.AppUser.Count();
        }

        public void Delete(int id)
        {
            var AppUser = GetById(id);
            if (AppUser != null)
                _context.AppUser.Remove(AppUser);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public AppUser GetByEmail(string email)
        {
            return _context.AppUser.FirstOrDefault(i => i.EmailAddress == email);
        }
        
    }
}
