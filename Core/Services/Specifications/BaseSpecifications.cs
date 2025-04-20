using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal abstract class BaseSpecifications<T>(Expression<Func<T, bool>>? criteria)
        : ISpecifications<T> where T : class
    {
        public Expression<Func<T, bool>> Criteria { get; } = criteria!;

        public List<Expression<Func<T, object>>> IncludeExpressions { get; } = [];

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> include) => IncludeExpressions.Add(include);

        protected void AddOrderBy(Expression<Func<T, object>> orderby) => OrderBy = orderby;
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescending) => OrderByDescending = orderByDescending;


    }


}
