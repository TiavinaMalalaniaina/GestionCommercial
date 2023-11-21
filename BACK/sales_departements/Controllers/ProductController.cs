using Microsoft.AspNetCore.Mvc;
using sales_departements.Context;
using sales_departements.Models;

namespace sales_departements.Controllers;

[Route("product")]
public class ProductController : Controller
{
    [HttpGet]
    [Route("get-all-products")]
    public  Bag GetAllProducts() {
        string? exception = null;
        object? products = null;
        try {
            SalesDepartementsContext context = new();
            products = Product.GetProducts(context);

        }
        catch(Exception ex) {
            Console.WriteLine(ex.Message);
            exception = ex.Message;
        }

        return new Bag(exception, products);
    }

    [HttpGet]
    [Route("get-all-necessary-products")]
    public  Bag GetAllProductss() {
        string? exception = null;
        object? products = null;
        try {
            SalesDepartementsContext context = new();
            products = context.VProductNecessarys.ToList();
        }
        catch(Exception ex) {
            exception = ex.Message;
        }

        return new Bag(exception, products);
    }



}

