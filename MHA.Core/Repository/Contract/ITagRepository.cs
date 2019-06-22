using MHA.Data.Domain;
using MHA.Data.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Repository.Contract
{
    public interface ITagRepository : IRepository<Tag>
    {
        IQueryable<Tag> Tagss(string[] tags);
        void AddTag(int NewsId, string tag);
        void AddNewsTag(int NewsId, string[] tags);

    }
}
