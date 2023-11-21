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
            List<Request> requests = new Request().GetRequestsNoValidated(context);
            List<object> requets1 = new (requests);
            //return Service.Serialize(requets1);
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
            List<Request> requests = new Request().GetRequestsValidated(context);
            List<object> requets1 = new (requests);
            //return Service.Serialize(requets1);
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
        Console.WriteLine("ENTRY");
        string? strings = null;
        try {
            SalesDepartementsContext context = new();

            string? departmentId = "DEP00001";
            string? employeeId = "EMP00001";

            DateTime createdAt = DateTime.Now;

            string requestId = new Request(departmentId, createdAt,employeeId).Create(context);
            Console.WriteLine(requestId + "request");

            // List<RequestDetail> requestDetails = (List<RequestDetail>)JsonConvert.DeserializeObject(requestDetailJson);
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

            string personId = new Session().GetSession();
            Console.WriteLine(personId);
            // var p = HttpContext.Session.GetString("personId");
            // Console.WriteLine("kkkk"+p+"oui");
            Employee employee = new Employee().GetEmployeeByPersonId(context, personId);
            employee.CanValidate(context);

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
