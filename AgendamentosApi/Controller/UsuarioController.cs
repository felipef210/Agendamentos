using AgendamentosApi.Dto.Usuario;
using AgendamentosApi.Services.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgendamentosApi.Controller;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;


    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;

    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<List<UsuarioDTO>>> ListarUsuarios()
    {
        var usuarios = await _usuarioService.ListarUsuarios();
        return Ok(usuarios);
    }

    [HttpGet("exibirusuario")]
    public async Task<ActionResult<UsuarioDTO>> BuscarUsuarioPorId()
    {
        var usuario = await _usuarioService.BuscarUsuarioPorId();
        return Ok(usuario);
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> Cadastrar([FromBody] CadastroDTO cadastroDTO)
    {
        await _usuarioService.Cadastrar(cadastroDTO);
        return Ok(new { message = "Cadastro efetuado com sucesso!" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var token = await _usuarioService.Logar(loginDTO);
        return Ok(token);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] EsqueceuSenhaDTO dto)
    {
        await _usuarioService.ForgotPasswordAsync(dto);
        return Ok(new { message = "E-mail de recuperação enviado com sucesso!" });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
    {
        await _usuarioService.ResetPasswordAsync(dto);
        return Ok(new { message = "Senha redefinida com sucesso!" });
    }

    [HttpPut]
    public async Task<IActionResult> EditarUsuario([FromBody] EditarPerfilDTO editarPerfilDTO)
    {
        var usuarioAtualizado = await _usuarioService.EditarUsuario(editarPerfilDTO);
        return Ok(usuarioAtualizado);
    }
}
