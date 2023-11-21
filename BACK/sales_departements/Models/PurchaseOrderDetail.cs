using System;
using System.Collections.Generic;
using sales_departements.Context;


namespace sales_departements.Models;

public partial class PurchaseOrderDetail
{
    public string PurchaseOrderDetailsId { get; set; } = null!;

    public string? PurchaseOrderId { get; set; }

    public string? ProductId { get; set; }

    public double? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual Product? Product { get; set; }

    public List<PurchaseOrderDetail> GetPurchaseOrderDetailsByPurchaseOrderId(SalesDepartementsContext context, string PurchaseOrderId) {
        List<PurchaseOrderDetail> purchaseOrderDetails = context.PurchaseOrderDetails.Where(p => p.PurchaseOrderId == PurchaseOrderId).ToList();
        for (var i = 0; i < purchaseOrderDetails.Count; i++)
        {
            purchaseOrderDetails[i].Product = new Product().GetProduct(context, purchaseOrderDetails[i].ProductId);
        }
        return purchaseOrderDetails;
    }
}
