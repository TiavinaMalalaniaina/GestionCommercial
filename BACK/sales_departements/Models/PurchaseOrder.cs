using System;
using System.Collections.Generic;
using sales_departements.Context;

namespace sales_departements.Models;

public partial class PurchaseOrder
{
    public string PurchaseOrderId { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public int? DeliveryDays { get; set; }

    public string? SupplierId { get; set; }

    public int? Validation { get; set; }

    public virtual Supplier? Supplier { get; set; }

    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails {get; set;}

    public void ValidatePurchaseOrderAndTheirProducts(SalesDepartementsContext context, string purchaseOrderId) {
        PurchaseOrder purchaseOrderToUpdate = context.PurchaseOrders.Find(purchaseOrderId);
        if (purchaseOrderToUpdate != null) {
            purchaseOrderToUpdate.Validation = 20;
        }
        List<PurchaseOrderDetail> purchaseOrderDetails = new PurchaseOrderDetail().GetPurchaseOrderDetailsByPurchaseOrderId(context, purchaseOrderId);
        foreach (PurchaseOrderDetail purchaseOrderDetail in purchaseOrderDetails)
        {
            new RequestDetail().ValidateProductInRequestDetail(context, purchaseOrderDetail.ProductId);
        }
    }


    public List<PurchaseOrder> GetPurchaseOrdersValidated(SalesDepartementsContext context) {
        List<PurchaseOrder> purchaseOrders = context.PurchaseOrders.Where(p => p.Validation == 20).ToList();
        for (var i = 0; i < purchaseOrders.Count; i++)
        {
            purchaseOrders[i].Supplier = new Supplier().GetSupplier(context, purchaseOrders[i].SupplierId);
            purchaseOrders[i].PurchaseOrderDetails = new PurchaseOrderDetail().GetPurchaseOrderDetailsByPurchaseOrderId(context, purchaseOrders[i].PurchaseOrderId);
        }
        return purchaseOrders;
    }

    public List<PurchaseOrder> GetPurchaseOrdersNoValidated(SalesDepartementsContext context) {
        List<PurchaseOrder> purchaseOrders = context.PurchaseOrders.Where(p => p.Validation == 10).ToList();
        for (var i = 0; i < purchaseOrders.Count; i++)
        {
            purchaseOrders[i].Supplier = new Supplier().GetSupplier(context, purchaseOrders[i].SupplierId);
            purchaseOrders[i].PurchaseOrderDetails = new PurchaseOrderDetail().GetPurchaseOrderDetailsByPurchaseOrderId(context, purchaseOrders[i].PurchaseOrderId);
        }
        return purchaseOrders;
    }
}
