using api.QueryHandlers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ISummaryQueryHandler _summaryQueryHandler;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ISummaryQueryHandler summaryQueryHandler)
    {
        _logger = logger;
        _summaryQueryHandler = summaryQueryHandler;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost("/account/summary")]
  //  [SwaggerRequestExample]
    public async Task<SummaryResult> GetSummary([FromBody] SummaryRequest request)
    {
        return await _summaryQueryHandler.Handle(request);
    }
}
//
// public class ValueResponseModelExample : IExamplesProvider
// {
//     public virtual object GetExamples()
//     {
//         var value = new ValueReference()
//         {
//             Id = Guid.NewGuid(),
//             Name = "value name",
//             Value = 123,
//             IsValid = true,
//             AddedOn = DateTimeOffset.Now
//         };
//         var model = new ValueResponseModel() { Value = value };
//
//         return model;
//     }
// }
