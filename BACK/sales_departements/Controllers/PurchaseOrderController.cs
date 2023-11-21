using Microsoft.AspNetCore.Mvc;
using sales_departements.Context;
using sales_departements.Models;

namespace sales_departements.Controllers;

[Route("purchase-order")]
public class PurchaseOrderController : Controller
{
    [HttpGet]
    [Route("validate")]
    public Bag Validate( string purchase_order_id) {
        string? exception = null;
        object? data = null;
        try
        {
            SalesDepartementsContext context = new ();
            new PurchaseOrder().ValidatePurchaseOrderAndTheirProducts(context, purchase_order_id);
            context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            exception = e.Message;
        }

        return new Bag(exception, data);
    }


    [HttpGet]
    [Route("get-all")]
    public Bag getAll() {
        string exception = null;
        List<PurchaseOrder> models = null;
        SalesDepartementsContext context = new SalesDepartementsContext();
        try {
            models = context.PurchaseOrders
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
            foreach (PurchaseOrder item in models)
            {
                item.PurchaseOrderDetails = context.PurchaseOrderDetails
                    .Where(p => p.PurchaseOrderId == item.PurchaseOrderId)
                    .ToList();
                foreach (PurchaseOrderDetail item2 in item.PurchaseOrderDetails)
                {
                    item2.Product = context.Products
                        .Where(p => p.ProductId==item2.ProductId)
                        .FirstOrDefault();
                }
            }

        } catch (Exception ex) {
            Console.WriteLine(ex);
            exception = ex.Message;
        }

        Bag bag = new Bag(exception, models);
        return bag;
    }

    [HttpGet]
    [Route("get-all-validated")]
    public Bag GetAllValidated() {
        string? exception = null;
        object? data = null;
        try
        {
            SalesDepartementsContext context = new ();
            data = new PurchaseOrder().GetPurchaseOrdersValidated(context);
        }
        catch (Exception e)
        {
            exception = e.Message;            
        }
        return new Bag(exception, data);
    }

    [HttpGet]
    [Route("get-all-no-validated")]
    public Bag GetAllNoValidated() {
        string? exception = null;
        object? data = null;
        try
        {
            SalesDepartementsContext context = new ();
            data = new PurchaseOrder().GetPurchaseOrdersNoValidated(context);
        }
        catch (Exception e)
        {
            exception = e.Message;            
        }
        return new Bag(exception, data);
    }


}
