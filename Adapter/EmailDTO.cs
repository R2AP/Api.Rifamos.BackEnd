using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Adapter{

public class EmailDTO{
    public string EmailFrom { get; set; } = null!;

    public string EmailTo { get; set; } = null!;

    public string EmailPassword { get; set; } = null!;

    public string EmailSubject { get; set; } = null!;

    public string EmailBody { get; set; } = null!;

    }
}
