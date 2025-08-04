using JuniorOnly.Application.Commons;
using JuniorOnly.Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorOnly.Application.Extensions
{
    public static class PaginationExtensions
    {
        public static PaginatedResponse<TResult> ToResponse<TSource, TResult>(
            this PaginatedList<TSource> list,
            IEnumerable<TResult> mappedItems)
        {
            return new PaginatedResponse<TResult>
            {
                PageIndex = list.PageIndex,
                TotalPage = list.TotalPages,
                TotalCount = list.TotalCount,
                HasNextPage = list.HasNextPage,
                HasPreviousPage = list.HasPreviousPage,
                Items = mappedItems.ToList(),
            };
        }
    }
}
