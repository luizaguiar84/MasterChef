using System.Collections.Generic;

namespace MasterChef.Domain.Models;

public class ResultDto<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public int Page { get; set; } = 0;
    public int TotalItems { get; set; } = 0;
}