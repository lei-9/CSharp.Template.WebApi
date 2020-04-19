using System;
using System.Linq;
using MySql.Data.MySqlClient;

namespace CSharp.Template.Repositories
{
    public interface IBaseRepository<T>
    {
        MySqlConnection Connection { get; }
        Lazy<IQueryable<T>> Entities { get; }
    }
}