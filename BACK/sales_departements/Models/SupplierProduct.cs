using System;
using System.Collections.Generic;
using sales_departements.Context;

namespace sales_departements.Models;

public partial class SupplierProduct
{
    public string SupplierProductId { get; set; } = null!;

    public string? SupplierId { get; set; }

    public string? ProductId { get; set; }

    public virtual Product? Product { get; set; }

    // public virtual Supplier? Supplier { get; set; }

    public List<SupplierProduct> GetSupplierProduct(SalesDepartementsContext context, string supplierId) {
        List<SupplierProduct> suppliers = context.SupplierProducts.Where(s => s.SupplierId == supplierId).ToList();
        for (int i = 0; i < suppliers.Count; i++)
        {
            suppliers[i].Product = new Product().GetProduct(context, suppliers[i].ProductId);
        }
        return suppliers;
    }
}

