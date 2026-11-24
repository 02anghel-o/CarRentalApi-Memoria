namespace CarRental.Api.Dtos;

public record RegistroDto(string Nombre, string Correo, string Contrasena);
public record LoginDto(string Correo, string Contrasena);
public record UsuarioTablaDto(int Id, string Nombre, string Correo);