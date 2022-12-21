using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterChef.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterChef.Infra.Helpers.ExtensionMethods;

public static class QueriableExtensions
{
    public static async Task<ResultDto<T>> ToListAsync<T>(this IQueryable<T> query, RequestDto key)
    {
        var totalItems = await query.CountAsync();

        var response = await query
            .Skip(key.Page * key.PageSize)
            .Take(key.PageSize)
            .ToListAsync();

        return new ResultDto<T>()
        {
            Page = key.Page,
            PageSize = key.PageSize,
            TotalItems = totalItems,
            Items = response
        };
    }
}