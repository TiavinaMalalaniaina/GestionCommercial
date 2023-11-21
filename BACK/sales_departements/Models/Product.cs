using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using sales_departements.Context;

namespace sales_departements.Models;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public string? ProductName { get; set; }

    // public virtual ICollection<ProformaDetail> ProformaDetails { get; } = new List<ProformaDetail>();

    // public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; } = new List<PurchaseOrderDetail>();

    // public virtual ICollection<RequestDetail> RequestDetails { get; } = new List<RequestDetail>();

    // public virtual ICollection<SupplierProduct> SupplierProducts { get; } = new List<SupplierProduct>();


    public static List<Product> GetProducts(SalesDepartementsContext context) {
        return context.Products.ToList();
    }

    public Product GetProduct(SalesDepartementsContext context, string productId) {
        return context.Products.Find(productId);
    }
}
