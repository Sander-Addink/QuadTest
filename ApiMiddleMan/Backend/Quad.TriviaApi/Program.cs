using Trivia.API.Models;
using Trivia.API;
using Quad.TriviaApi.Services;
using Quad.Shared.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "QuadSolutions";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? [];
var apiBase = builder.Configuration.GetSection("ApiBase").Get<string>() ?? string.Empty;

builder.Services.AddCors(options =>
{
    options.AddPolicy("policy", policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});
builder.Services.AddTrivia(new TriviaAPISettings()
{
    EndpointBase = apiBase
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITriviaCacheService, TriviaCacheService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<QuestionMapper>();

builder.Services.AddDistributedMemoryCache();

var app = builder.Build();
app.UseCors("policy");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseSession();

app.MapControllers();

app.Run();
