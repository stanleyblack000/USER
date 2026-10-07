namespace CountryLookupApi.Models;

public class CountryDetail
{
    public int Id { get; set; }

    public int CountryId { get; set; }

    public string Operator { get; set; } = string.Empty;

    public string OperatorCode { get; set; } = string.Empty;

    public Country Country { get; set; } = null!;
}