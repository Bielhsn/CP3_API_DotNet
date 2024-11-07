using CP3.IoC;

var builder = WebApplication.CreateBuilder(args);

// Configuração de serviços padrão
builder.Services.AddControllers();

// Configuração de Injeção de Dependência
Bootstrap.Start(builder.Services, builder.Configuration);

var app = builder.Build();

// Configuração do pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();