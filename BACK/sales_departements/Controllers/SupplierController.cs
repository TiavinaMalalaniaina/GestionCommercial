using Microsoft.AspNetCore.Mvc;
using sales_departements.Context;
using sales_departements.Models;

namespace sales_departements.Controllers;

[Route("supplier")]
public class SupplierController
{

    [HttpGet]
    [Route("get-all-suppliers")]
    public Bag GetAllSupplierWihtProducts() {
        string? exception = null;
        object? data = null;
        try
        {
            SalesDepartementsContext context = new ();
            data = new Supplier().GetSuppliers(context);
        }
        catch (Exception e)
        {
            exception = e.Message;
        }

        return new Bag(exception, data);
    }
}

