using Microsoft.AspNetCore.Mvc;
using sales_departements.Models;
using sales_departements.Models.Display;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using sales_departements.Context;
using System.Text.Json;
using System.Text.Json.Serialization;



namespace sales_departements.Controllers;

[Route("request")]
public class RequestController : Controller
{
    [HttpGet]
    [Route("get-all-requests-no-validated")]
    public Bag GetAllRequestsNoValidated() {
        string? exception = null;
        object? data = null;
        try
        {
            SalesDepartementsContext context = new ();

            string employeeId = new Session().GetSession();
            List<Request> requests = new Request().GetRequestsNoValidatedByDepartement(context, employeeId);
            data = requests;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            exception = e.Message;
        }

        return new Bag(exception, data);
    }

    [HttpGet]
    [Route("get-all-requests-validated")]
    public Bag GetAllRequestsValidated() {
        string? exception = null;
        object? data = null;
        try
        {
            SalesDepartementsContext context = new ();

            string employeeId = new Session().GetSession();
            List<Request> requests = new Request().GetRequestsValidatedByDepartement(context, employeeId);
            data = requests;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            exception = e.Message;
        }

        return new Bag(exception, data);
    }

    [HttpGet]
    [Route("get-all-requests-send-by-self")]
    public Bag GetAllRequests() {
        string? exception = null;
        object? data = null;
        try
        {
            SalesDepartementsContext context = new ();

            string employeeId = new Session().GetSession();
            List<Request> requests = new Request().GetRequestsSendBySelf(context, "EMP00001");
            data = requests;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            exception = e.Message;
        }

        return new Bag(exception, data);
    }

    [HttpPost]
    [Route("create")]
    public string? Create([FromBody] List<RequestDetail> requestDetails) {
        string? strings = null;
        try {
            SalesDepartementsContext context = new();

            string? employeeId = new Session().GetSession();
            Employee employee = new Employee().GetEmployee(context, employeeId);
            string? departmentId = employee.DepartmentId;


            DateTime createdAt = DateTime.Now;

            string requestId = new Request(departmentId, createdAt,employeeId).Create(context);

            new RequestDetail().Creates(context, requestDetails, requestId);
            Console.WriteLine(requestId);
            context.SaveChanges();

        }
        catch(Exception ex) {
            Console.WriteLine(ex);
            strings = ex.Message;
        }
        return strings;
    }

    [HttpPost]
    [Route("validate")]
    public Bag Validate([FromBody] RequestModel model) {
        string? exception = null;
        object? data = null;
        try {
            SalesDepartementsContext context = new();

            string employeeId = new Session().GetSession();
            Employee employee = new Employee().GetEmployeeById(context, employeeId);
            employee.CanValidateRequest();
            new Request().UpdateIsValidate(context, model.RequestId);
            new RequestDetail().UpdateIsValidates(context, model.RequestDetailsId);

            context.SaveChanges();
        }
        catch(Exception ex) {
            Console.WriteLine(ex);
            exception = ex.Message;
        }
        return new Bag(exception, data);
    }
 
    [HttpGet]
    [Route("getAllByProduct")]
    public string GetAllByProductsAndDepartment()
    {
        SalesDepartementsContext context = new SalesDepartementsContext();
        var allData = context.VRequestCurves.ToList();

        Dictionary<string, List<Dictionary<string, object>>> productsByProduct = new Dictionary<string, List<Dictionary<string, object>>>();

        foreach (var item in allData)
        {
            if (!productsByProduct.ContainsKey(item.ProductId))
            {
                productsByProduct[item.ProductId] = new List<Dictionary<string, object>>();
            }

            var productData = productsByProduct[item.ProductId].FirstOrDefault(p => p.ContainsKey("department") && p["department"].Equals(item.DepartmentId));

            if (productData != null)
            {
                if (productData.ContainsKey("data") && productData["data"] is List<Dictionary<string, object>> dataList)
                {
                    var data = new Dictionary<string, object>
                    {
                        { "quantite", item.TotalQuantity },
                        { "mois", item.Month }
                    };

                    dataList.Add(data);
                }
            }
            else
            {
                var newProductData = new Dictionary<string, object>
                {
                    { "department", item.DepartmentId },
                    { "data", new List<Dictionary<string, object>> {
                        new Dictionary<string, object>
                        {
                            { "quantite", item.TotalQuantity },
                            { "mois", item.Month }
                        }
                    }}
                };

                productsByProduct[item.ProductId].Add(newProductData);
            }
        }

        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        return JsonConvert.SerializeObject(productsByProduct, settings);
    }

    // [HttpGet]
    // [Route("add-in-request-detail")]
    // public void AddInRequestDetail(string product_id, int quantity, string reason) {
    //     string requestDetailJson = HttpContext.Session.GetString("requestDetailJson");

    //     RequestDetail requestDetail = new (product_id, quantity, reason);
    //     string newRequestDetailJson = JsonConvert.SerializeObject(requestDetail);

    //     if(string.IsNullOrEmpty(requestDetailJson)) {
    //         HttpContext.Session.SetString("requestDetailJson", newRequestDetailJson);
    //     }
    //     else {
    //         List<RequestDetail> requestDetails =  JsonConvert.DeserializeObject<List<RequestDetail>>(requestDetailJson);
    //         requestDetails.Add(requestDetail);
    //         newRequestDetailJson = JsonConvert.SerializeObject(requestDetails);
    //         HttpContext.Session.SetString("requestDetailJson", newRequestDetailJson);
    //     }
    // }

}
