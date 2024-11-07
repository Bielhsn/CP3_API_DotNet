using CP3.IoC;

var builder = WebApplication.CreateBuilder(args);

// Configura��o de servi�os padr�o
builder.Services.AddControllers();

// Configura��o de Inje��o de Depend�ncia
Bootstrap.Start(builder.Services, builder.Configuration);

var app = builder.Build();

// Configura��o do pipeline de requisi��o HTTP
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