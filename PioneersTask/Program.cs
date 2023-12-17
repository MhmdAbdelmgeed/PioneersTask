using DataModel;
using DtoModel.Mappping;
using EntityFramework;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using PioneersTask.MiddleWares;
using Shared.Cache;
using Shared.ServiceRegister;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region sqlInject
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDataContext>(options =>
    options.UseSqlServer(connectionString));
#endregion
#region AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

#endregion
#region Using Scrutor To Scan Service

builder.Services.Scan(scan => scan
    .FromAssemblyDependencies(Assembly.GetExecutingAssembly())
    .AddClasses(classes => classes.AssignableTo(typeof(ITransientService)))
    .AsImplementedInterfaces()
    .WithTransientLifetime());

builder.Services.Scan(scan => scan
    .FromAssemblyDependencies(Assembly.GetExecutingAssembly())
    .AddClasses(classes => classes.AssignableTo(typeof(ISingletonService)))
    .AsImplementedInterfaces()
    .WithSingletonLifetime());

builder.Services.Scan(scan => scan
    .FromAssemblyDependencies(Assembly.GetExecutingAssembly())
    .AddClasses(classes => classes.AssignableTo(typeof(IScopedService)))
    .AsImplementedInterfaces()
    .WithScopedLifetime());
#endregion
#region UnitOfWork
builder.Services.AddTransient<IUnitOfWork<TransactionEntity>, UnitOfWork<ApplicationDataContext, TransactionEntity>>();
builder.Services.AddTransient<IUnitOfWork<GoodEnitty>, UnitOfWork<ApplicationDataContext, GoodEnitty>>();

#endregion
#region caching
builder.Services.AddScoped<ICacheService, CacheService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region AddCors
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

#endregion

app.UseAuthorization();

#region MiddleWare
app.UseMiddleware<ProfilingMiddleWare>();
#endregion

app.MapControllers();

app.Run();
