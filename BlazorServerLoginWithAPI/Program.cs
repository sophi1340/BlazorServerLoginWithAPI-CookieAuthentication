using MudBlazor.Services;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.WithOrigins("https://yourclientdomain.com")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // اجازه ارسال کوکی‌ها
        });
});


builder.Services.AddMudServices();
builder.Services.AddControllers();
builder.Services.AddAuthentication()
       .AddCookie("Cookies", options =>
       {
           options.Cookie.SameSite = SameSiteMode.None;
           options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
           options.Cookie.HttpOnly = false;
           //options.ExpireTimeSpan = TimeSpan.Zero;
       });


builder.Services.AddHttpClient("", options =>
{
    options.BaseAddress = new Uri("https://localhost:7190/");
    options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

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
app.UseCors("AllowAllOrigins"); // استفاده از سیاست CORS

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();



app.Run();
