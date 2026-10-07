using CountryLookupApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("CountryDatabase"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


// GET
app.MapGet("/api/countries/lookup",
    async (string phoneNumber, AppDbContext context) =>
    {

        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return Results.BadRequest(new
            {
                message = "Phone number is required."
            });
        }


        var number = phoneNumber.Trim();

        if (number.StartsWith("+"))
        {
            number = number[1..];
        }


        if (!number.All(char.IsDigit))
        {
            return Results.BadRequest(new
            {
                message = "Phone number must contain digits only."
            });
        }


        var countries = await context.Countries
            .Include(c => c.CountryDetails)
            .ToListAsync();


        var country = countries
            .Where(c => number.StartsWith(c.CountryCode))
            .OrderByDescending(c => c.CountryCode.Length)
            .FirstOrDefault();


        if (country is null)
        {
            return Results.NotFound(new
            {
                message = "Country code not found."
            });
        }


        return Results.Ok(new
        {
            number,
            country = new
            {
                countryCode = country.CountryCode,
                name = country.Name,
                countryIso = country.CountryIso,

                countryDetails = country.CountryDetails
                    .Select(detail => new
                    {
                        @operator = detail.Operator,
                        operatorCode = detail.OperatorCode
                    })
            }
        });
    })
.WithName("LookupCountry")
.WithSummary("Detect country from phone number");

app.Run();