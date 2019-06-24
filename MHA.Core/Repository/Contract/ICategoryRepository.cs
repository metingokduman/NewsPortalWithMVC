using MHA.Data.Domain;
using MHA.Data.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Core.Repository.Contract
{
    public interface ICategoryRepository:IRepository<Category>
    {
        Category GetFirst();
    }
}
