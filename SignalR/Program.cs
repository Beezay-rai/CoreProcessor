using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using SignalR.Security;
using SignalR.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(op =>
{
    op.AddPolicy("api_gate", policy =>
    {
        //policy.AllowAnyHeader().WithOrigins("https://localhost:7245").AllowAnyMethod().AllowCredentials();
        policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});
builder.Services.AddAuthentication()
    .AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("Basic",null);
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Basic", policy =>
    {
        policy.AddAuthenticationSchemes("Basic");
        policy.RequireAuthenticatedUser();
    });

builder.Services.AddSignalR(opt =>
{
    opt.KeepAliveInterval =TimeSpan.FromSeconds(5);
    opt.ClientTimeoutInterval = TimeSpan.FromSeconds(5);
});
//builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    app.UseExceptionHandler("/Home/Error");
}

app.UseCors("api_gate");

app.UseAuthentication();
app.UseAuthorization();


app.MapHub<ChatHub>("/chathub", opt =>
{
    opt.AuthorizationData.Add(new AuthorizeAttribute());
});



app.Run();
