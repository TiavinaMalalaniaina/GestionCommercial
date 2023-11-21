using sales_departements.Context;

namespace sales_departements.Models;

public partial class Request
{
    public string RequestId { get; set; } = null!;

    public string? DepartmentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsValidated { get; set; }
    public string EmployeeId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual List<RequestDetail> RequestDetails { get; set;}
    // public virtual Employee Employee{ get; set; }

    public Request(string? departementId, DateTime? createdAt, string employeeId) {
        DepartmentId = departementId;
        CreatedAt = createdAt;
        EmployeeId = employeeId;
    }

    public Request() {

    }

    public string Create(SalesDepartementsContext context) {
        context.Requests.Add(this);
        context.SaveChanges();
        return this.RequestId;
    }

    public void UpdateIsValidate(SalesDepartementsContext context, string requestId) {
        var requestToUpdate = context.Requests.Find(requestId);
        if (requestToUpdate != null) {
            requestToUpdate.IsValidated = true;
        }
    }

    public List<Request> GetRequestsNoValidated(SalesDepartementsContext context) {
        List<Request> requests = context.Requests.Where(r => r.IsValidated == false).ToList();
        for (int i = 0; i < requests.Count; i++)
        {
            requests[i].RequestDetails = new RequestDetail().GetRequestDetails(context, requests[i].RequestId);
            requests[i].Department = new Department().GetDepartment(context, requests[i].DepartmentId);
            // requests[i].Employee = new Employee().GetEmployee(context, requests[i].EmployeeId);
        }
        return requests;

    }

    public List<Request> GetRequestsValidated(SalesDepartementsContext context) {
        List<Request> requests = context.Requests.Where(r => r.IsValidated == true).ToList();
        for (int i = 0; i < requests.Count; i++)
        {
            requests[i].RequestDetails = new RequestDetail().GetRequestDetails(context, requests[i].RequestId);
            requests[i].Department = new Department().GetDepartment(context, requests[i].DepartmentId);
            // requests[i].Employee = new Employee().GetEmployee(context, requests[i].EmployeeId);
        }
        return requests;

    }
}