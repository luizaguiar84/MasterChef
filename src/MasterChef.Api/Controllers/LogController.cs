using Elasticsearch.Net;
using MasterChef.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace MasterChef.Api.Controllers;

/// <summary>
/// Log Controller
/// </summary>
[ApiController]
[Route("[controller]")]
public class LogController : ControllerBase
{
    private readonly IElasticClient _elasticClient;

    /// <summary>
    /// Log Controller
    /// </summary>
    /// <param name="elasticClient"></param>
    public LogController(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    /// <summary>
    /// Get All Logs
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ElasticsearchClientException"></exception>
    [HttpGet("all")]
    public async Task<IActionResult> GetAllLogs()
    {
        var search = new SearchDescriptor<LogViewModel>("indexlogs")
            .MatchAll()
            .Size(100)
            .Sort(descriptor => descriptor.Descending(p => p.Timestamp));

        var response = await _elasticClient.SearchAsync<LogViewModel>(search);

        if (!response.IsValid)
            throw new ElasticsearchClientException(response?.ServerError?.ToString());

        return Ok(response.Documents.ToList());
    }

    /// <summary>
    /// Get Logs by Date
    /// </summary>
    /// <param name="dateBegin"></param>
    /// <param name="dateEnd"></param>
    /// <returns></returns>
    /// <exception cref="ElasticsearchClientException"></exception>
    [HttpGet("date")]
    public async Task<IActionResult> GetLogsByDate([FromQuery] DateTime dateBegin, [FromQuery] DateTime dateEnd)
    {
        var search = new QueryContainerDescriptor<LogViewModel>()
            .Bool(b => b.Filter(f => f.DateRange(dt => dt
                .Field(field => field.Timestamp)
                .GreaterThanOrEquals(dateBegin)
                .LessThanOrEquals(dateEnd)
                .TimeZone("+00:00"))));

        var response = await _elasticClient.SearchAsync<LogViewModel>(descriptor => descriptor
            .Index("indexlogs")
            .Size(100)
            .Sort(sort => sort.Descending(p => p.Timestamp))
            .Query(_ => search));

        if (!response.IsValid)
            throw new ElasticsearchClientException(response?.ServerError?.ToString());

        return Ok(response.Documents.ToList());
    }
}