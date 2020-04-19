using System;
using System.Linq;

namespace CSharp.Template.IRepositories
{
    public interface IBaseRepository<T>
    {
        Lazy<IQueryable<T>> Entities { get;  }
    }
}