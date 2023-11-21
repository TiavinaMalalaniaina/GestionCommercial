using System;
using System.Collections.Generic;
using sales_departements.Context;

namespace sales_departements.Models.Display;

public partial class RequestModel
{
    public string RequestId {get; set;}
    public string[] RequestDetailsId {get; set;}
}
