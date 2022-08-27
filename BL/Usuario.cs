using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.UserName}', '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}', '{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.FechaNacimiento}', '{usuario.Curp}', '{usuario.Imagen}', {usuario.Rol.IdRol}, '{usuario.Password}' ,'{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo registrar al Usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario},'{usuario.UserName}', '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}', '{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.FechaNacimiento}' , '{usuario.Curp}', '{usuario.Imagen}' , {usuario.Rol.IdRol}, '{usuario.Estatus}', '{usuario.Password}', '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el Usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result Delete(int IdDireccion, int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioDelete {IdDireccion}, {IdUsuario}");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se eliminó al Usuario";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
                    usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
                    usuario.UserName = (usuario.UserName == null) ? "" : usuario.UserName;
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.UserName}' ").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            usuario = new ML.Usuario();
                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.UserName = obj.UserName;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Email = obj.Email;
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.FechaNacimiento = obj.FechaNacimiento?.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            usuario.Curp = obj.Curp;
                            usuario.Imagen = obj.Imagen;
                            usuario.Estatus = (bool)obj.Estatus;
                            usuario.Password = obj.Password;

                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = obj.IdRol.Value;
                            usuario.Rol.Nombre = obj.RolNombre;

                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = obj.IdDireccion.Value;
                            usuario.Direccion.Calle = obj.DireccionCalle;
                            usuario.Direccion.NumeroInterior = obj.DireccionNumeroInterior;
                            usuario.Direccion.NumeroExterior = obj.DireccionNumeroExterior;
                            
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.Nombre = obj.NombreColonia;
                            usuario.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;

                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;

                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;

                            result.Objects.Add(usuario);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.UserName = query.UserName;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.FechaNacimiento = query.FechaNacimiento?.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                        usuario.Curp = query.Curp;
                        usuario.Imagen = query.Imagen;
                        usuario.Estatus = (bool)query.Estatus;
                        usuario.Password = query.Password;

                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = query.IdRol.Value;
                        usuario.Rol.Nombre = query.RolNombre;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = query.IdDireccion.Value;
                        usuario.Direccion.Calle = query.DireccionCalle;
                        usuario.Direccion.NumeroInterior = query.DireccionNumeroInterior;
                        usuario.Direccion.NumeroExterior = query.DireccionNumeroExterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = query.IdColonia.Value;
                        usuario.Direccion.Colonia.Nombre = query.NombreColonia;
                        usuario.Direccion.Colonia.CodigoPostal = query.CodigoPostal;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio.Value;
                        usuario.Direccion.Colonia.Municipio.Nombre = query.NombreMunicipio;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado.Value;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = query.NombreEstado;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = query.IdPais.Value;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = query.NombrePais;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al consultar al Usuario";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result GetByEmail(string email)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LRomanProgramacionNCapasContext context = new DL.LRomanProgramacionNCapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetByEmail '{email}'").AsEnumerable().FirstOrDefault();

                    if(query != null)
                    {
                        ML.Usuario usuario1 = new ML.Usuario();

                        usuario1.IdUsuario = query.IdUsuario;
                        usuario1.UserName = query.UserName;
                        usuario1.Nombre = query.Nombre;
                        usuario1.ApellidoPaterno = query.ApellidoPaterno;
                        usuario1.ApellidoMaterno = query.ApellidoMaterno;
                        usuario1.Email = query.Email;
                        usuario1.Sexo = query.Sexo;
                        usuario1.Telefono = query.Telefono;
                        usuario1.Celular = query.Celular;
                        usuario1.FechaNacimiento = query.FechaNacimiento?.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                        usuario1.Curp = query.Curp;
                        usuario1.Imagen = query.Imagen;
                        usuario1.Estatus = (bool)query.Estatus;
                        usuario1.Password = query.Password;

                        usuario1.Rol = new ML.Rol();
                        usuario1.Rol.IdRol = query.IdRol.Value;
                        usuario1.Rol.Nombre = query.RolNombre;

                        usuario1.Direccion = new ML.Direccion();
                        usuario1.Direccion.IdDireccion = query.IdDireccion.Value;
                        usuario1.Direccion.Calle = query.DireccionCalle;
                        usuario1.Direccion.NumeroInterior = query.DireccionNumeroInterior;
                        usuario1.Direccion.NumeroExterior = query.DireccionNumeroExterior;

                        usuario1.Direccion.Colonia = new ML.Colonia();
                        usuario1.Direccion.Colonia.IdColonia = query.IdColonia.Value;
                        usuario1.Direccion.Colonia.Nombre = query.NombreColonia;
                        usuario1.Direccion.Colonia.CodigoPostal = query.CodigoPostal;

                        usuario1.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario1.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio.Value;
                        usuario1.Direccion.Colonia.Municipio.Nombre = query.NombreMunicipio;

                        usuario1.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario1.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado.Value;
                        usuario1.Direccion.Colonia.Municipio.Estado.Nombre = query.NombreEstado;

                        usuario1.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario1.Direccion.Colonia.Municipio.Estado.Pais.IdPais = query.IdPais.Value;
                        usuario1.Direccion.Colonia.Municipio.Estado.Pais.Nombre = query.NombrePais;

                        result.Object = usuario1;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al consultar al Usuario";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}