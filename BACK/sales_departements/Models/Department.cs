using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using sales_departements.Context;

namespace sales_departements.Models;

public partial class Department
{
    public string DepartmentId { get; set; } = null!;

    public string? DepartmentName { get; set; }

    // public string? DepartmentHeadId { get; set; }

    // public virtual Employee? DepartmentHead { get; set; }

    // public virtual ICollection<Employee> Employees { get; set; }

    // public virtual ICollection<Request> Requests { get; } = new List<Request>();

    public List<Department> GetDepartments(SalesDepartementsContext context) {
        List<Department> departments = context.Departments.ToList();
        return departments;
    }

    public Department GetDepartment(SalesDepartementsContext context, string departementId) {
        Department department = context.Departments.Find(departementId);
        return department;
    }


}
