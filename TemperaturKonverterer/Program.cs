using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

var builder = WebApplication.CreateBuilder(args);

//Adding permissions
builder.Services.AddCors(options =>
{
    options.AddPolicy("allowAll", policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

var app = builder.Build();

//Enable CORS
app.UseCors("allowAll");

//Serve static files from wwwroot folder
app.UseStaticFiles();

//app.MapGet("/", () => "Hello World!");

//Redirect to index.html
app.MapGet("/", content =>
{
    content.Response.Redirect("/index.html");
    return Task.CompletedTask;
});

app.MapPost("/tempconv", ([FromForm] string inputStr, [FromForm] string unit1, [FromForm] string unit2) => 
{
    //Validate input number
    if (!double.TryParse(inputStr, out double input))
    {
        return Results.BadRequest("Input must be a valid number.");
    }

    //Check for absolute zero
    if (unit1 == "kelvin" && input < 0)
    {
        return Results.BadRequest("Kelvin cannot be lower than 0 (absolute zero)");
    }
    else if (unit1 == "celsius" && input < -273.15)
    {
        return Results.BadRequest("Celsius cannot be lower than -273.15 (absolute zero)");
    }
    else if (unit1 == "fahrenheit" && input < -459.67)
    {
        return Results.BadRequest("Fahrenheit cannot be lower than -459.67 (absolute zero)");
    }

    double tempKelvin;

    switch (unit1)
    {
        case "celsius":
        tempKelvin = input + 273.15;
        break;

        case "fahrenheit":
        tempKelvin = ((input + 459.67) / 1.8);
        break;

        case "kelvin":
        tempKelvin = input;
        break;

        default:
        return Results.BadRequest("Invalid input..");
    }

    switch (unit2)
    {
        case "celsius":
        return Results.Ok(new {message = $"{Math.Round(tempKelvin - 273.15), 2} ℃"});

        case "fahrenheit":
        return Results.Ok(new {message = $"{Math.Round(((tempKelvin * 1.8) - 459.67), 2)} ℉"});

        case "kelvin":
        return Results.Ok(new {message = $"{tempKelvin} K"});

        default:
        return Results.BadRequest("Error! Please pick either celsius, fahrenheit or kelvin.");
    }

}).DisableAntiforgery();


app.Run();
