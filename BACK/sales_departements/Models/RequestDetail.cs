using System;
using System.Collections.Generic;
using Npgsql;
using sales_departements.Context;

namespace sales_departements.Models;

public partial class RequestDetail
{
    public string RequestDetailsId { get; set; } = null!;

    public string? RequestId { get; set; }

    public string? ProductId { get; set; }

    public int? Quantity { get; set; }
    public string? Reason { get; set; }

    public int Treated {get; set;}

    public virtual Product? Product { get; set; }
    public bool IsValidated { get; set; }
    public virtual string? DepartmentName { get; set; }

    // public virtual Request? Request { get; set; }

    public RequestDetail() {

    }
    public RequestDetail(string productId, int quantity, string reason) {
        ProductId = productId;
        Quantity = quantity;
        Reason = reason;
    }

    public void Create(SalesDepartementsContext context) {
        context.RequestDetails.Add(this);
    }

    /*insert many product for a request */
    public void Creates(SalesDepartementsContext context, List<RequestDetail> requestDetails, string requestId) {
        foreach (RequestDetail item in requestDetails)
        {
            item.RequestId = requestId;
            item.Create(context);
        }
    }

    public List<RequestDetail> GetRequestDetails(SalesDepartementsContext context, string requestId) {
        List<RequestDetail> requestDetails = context.RequestDetails.Where(r => r.RequestId == requestId).ToList();
        for (int i = 0; i < requestDetails.Count; i++)
        {
            requestDetails[i].Product = new Product().GetProduct(context, requestDetails[i].ProductId);
        }
        return requestDetails;
    }

    public List<RequestDetail> GetRequestDetailsValidated(SalesDepartementsContext context, string requestId) {
        List<RequestDetail> requestDetails = context.RequestDetails.Where(r => r.RequestId == requestId && r.IsValidated).ToList();
        for (int i = 0; i < requestDetails.Count; i++)
        {
            requestDetails[i].Product = new Product().GetProduct(context, requestDetails[i].ProductId);
        }
        return requestDetails;
    }
    public void UpdateIsValidate(SalesDepartementsContext context, string requestDetailId) {
        var requestDetailToUpdate = context.RequestDetails.Find(requestDetailId);
        requestDetailToUpdate.Treated = -1;
        if (requestDetailToUpdate != null) {
            requestDetailToUpdate.IsValidated = true;
            requestDetailToUpdate.Treated = 1;
        }
    }

    public void UpdateIsValidates(SalesDepartementsContext context, string[] requestDetailIdS) {
        foreach (string requestDetailId in requestDetailIdS)
        {
            UpdateIsValidate(context, requestDetailId);
        }
    }

    public List<RequestDetail> GetRequestDetailsGroupByProductAndRequest(NpgsqlConnection connection) {
        List<RequestDetail> requestDetails = new ();

        SalesDepartementsContext context = new ();

        string sql = "SELECT sum(quantity) as total, red.request_id as req, red.product_id as pro FROM request_details as red"+
                    " JOIN request as r ON r.request_id = red.request_id "+
                    " GROUP BY red.request_id, red.product_id";

        NpgsqlCommand command = new NpgsqlCommand(sql, connection);
        NpgsqlDataReader dr = command.ExecuteReader();
        while (dr.Read())
        {
            RequestDetail requestDetail = new ();
            requestDetail.Quantity = Convert.ToInt32(dr["total"]);
            requestDetail.RequestId = dr["req"].ToString();
            requestDetail.ProductId = dr["pro"].ToString();
            requestDetail.Product = new Product().GetProduct(context, requestDetail.ProductId);
            requestDetail.DepartmentName = new RequestDetail().GetDepartmentNameByRequestId(requestDetail.RequestId);
            requestDetails.Add(requestDetail);
        }
        return requestDetails;
    }

    public string GetDepartmentNameByRequestId(string requestId) {
        NpgsqlConnection connection = new Connection().Connect();
        string sql = "SELECT department_name FROM request r "+
                    " JOIN department d ON r.department_id = d.department_id"+
                    " WHERE r.request_id = '"+requestId+"'";
        NpgsqlCommand command = new (sql, connection);
        NpgsqlDataReader dr = command.ExecuteReader();
        if(dr.Read()) {
            return dr["department_name"].ToString();
        }
        return null;
    }

    public void ValidateProductInRequestDetail(SalesDepartementsContext context, string productId) {
        List<RequestDetail> requestDetails = context.RequestDetails.Where(r => r.ProductId == productId && r.IsValidated).ToList();

        foreach (RequestDetail requestDetail in requestDetails)
        {
            requestDetail.Treated = 2;
        }
    }
}
