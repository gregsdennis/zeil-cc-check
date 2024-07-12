using Serilog;
using Serilog.Enrichers.Sensitive;
using System.Text.Json;

Log.Logger = new LoggerConfiguration()
	.Enrich.WithSensitiveDataMasking(_ => { })
	.WriteTo.Console()
	.CreateLogger();

try
{
	var builder = WebApplication.CreateBuilder(args);

	builder.Services.AddSerilog();
	builder.Services.AddControllers()
		.AddJsonOptions(options =>
		{
			options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
		}); ;
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();

	var app = builder.Build();

	app.UseSerilogRequestLogging();

	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseHttpsRedirection();

	app.UseAuthorization();

	app.MapControllers();

	app.Run();

}
catch (Exception e)
{
	Log.Fatal(e, "Application terminated unexpectedly");
}
finally
{
	Log.CloseAndFlush();
}

public partial class Program;