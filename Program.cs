using ProductsAppRPSpetnagel.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

ProductsAppRPSpetnagel.Utils.ConnectionStringHelper.CONNSTR_SQL =
builder.Configuration.GetConnectionString("ProductsDBConnSql") ?? throw new
InvalidOperationException("Connection string 'ProductsDBConnSql' not found."); // added
ProductsAppRPSpetnagel.Utils.ConnectionStringHelper.CONNSTR_MYSQL =
builder.Configuration.GetConnectionString("ProductsDBConnMySql") ?? throw new
InvalidOperationException("Connection string 'ProductsDBConnMySql' not found."); // added

builder.Services.AddRazorPages().AddRazorOptions(options =>
{
    options.PageViewLocationFormats.Add("/Pages/Partials/{0}.cshtml");
});
// builder provides a bridge
//builder.Services.AddSingleton<IProductsRepository, RepositoryList>();
builder.Services.AddSingleton<IProductsRepository, RepositoryDBSQL>();
//builder.Services.AddSingleton<IProductsRepository, RepositoryDBMySql>();

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

app.MapRazorPages();

app.Run();
