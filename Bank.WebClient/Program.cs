var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
string serverHeader = "0";
var app = builder.Build();
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Server", serverHeader);
    // Set CSP
    context.Response.Headers.Append("Content-Security-Policy",
        "script-src 'self'; " +
        "style-src 'self' https://fonts.googleapis.com;" +
        "img-src 'self';" +
        "font-src 'self' https://fonts.gstatic.com;");
    await next(context);
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
