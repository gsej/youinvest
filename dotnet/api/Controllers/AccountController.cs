using api.QueryHandlers;
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
                ["date"] = new OpenApiString("2020-10-30"),
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

    public AccountController(ILogger<AccountController> logger, 
        ISummaryQueryHandler summaryQueryHandler,
        IAccountQueryHandler accountQueryHandler)
    {
        _logger = logger;
        _summaryQueryHandler = summaryQueryHandler;
        _accountQueryHandler = accountQueryHandler;
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
}