using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Adapter{

public class LoginDTO { 
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    //Sesion
    public string Ip { get; set; } = null!;

    }
}
