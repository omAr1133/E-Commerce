﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal abstract class BaseSpecifications<T> : ISpecifications<T> where T : class
    {
        protected BaseSpecifications(Expression<Func<T,bool>>? criteria)
        {
            Criteria= criteria;
        }
        public Expression<Func<T, bool>> Criteria { get;  }

        public List<Expression<Func<T, object>>> IncludeExpressions { get; } = [];

        protected void AddInclude(Expression<Func<T, object>> include)
        {
            IncludeExpressions.Add(include);
        }

    }


}
