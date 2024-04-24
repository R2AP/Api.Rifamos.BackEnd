namespace Api.Rifamos.BackEnd.Adapter{

public class UsuarioPasswordDTO{
    public string Email { get; set; } = null!;
    
    public string Password { get; set; } = null!;

    public string PasswordNuevo { get; set; } = null!;

    public string PasswordNuevoConfirmado { get; set; } = null!;

    public string AuditoriaUsuario { get; set; } = null!;

    }

}
