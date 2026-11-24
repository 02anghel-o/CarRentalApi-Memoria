using CarRental.Api.Models;

namespace CarRental.Api.Services;

public class UsuarioMemoriaService
{
    private readonly List<Usuario> _usuarios = new()
    {
        new Usuario { Id = 1, Nombre = "Admin", Correo = "admin@correo.com", Contrasena = "123" }
    };

    private int _siguienteId = 2;

    public IEnumerable<Usuario> ObtenerTodos() => _usuarios;

    public Usuario? BuscarPorCorreo(string correo) =>
        _usuarios.FirstOrDefault(u => u.Correo.Equals(correo, StringComparison.OrdinalIgnoreCase));

    public Usuario Registrar(string nombre, string correo, string contrasena)
    {
        var nuevo = new Usuario
        {
            Id = _siguienteId++,
            Nombre = nombre,
            Correo = correo,
            Contrasena = contrasena
        };

        _usuarios.Add(nuevo);
        return nuevo;
    }
}