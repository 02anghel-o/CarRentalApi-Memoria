using CarRental.Api.Dtos;
using CarRental.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CuentasController : ControllerBase
{
    private readonly UsuarioMemoriaService _servicio;

    public CuentasController(UsuarioMemoriaService servicio)
    {
        _servicio = servicio;
    }

    [HttpPost("registro")]
    public IActionResult Registrar([FromBody] RegistroDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Correo) || string.IsNullOrWhiteSpace(dto.Contrasena))
            return BadRequest("El correo y la contraseña son obligatorios.");

        if (_servicio.BuscarPorCorreo(dto.Correo) is not null)
            return Conflict("Ya existe un usuario registrado con ese correo.");

        var creado = _servicio.Registrar(dto.Nombre, dto.Correo, dto.Contrasena);
        var respuesta = new UsuarioTablaDto(creado.Id, creado.Nombre, creado.Correo);

        return CreatedAtAction(nameof(ObtenerTablaUsuarios), new { id = creado.Id }, respuesta);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        var usuario = _servicio.BuscarPorCorreo(dto.Correo);

        if (usuario is null || usuario.Contrasena != dto.Contrasena)
            return Unauthorized("Correo o contraseña incorrectos.");

        return Ok(new
        {
            mensaje = "Inicio de sesión exitoso",
            usuario = new UsuarioTablaDto(usuario.Id, usuario.Nombre, usuario.Correo)
        });
    }

    [HttpGet("usuarios")]
    public ActionResult<IEnumerable<UsuarioTablaDto>> ObtenerTablaUsuarios()
    {
        var tabla = _servicio.ObtenerTodos()
            .Select(u => new UsuarioTablaDto(u.Id, u.Nombre, u.Correo));

        return Ok(tabla);
    }
}