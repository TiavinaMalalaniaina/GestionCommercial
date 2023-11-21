using System;
using System.Collections.Generic;
using sales_departements.Context;

namespace sales_departements.Models;

public partial class Supplier
{
    public string SupplierId { get; set; } = null!;

    public string? Name { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public string? Address { get; set; }

    // public virtual ICollection<Proforma> Proformas { get; } = new List<Proforma>();

    // public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set;}

    public virtual ICollection<SupplierProduct> SupplierProducts { get; set;}

    public Supplier GetSupplier(SalesDepartementsContext context, string supplierId) {
        return context.Suppliers.Find(supplierId);
    }

    public List<Supplier> GetSuppliers(SalesDepartementsContext context) {
        List<Supplier> suppliers = context.Suppliers.ToList();
        for (int i = 0; i < suppliers.Count; i++)
        {
            suppliers[i].SupplierProducts = new SupplierProduct().GetSupplierProduct(context, suppliers[i].SupplierId);
        }

        return suppliers;
    }
}

