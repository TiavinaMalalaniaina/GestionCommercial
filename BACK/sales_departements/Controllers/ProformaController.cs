using Microsoft.AspNetCore.Mvc;
using sales_departements.Context;
using sales_departements.Models;

namespace sales_departements.Controllers;

[Route("proforma")]
public class ProformaController : Controller
{

    [HttpPost]
    [Route("create-purchase-order")]
    public Bag Test([FromBody] ListString productIds) {
        string? exception = null;
        object? data = null;
        Dictionary<string, List<VProforma>> dico = new Dictionary<string, List<VProforma>>();
        try {
            SalesDepartementsContext context = new ();
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<VProforma> vProformas = context.VProformas.Where(p => productIds.ProductIds.Contains(p.ProductId)).ToList();
                    foreach (var item in vProformas)
                    {
                        if (dico.ContainsKey(item.SupplierId)) {
                            List<VProforma> v = dico[item.SupplierId];
                            v.Add(item);
                        } else {
                            List<VProforma> v = new();
                            v.Add(item);
                            dico.Add(item.SupplierId, v);
                        }
                    }
                    var items = dico.Values;
                    foreach (List<VProforma> vProformaPluriel in items)
                    {
                        string supplierId = vProformaPluriel[0].SupplierId;
                        PurchaseOrder purchaseOrder = new PurchaseOrder{
                            SupplierId = supplierId,
                        };
                        context.PurchaseOrders.Add(purchaseOrder);
                        Console.WriteLine(purchaseOrder.PurchaseOrderId);
                        context.SaveChanges();
                        foreach (VProforma vProforma in vProformaPluriel)
                        {
                            PurchaseOrderDetail purchaseOrderDetail = new PurchaseOrderDetail{
                                PurchaseOrderId = purchaseOrder.PurchaseOrderId,
                                ProductId = vProforma.ProductId,
                                Quantity = vProforma.Quantity,
                                Price = vProforma.Price,
                            };
                            context.PurchaseOrderDetails.Add(purchaseOrderDetail);
                        }
                        context.SaveChanges();
                    }
                    context.SaveChanges();
                    transaction.Commit();
                    Console.WriteLine("DONE");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    transaction.Rollback();
                }
            }
            
            context.SaveChanges();
        } catch (Exception ex) {
            Console.WriteLine(ex);
        }
        return new Bag(exception, data);
    }




    [HttpPost]
    [Route("create")]
    public Bag Create([FromBody] Proforma proforma) {
        string exception = null;
        Console.WriteLine(proforma);
        SalesDepartementsContext context = new SalesDepartementsContext();
        try {
            context.Proformas.Add(proforma);
            context.SaveChanges();

            foreach (ProformaDetail item in proforma.ProformaDetails) {
                Console.WriteLine(item);
                item.ProformaId = proforma.ProformaId;
                context.ProformaDetails.Add(item);
            }

            context.SaveChanges();
            // transaction.Commit();
        } catch (Exception ex) {
            // context.RollBack();
            Console.WriteLine(ex);
            exception = ex.Message;
        }

        Bag bag = new Bag(exception, null);
        return bag;
    }


    [HttpGet]
    [Route("get-all-proformas")]
    public Bag GetAllProforma() {
        string? exception = null;
        object? data = null;
        try
        {
            SalesDepartementsContext context = new ();
            List<Proforma> proformas = new Proforma().GetAllProformas(context);
            data = proformas;
        }
        catch (Exception e)
        {
            exception = e.Message;
        }

        return new Bag(exception, data);
    }
}
