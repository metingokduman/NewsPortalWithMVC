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
    public class TagRepository : ITagRepository
    {
        private readonly NewsContext _context = new NewsContext();

        public IEnumerable<Tag> GetAll()
        {
            return _context.Tag.Select(i => i);
        }

        public Tag GetById(int id)
        {
            return _context.Tag.FirstOrDefault(i => i.Id == id);
        }

        public Tag Get(Expression<Func<Tag, bool>> expression)
        {
            return _context.Tag.FirstOrDefault(expression);
        }



        public IQueryable<Tag> GetMany(Expression<Func<Tag, bool>> expression)
        {
            return _context.Tag.Where(expression);
        }

        public void Insert(Tag obj)
        {
            _context.Tag.Add(obj);
        }
        public void Update(Tag obj)
        {
            _context.Tag.AddOrUpdate();
        }
        public int Count()
        {
            return _context.Tag.Count();
        }

        public void Delete(int id)
        {
            var Tag = GetById(id);
            if (Tag != null)
                _context.Tag.Remove(Tag);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IQueryable<Tag> Tagss(string[] tags)
        {
            return _context.Tag.Where(x => tags.Contains(x.TagName));
        }

        public void AddTag(int NewsId, string tag)
        {
            if (!string.IsNullOrWhiteSpace(tag))
            {
                string[] Tags = tag.Split(',');
                foreach (var item in Tags)
                {
                    Tag tag_ = this.Get(x => x.TagName.ToLower() == item.ToLower().Trim());
                    if (tag_==null)
                    {
                        tag_ = new Tag();
                        tag_.TagName = item;
                        this.Insert(tag_);
                        this.Save();
                    }
                    
                }
                this.AddNewsTag(NewsId, Tags);
            }
           

        }

        public void AddNewsTag(int NewsId, string[] tags)
        {
            var News = _context.News.FirstOrDefault(x => x.Id == NewsId);
            var returnedTag = this.Tagss(tags);

            News.Tag.Clear();
            returnedTag.ToList().ForEach(tag => News.Tag.Add(tag));
            _context.SaveChanges();
        }
    }
}
