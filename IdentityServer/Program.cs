var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddSwaggerGen(opt =>
{
});

builder.Services.AddIdentityServer(opt =>
{
    
}).;

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.DocumentTitle = "Identity Server";
});


app.Run();


