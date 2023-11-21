using System;
using System.Collections.Generic;
using sales_departements.Context;

namespace sales_departements.Models;

public partial class ProformaDetail
{
    public string ProformaDetailsId { get; set; } = null!;

    public string? ProformaId { get; set; }

    public string? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual Product? Product { get; set; }

    // public virtual Proforma? Proforma { get; set; }

    public override string ToString() {
        return $"ProformaId:{ProformaId}";
    }

    public void Create(SalesDepartementsContext context) {
        context.ProformaDetails.Add(this);
    }

    public void Creates(SalesDepartementsContext context, List<ProformaDetail> proformaDetails, string proformaId) {
        foreach (ProformaDetail proformaDetail in proformaDetails)
        {
            proformaDetail.ProformaId = proformaId;
            proformaDetail.Create(context);
        }
    }

    public List<ProformaDetail> GetProformaDetailsByProformaId(SalesDepartementsContext context, string proformaId) {
        List<ProformaDetail> p = context.ProformaDetails
            .Where(p => p.ProformaId == proformaId)
            .ToList();
        foreach (ProformaDetail item in p)
        {
            item.Product = context.Products.Where(p => p.ProductId == item.ProductId).FirstOrDefault();
        }
        return p;
    }
}
