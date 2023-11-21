using System;
using System.Collections.Generic;
using sales_departements.Context;

namespace sales_departements.Models;

public partial class VProforma
{
    public string? ProformaDetailsId {get; set;}
    public string? ProformaId {get; set;}
    public string? ProductId {get; set;}
    public int Quantity {get; set;}
    public decimal? Price {get; set;}
    public string? SupplierId {get; set;}
}
