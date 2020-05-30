﻿using Emprise.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emprise.Domain.Core.Extensions
{
    public static class PagerExtensions
    {
        /// <summary>
        /// 分页 
        /// </summary>
        /// <typeparam name="T">要返回的实体类型</typeparam>
        /// <param name="query"></param>
        /// <param name="page">第几页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Paging<T> Paged<T>(this IQueryable<T> query, int page, int pageSize, int count)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            return new Paging<T>()
            {
                PageIndex = page,
                Count = count,
                PageCount = count % pageSize == 0 ? count / pageSize : (count / pageSize + 1),
                Data = query.Skip((page - 1) * pageSize).Take(pageSize).ToList()
            };
        }
    }
}