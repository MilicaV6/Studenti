using Auth0.AspNetCore.Authentication;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using PrijemniMVC;
using PrijemniMVC.Mappings;
using PrijemniMVC.Models;
using System.Configuration;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<PrijemniContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString(name:"Prijemni")));
builder.Services.AddAutoMapper(typeof(Maps));
// Okta
//builder.Services.AddAuthentication(options =>
//{
//    //Ako je autentifikacioni cookie prisutan, koristi ga za dobijanje informacije o autentifikaciji
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    //Ako je autentifikacija zahtjevana i nema cookija, koristi Okta za prijavu 
//    options.DefaultChallengeScheme = "Okta";
//})
//    .AddCookie()
//    .AddOAuth("Okta", options =>
//     {
//         var oktaDomain = builder.Configuration.GetValue<string>("Okta:OktaDomain");
//         options.AuthorizationEndpoint = $"{oktaDomain}/oauth2/default/v1/authorize";

//         options.Scope.Add("openid");
//         options.Scope.Add("profile");
//         options.Scope.Add("email");

//         options.CallbackPath = new PathString("/authorization-code/callback");

//         options.ClientId = builder.Configuration.GetValue<string>("Okta:ClientId");
//         options.ClientSecret = builder.Configuration.GetValue<string>("Okta:ClientSecret");
//         options.TokenEndpoint = $"{oktaDomain}/oauth2/default/v1/token";

//         options.UserInformationEndpoint = $"{oktaDomain}/oauth2/default/v1/userinfo";

//         options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "sub");
//         options.ClaimActions.MapJsonKey(ClaimTypes.Name, "given_name");
//         options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");

//         options.Events = new OAuthEvents
//         {
//             OnCreatingTicket = async context =>
//               {
//                   var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
//                   request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//                   request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

//                   var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
//                   response.EnsureSuccessStatusCode();



//                   var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

//                   context.RunClaimActions(user.RootElement);
//               }
//         };
//     });

// google
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
    .AddCookie(options =>
    {
        options.LoginPath = "/account/google-login";       
    })
    .AddGoogle(GoogleDefaults.AuthenticationScheme,options =>
    {
        options.ClientId = builder.Configuration["Google:ClientId"];
        options.ClientSecret = builder.Configuration["Google:ClientSecret"];
       
    });

// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Students}/{action=Index}/{id?}");

app.Run();
