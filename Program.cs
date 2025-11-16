using MySqlConnector;
var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddTransient<MySqlConnection>(_ =>
    new MySqlConnection(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {

            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


builder.Services.AddControllers();


builder.Services.AddAuthorization();


var app = builder.Build();


app.UseCors("AllowReactApp");


app.UseAuthorization();

app.MapControllers();

app.Run();