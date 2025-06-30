using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web;
using Web.Features.Auth.Confirm;
using Web.Features.Auth.Profile;
using Web.Features.Auth.SignUp;
using Web.Features.Auth.SignIn;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBase = builder.Configuration["ApiUrl"];
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(apiBase) });

builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<ISignUpService, SignUpService>();
builder.Services.AddScoped<IConfirmService, ConfirmService>();
builder.Services.AddScoped<ISignInService, SignInService>();

await builder.Build().RunAsync();   