using System;
using System.Collections.Generic;
using sales_departements.Context;

namespace sales_departements.Models;

public partial class VProductNecessary
{
    public string ProductId {get; set;}
    public string ProductName {get; set;}
    public int Quantity {get; set;}
}
