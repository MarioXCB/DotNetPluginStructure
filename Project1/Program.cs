using Core.ControllerRegistrator;
using Core.PluginBus;
using Humanizer.Bytes;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Description = "Docs for my API", Version = "v1" });
});

builder.Services.AddSingleton<IPluginManager, ControllerPluginManager>();
builder.Services.AddSingleton<IServiceCollection, ServiceCollection>();
builder.Services.AddSingleton<IControllerRegistrator>(new ControllerRegistrator(builder.Services));
builder.Services.AddSingleton<Microsoft.AspNetCore.Mvc.Infrastructure.IActionDescriptorChangeProvider>(MyActionDescriptorChangeProvider.Instance);
builder.Services.AddSingleton(MyActionDescriptorChangeProvider.Instance);

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = ByteSize.FromGigabytes(10).Bits;
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = ByteSize.FromGigabytes(10).Bits; // if don't set default value is: 30 MB
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
