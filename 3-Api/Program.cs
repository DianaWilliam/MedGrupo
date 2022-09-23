using System.Text.Json.Serialization;
using DataAccess.ContactData;
using MedGrupo.Domain.ContactAggregate;
using MedGrupo.Services.ContactService;
using Repository.ContactRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(op => {
    op.JsonSerializerOptions.Converters. Add(new JsonStringEnumConverter());
    op.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactServices, ContactServices>();
builder.Services.AddScoped<IDBContext, SQLServerContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
