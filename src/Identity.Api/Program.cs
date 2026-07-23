var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
    TypedResults.Ok(new ServiceInformation(Name: "Identity.Api", Status: "Running")));

app.Run();

public sealed record ServiceInformation(
    string Name,
    string Status);

public partial class Program;