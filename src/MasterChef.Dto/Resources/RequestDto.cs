

namespace MasterChef.Dto.Resources;

public class RequestDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public RequestDto()
    {
        Page = 0;
        PageSize = 50;
    }
    
    public RequestDto(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;

        if (Page <= 0) 
            Page = 0;

        if (PageSize <= 0) 
            PageSize = 50;
    }
}