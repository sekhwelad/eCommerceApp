﻿using eShopper.Core.Entities;
using eShopper.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopper.Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity 
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputeQuery , 
            ISpecification<TEntity> spec)
        {
            var query = inputeQuery;
            if (spec.Criteria != null)
            {
                query =query.Where(spec.Criteria);  // p=> p.ProductTypeId == id;
            }
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
