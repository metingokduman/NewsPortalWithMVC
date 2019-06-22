using MHA.Core.Repository.Contract;
using MHA.Data.DataContext;
using MHA.Data.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Repository.Contrete
{
    public class SliderRepository:ISliderRepository
    {
        private readonly NewsContext _context = new NewsContext();

        public IEnumerable<Slider> GetAll()
        {
            return _context.Slider.Select(i => i);
        }

        public Slider GetById(int id)
        {
            return _context.Slider.FirstOrDefault(i => i.Id == id);
        }

        public Slider Get(Expression<Func<Slider, bool>> expression)
        {
            return _context.Slider.FirstOrDefault(expression);
        }


        public IQueryable<Slider> GetMany(Expression<Func<Slider, bool>> expression)
        {
            return _context.Slider.Where(expression);
        }

        public void Insert(Slider obj)
        {
            _context.Slider.Add(obj);
        }
        public void Update(Slider obj)
        {
            _context.Slider.AddOrUpdate();
        }
        public int Count()
        {
            return _context.Slider.Count();
        }

        public void Delete(int id)
        {
            var Slider = GetById(id);
            if (Slider != null)
                _context.Slider.Remove(Slider);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
