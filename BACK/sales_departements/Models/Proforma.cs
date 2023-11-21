using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using sales_departements.Context;

namespace sales_departements.Models;

public partial class Proforma
{
    public string ProformaId { get; set; } = null!;

    public DateOnly? IssueDate { get; set; }

    public DateOnly? DueDate { get; set; }

    public string? SupplierId { get; set; }

    public virtual ICollection<ProformaDetail> ProformaDetails { get; set; }
    public virtual Supplier? Supplier { get; set; }

    public Proforma(DateOnly issueDate, DateOnly dueDate, string supplierId) {
        IssueDate = issueDate;
        DueDate = dueDate;
        SupplierId = supplierId;
    }

    public Proforma() {

    }

    public override string ToString() {
        return $"ProformaId:{ProformaId};;IssueDate:{IssueDate};;DueDate:{DueDate};;SupplierId:{SupplierId};;CountProformaDetails:{ProformaDetails.Count}";
    }

    public String Create(SalesDepartementsContext context) {
        context.Proformas.Add(this);
        context.SaveChanges();
        return this.ProformaId;
    }

    public List<Proforma> GetAllProformas(SalesDepartementsContext context) {
        List<Proforma> proformas = context.Proformas.Include(p => p.ProformaDetails).Include(p => p.Supplier).ToList();
        for (int i = 0; i < proformas.Count; i++)
        {
            proformas[i].ProformaDetails = new ProformaDetail().GetProformaDetailsByProformaId(context, proformas[i].ProformaId);
            proformas[i].Supplier = new Supplier().GetSupplier(context, proformas[i].SupplierId);
        }
        return proformas;
    }
}
