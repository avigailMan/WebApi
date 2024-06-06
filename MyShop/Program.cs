
using Microsoft.EntityFrameworkCore;
using middlewares;
using MyShop;
using NLog.Web;
using PresidentsApp.Middlewares;
using Repository;
using Service;
using Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ICategoryServices,CategoryServices>();

builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();

builder.Services.AddDbContext<ProductDbContext>(option => option.UseSqlServer("Data Source=SRV2\\PUPILS;Initial Catalog=Product_db;Integrated Security=True;Encrypt=False"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseNLog();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseErrorHandlingMiddleware();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();
app.UseRatingMiddleware();

app.MapControllers();

app.Run();



