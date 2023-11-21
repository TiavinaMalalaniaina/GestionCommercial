using System;
using System.Collections.Generic;
using sales_departements.Context;

namespace sales_departements.Models;

public partial class Person
{
    public string PersonId { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Department> Departments { get; } = new List<Department>();

    public virtual Employee? Employee { get; set; }

    public static Person GetPerson(SalesDepartementsContext context, String personId) {
        Person person = context.People.Find(personId);
        return person;
    }

    public static List<Person> GetPersons(SalesDepartementsContext context) {
        List<Person> persons = context.People.ToList();
        return persons;
    }
}
