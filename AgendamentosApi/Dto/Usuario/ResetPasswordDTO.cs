using System.ComponentModel.DataAnnotations;

namespace AgendamentosApi.Dto.Usuario;

public class ResetPasswordDTO
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string NovaSenha { get; set; } = string.Empty;

    [Compare("NovaSenha", ErrorMessage = "As senhas não coincidem.")]
    public string ConfirmarSenha { get; set; } = string.Empty;
}
