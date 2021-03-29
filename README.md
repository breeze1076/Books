Task Description:
Using ASP.NET Core MVC create an app (.Net Core 3.1), that will have following functionality:
3 tables:
- Authors
- Books
- Clients
Relations:
- Author can have many books
- Book can have many authors
- Client can borrow many books
Using EF Core
- implement functionality for seeding data
- create controllers and views with corresponded infrastructure for CRUD operations
Create a middleware that logs
- usefull information for incomming requests (query strings, headers, body etc). 
- exception details
- should have a corresponded settting in appsettings.json to enable/disable request logging
- should have possibility to swith on/off without restarting app (use Options API)                                     1 
- log request Id in all log entries in the scope of request (use BeginScope from Microsoft.Extensions.Logging)         2
Logging
- configure app to use console and file logging
- log file format - JSON                                                                                               3
- each log entry in file should contain execution context - thread Id, app name, machine name, environment name
- use Serilog or NLog as a loging library
- use semantic (structured) logging approach                                                                           4
Dependency injection
Controllers should be thin and not contain business logic.Thus, all services should be injected into constructor/action (use both approaches 5

My notes:
!!! Path to the log files: ..\Books\bin\Debug\netcoreapp3.1\logs\{shortdate}.log

