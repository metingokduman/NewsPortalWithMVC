using MHA.Core.Repository.Contract;
using MHA.Data.DataContext;
using MHA.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Linq.Expressions;

namespace MHA.Core.Repository.Contrete
{
    public class RoleRepository:IRoleRepository
    {
        private readonly NewsContext _context = new NewsContext();

        public IEnumerable<Role> GetAll()
        {
            return _context.Role.Select(i => i);
        }

        public Role GetById(int id)
        {
            return _context.Role.FirstOrDefault(i => i.Id == id);
        }

        public Role Get(Expression<Func<Role, bool>> expression)
        {
            return _context.Role.FirstOrDefault(expression);
        }


        public IQueryable<Role> GetMany(Expression<Func<Role, bool>> expression)
        {
            return _context.Role.Where(expression);
        }

        public void Insert(Role obj)
        {
            _context.Role.Add(obj);
        }
        public void Update(Role obj)
        {
            _context.Role.AddOrUpdate();
        }
        public int Count()
        {
            return _context.Role.Count();
        }

        public void Delete(int id)
        {
            var Role = GetById(id);
            if (Role != null)
                _context.Role.Remove(Role);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
