using Questionnaire.Components;
using Questionnaire.Mappers;
using Questionnaire.States;
using Trivia.API;
using Trivia.API.Models;

internal static class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddTrivia(new TriviaAPISettings()
            {
                EndpointBase = "https://opentdb.com/api.php"
            });

        builder.Services.AddScoped<QuestionSessionState>();
        builder.Services.AddScoped<QuestionMapper>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
