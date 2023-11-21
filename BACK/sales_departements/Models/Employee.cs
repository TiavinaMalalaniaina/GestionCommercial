using System;
using System.Collections.Generic;
using sales_departements.Context;

namespace sales_departements.Models;

public partial class Employee
{
    public string EmployeeId { get; set; } = null!;

    public string? PersonId { get; set; }

    public string? DepartmentId { get; set; }
    public string? DepartmentHeadId { get; set; }
    public DateOnly? HireDate { get; set; }

    public string? JobTitle { get; set; }

    public decimal? Salary { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public Boolean Daf { get; set; }

    public virtual Department? Department { get; set; }
    public virtual Department? DepartmentHead { get; set; }

    public virtual Person? Person { get; set; }

    public Employee GetEmployeeById(SalesDepartementsContext context, string employeeId) {
        return context.Employees.Find(employeeId);
    }
    public List<Employee> GetEmployees(SalesDepartementsContext context) {
        return context.Employees.ToList();
    }

    public Employee GetEmployeeByEmailAndPassword(SalesDepartementsContext context, string email, string password) {
        List<Employee> employees = GetEmployees(context);
        Employee employeetWithEmail = null;
        foreach (Employee employee in employees)
        {
            if (email.Equals(employee.Email)) {
                employeetWithEmail = employee;
                if (password.Equals(employee.Password)) {
                    employee.Department = new Department().GetDepartment(context, employee.DepartmentId);
                    employee.Person = Person.GetPerson(context, employee.PersonId);
                    employee.DepartmentHead = new Department().GetDepartment(context, employee.DepartmentHeadId);
                    return employee;
                }

            }
        }

        if (employeetWithEmail == null) throw new Exception("Your email is not still saved");
        throw new Exception("Your password is incorrcet");
    }

    public int GetEmployeeType() {

        if(Daf)
            return 2;
        if(!string.IsNullOrEmpty(DepartmentHeadId)){
            return 1;
        }

        return 0;
    }

    public Dictionary<int, string> GetEmployeeTypeName() {
        Dictionary<int, string> keyValuePairs= new();
        keyValuePairs.Add(0,"employee");
        keyValuePairs.Add(1,"chef de departement");
        keyValuePairs.Add(2,"Directeur administratif financier");

        return keyValuePairs;
    }

    public void CanValidateRequest() {
        int type = GetEmployeeType();
        Dictionary<int, string> keyValuePairs = GetEmployeeTypeName();
        if (type < 1)
            throw new Exception("Le "+keyValuePairs[type]+" ne peut pas le valider");
    }

    public void CanValidatePurcahaseOrder() {
        int type = GetEmployeeType();
        Dictionary<int, string> keyValuePairs = GetEmployeeTypeName();
        if (type < 2)
            throw new Exception("Le "+keyValuePairs[type]+" ne peut pas le valider");
    }


    public Employee GetEmployee(SalesDepartementsContext context, string employeeId) {
        return context.Employees.Find(employeeId);
    }

}
