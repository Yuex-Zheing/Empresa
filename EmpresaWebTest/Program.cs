using EmpresaWebTest.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Configuration.AddJsonFile("appsettings.json", false, true);
var _ambiente = builder.Configuration.GetSection("Ambiente").Get<EmpresaWebTest.Models.Ambiente>();


#region Inyccion de dependencia

// Conexion de Repos siempre la misma por Peticion
builder.Services.AddScoped<IEmpresaRepos>(x=> new EmpresaRepos(_ambiente.apiUri)); //, EmpresaRepos>();


#endregion

#region Builder
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Urls.Add("http://*:9000/");

app.Run();
#endregion