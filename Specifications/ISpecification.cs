using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ThirdTryAPI.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T,bool>> Criteria { get; }
        List<Expression<Func<T,object>>> Includes { get; }
        
        //paging and sorting
        Expression<Func<T,object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}
