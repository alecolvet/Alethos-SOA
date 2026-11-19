using Microsoft.EntityFrameworkCore;
using ProtegePlus.Api.Data;
using ProtegePlus.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProtegePlusDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ProtegePlus")));
builder.Services.AddScoped<IOcorrenciaService, OcorrenciaService>();
builder.Services.AddScoped<ISolicitacaoAjudaService, SolicitacaoAjudaService>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("/", () => Results.Ok(new
{
    sistema = "Protege+ API",
    mensagem = "API de apoio à prevenção de golpes digitais.",
    documentacao = "/swagger"
}));
app.Run();

public partial class Program { }
