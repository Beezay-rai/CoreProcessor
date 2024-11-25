using ApiGateway.Library;
using ApiGateway.Services;
using Microsoft.AspNetCore.Identity;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

//builder.Services.AddSingleton<IProxyConfigProvider, YarpConfigProvider>();
builder.Services.AddCors(op =>
{
    op.AddPolicy("default", policy =>
    {
        policy.AllowAnyHeader().WithOrigins("https://localhost:7073").AllowAnyMethod().AllowCredentials();
    });
});
builder.Services.AddSwaggerGen();

builder.Services.AddReverseProxy()
                .AddTransforms(
                    option =>
                    {
                        option.AddRequestTransform(async context =>
                        {
                            var requestTransform = new CustomRequestTransformer();
                            await requestTransform.ApplyAsync(context);
                        });
                        option.AddResponseTransform(async context =>
                        {
                            var transofrm = new CustomResponseTransformer();
                            await transofrm.ApplyAsync(context);
                        });
                    }
                )
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy")); 


var app = builder.Build();

app.UseCors("default");
app.UseWebSockets();


app.UseSwagger();
app.UseSwaggerUI();



app.UseAuthorization();

app.MapReverseProxy(op =>
{
    
});

app.MapControllers();

app.Run();
