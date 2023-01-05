namespace MasterChef.Dto.Dto;

public class ResultDto<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public int Page { get; set; } = 0;
    public int PageSize { get; set; } = 0;
    public int TotalItems { get; set; } = 0;
}