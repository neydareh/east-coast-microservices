using Basket.API.Controllers;
using Basket.API.GRPC.Services;
using Basket.API.Repositories;
using Discount.GRPC.Protos;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Redis
builder.Services
  .AddStackExchangeRedisCache(options =>
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString"));

// Repositories and Services
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<DiscountGrpcService>();

// GRPC
builder.Services
  .AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
    options.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:DiscountUrl")));

// Mapper
builder.Services.AddAutoMapper(typeof(Program));

// RabbitMQ
builder.Services.AddMassTransit(config =>
{
  config.UsingRabbitMq((_, cfg) => { cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]); });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();