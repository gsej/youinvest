using api.QueryHandlers;
using api.QueryHandlers.History;
using api.QueryHandlers.Summary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace api.Controllers;


public class ExampleSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(SummaryRequest))
        {
            schema.Example = new OpenApiObject()
            {
                ["accountCodes"] = new OpenApiArray { new OpenApiString("GSEJ-SIPP")},
                ["date"] = new OpenApiString("2024-10-30"),
            };
        }
    }
}


[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freeoing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public record AccountResponse(IList<Account> Accounts);

    private readonly ILogger<AccountController> _logger;
    private readonly ISummaryQueryHandler _summaryQueryHandler;
    private readonly IAccountQueryHandler _accountQueryHandler;
    private readonly IHistoryQueryHandler _historyQueryHandler;

    public AccountController(ILogger<AccountController> logger, 
        ISummaryQueryHandler summaryQueryHandler,
        IAccountQueryHandler accountQueryHandler, 
        IHistoryQueryHandler historyQueryHandler)
    {
        _logger = logger;
        _summaryQueryHandler = summaryQueryHandler;
        _accountQueryHandler = accountQueryHandler;
        _historyQueryHandler = historyQueryHandler;
    }

    [HttpGet("/accounts")]
    public async Task<AccountResponse> GetAccounts()
    {
            var accounts = await _accountQueryHandler.Handle(new AccountRequest());
            return new AccountResponse(accounts);
    }

    [HttpPost("/account/summary")]
    public async Task<SummaryResult> GetSummary([FromBody] SummaryRequest request)
    {
        return await _summaryQueryHandler.Handle(request);
    }
    
    [HttpPost("/account/history")]
    public async Task<HistoryResult> GetHistory([FromBody] HistoryRequest request)
    {
        return await _historyQueryHandler.Handle(request);
    }
}