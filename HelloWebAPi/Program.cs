var builder = WebApplication.CreateBuilder(args);

//Service(container)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
 app.UseSwagger();
 app.UseSwaggerUI();
 app.MapControllers();

app.Run();
