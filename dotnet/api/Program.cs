
using api.Controllers;
using api.Correlation;
using api.QueryHandlers;
using api.QueryHandlers.History;
using api.QueryHandlers.Summary;
using database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SchemaFilter<ExampleSchemaFilter>();
});

builder.Services.AddDbContext<InvestmentsDbContext>(
    opts => opts.UseSqlServer(
        "Server=localhost;Initial Catalog=investments;Persist Security Info=False;User ID=sa;Password=Password123!;MultipleActiveResultSets=False;Encrypt=True;Trust Server Certificate=True;Connection Timeout=30;")
);

builder.Services.AddScoped<ISummaryQueryHandler, SummaryQueryHandler>();
builder.Services.AddScoped<IAccountQueryHandler, AccountQueryHandler>();
builder.Services.AddScoped<IHistoryQueryHandler, HistoryQueryHandler>();

builder.Services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
//    app.UseSwaggerUI(c => c.);
}


app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

// app.UseAuthorization();
app.MapGet("/", http =>
{
    http.Response.Redirect("/swagger/index.html", false);
    return Task.CompletedTask;
});


app.MapControllers();

app.UseMiddleware<CorrelationIdMiddleware>();

app.Run();
