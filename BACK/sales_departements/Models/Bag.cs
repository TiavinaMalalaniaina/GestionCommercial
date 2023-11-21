namespace sales_departements.Models;

public class Bag
{
    public string? Exception { get; set; }
    public object? Data { get; set; }

    public Bag(string? exception, object? data) {
        Exception = exception;
        Data = data;
    }
}
