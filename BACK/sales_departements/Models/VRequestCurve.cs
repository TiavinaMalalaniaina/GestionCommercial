namespace sales_departements.Models;

public partial class VRequestCurve
{
    public string? DepartmentId { get; set; }
    public string? ProductId { get; set; }
    public string? DepartmentName { get; set; }
    public string? ProductName { get; set; }
    public string? Month { get; set; }
    public int TotalQuantity { get; set; }
}