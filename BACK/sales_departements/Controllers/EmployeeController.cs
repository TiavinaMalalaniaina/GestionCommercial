using Microsoft.AspNetCore.Mvc;
using sales_departements.Context;
using sales_departements.Models;

namespace sales_departements.Controllers;

[Route("employee")]
public class EmployeeController : Controller
{
    [HttpGet]
    [Route("log-in")]
    public Bag LogIn(string email, string password) {
        string strings = null;
        object? data = null;
        try
        {
            SalesDepartementsContext context = new ();
            Employee employee = new Employee().GetEmployeeByEmailAndPassword(context, email, password);
            new Session().SetSession(employee.EmployeeId);
            data = employee;
            //List<object> departmentObjects = new(departments);
            //return Service.Serialize(departmentObjects);
        }
        catch (Exception e)
        {
            strings = e.Message;
        }

        Console.WriteLine(strings);
        return new Bag(strings, data);
    }
}
