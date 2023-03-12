using CurrieTechnologies.Razor.SweetAlert2;
using EnglishChallengesWebApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Supabase;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSpeechSynthesis();
builder.Services.AddSweetAlert2();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.

    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
});


var supabaseUrl = "https://qhmtgkkbpwqfroujzubx.supabase.co";
var supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InFobXRna2ticHdxZnJvdWp6dWJ4Iiwicm9sZSI6ImFub24iLCJpYXQiOjE2NzgzNDA0NTgsImV4cCI6MTk5MzkxNjQ1OH0.i8n4_4GpRhtAsMwy0Jou2Qo1tf8y4W2ccIrKzgoPXzw";


var options = new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true
    //SessionHandler = new SupabaseSessionHandler()
};

builder.Services.AddSingleton(provider => new Client(supabaseUrl, supabaseKey, options));

await builder.Build().RunAsync();
