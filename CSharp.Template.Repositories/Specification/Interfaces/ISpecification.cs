using System;
using System.Linq.Expressions;

namespace CSharp.Template.Repositories.Specification.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        Expression<Func<T, object>> GroupBy { get; }
        
        int Skip { get; }
        int Take { get; }
        bool IsPagingEnabled { get; }
    }
}