using Microsoft.AspNetCore.Mvc;
using Npgsql;
using sales_departements.Context;
using sales_departements.Models;

namespace sales_departements.Controllers;

[Route("request-detail")]
public class RequestDetailController : Controller
{

    [HttpGet]
    [Route("get-all")]
    public Bag GetAll() {
        string? exception = null;
        object? data = null;
        NpgsqlConnection connection = null;
        try
        {
            connection = new Connection().Connect();
            data = new RequestDetail().GetRequestDetailsGroupByProductAndRequest(connection);
        }
        catch (Exception ex)
        {
            exception = ex.Message;
        }
        finally {
            if (connection != null)
            {
                connection.Close();
            }
        }

        return new Bag(exception, data);
    }
}
