using Microsoft.Extensions.DependencyInjection;
using Library.Infrastructure.Data;
using Library.ApplicationCore;
using Microsoft.Extensions.Configuration;
using Library.ApplicationCore.Services;

var services = new ServiceCollection();

var configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appSettings.json")
.Build();

services.AddSingleton<IConfiguration>(configuration);

services.AddScoped<IPatronRepository, JsonPatronRepository>();
services.AddScoped<ILoanRepository, JsonLoanRepository>();
services.AddScoped<ILoanService, LoanService>();
services.AddScoped<IPatronService, PatronService>();
services.AddScoped<BookService>();

services.AddSingleton<JsonData>();
services.AddSingleton<ConsoleApp>(provider =>
{
	var loanService = provider.GetRequiredService<ILoanService>();
	var patronService = provider.GetRequiredService<IPatronService>();
	var patronRepository = provider.GetRequiredService<IPatronRepository>();
	var loanRepository = provider.GetRequiredService<ILoanRepository>();
	var bookService = provider.GetRequiredService<BookService>();
	var jsonData = provider.GetRequiredService<JsonData>();
	return new ConsoleApp(loanService, patronService, patronRepository, loanRepository, bookService, jsonData);
});

var servicesProvider = services.BuildServiceProvider();

var consoleApp = servicesProvider.GetRequiredService<ConsoleApp>();
consoleApp.Run().Wait();
