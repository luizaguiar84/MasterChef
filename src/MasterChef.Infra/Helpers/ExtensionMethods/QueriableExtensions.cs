using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterChef.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterChef.Infra.Helpers.ExtensionMethods;

public static class QueriableExtensions
{
    public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> query, RequestDto key)
    {
        var response = await query
            .Skip(key.Page * key.PageSize)
            .Take(key.PageSize)
            .ToListAsync();

        return response;
    }
}