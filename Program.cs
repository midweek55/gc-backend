using UserManagementApi.Application.Services;
using UserManagementApi.Domain.Ports;
using UserManagementApi.Infrastructure;
using UserManagementApi.Infrastructure.Persistence.MongoDb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MongoDB configuration
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddScoped<IUserRepository, MongoDbUserRepository>();
builder.Services.AddScoped<UserApplicationService>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", 
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
