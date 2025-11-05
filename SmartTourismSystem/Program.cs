using Microsoft.EntityFrameworkCore;
using SmartTourismSystem.Models;
using SmartTourismSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// اضافه کردن سرویس HTTP Client
builder.Services.AddHttpClient();


// ثبت سرویس Gemini با API Key
builder.Services.AddScoped<IGeminiService>(provider =>
{
    var httpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient();

    string apiKey = "AIzaSyBtrlMFYlaX-XOs_WJVLeX_tUYiXiYqi4A";

    Console.WriteLine($"Using Gemini API Key: {apiKey.Substring(0, 10)}...");

    return new GeminiService(httpClient, apiKey);
});


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<SmartTourismService>();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages();

app.Run();
