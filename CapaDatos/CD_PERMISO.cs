using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Permiso
    {
        // Método para listar los permisos por IdRol
        public List<Permiso> ListarPorRol(int idRol)
        {
            List<Permiso> lista = new List<Permiso>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT p.IdPermiso, p.IdRol, r.Descripcion, p.NombreMenu");
                    query.AppendLine("FROM PERMISO p");
                    query.AppendLine("INNER JOIN ROL r ON r.IdRol = p.IdRol");
                    query.AppendLine("WHERE r.IdRol = @IdRol");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@IdRol", idRol);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Permiso()
                            {
                                IdPermiso = Convert.ToInt32(dr["IdPermiso"]),
                                oRol = new Rol()
                                {
                                    IdRol = Convert.ToInt32(dr["IdRol"]),
                                    Descripcion = dr["Descripcion"].ToString()
                                },
                                NombreMenu = dr["NombreMenu"].ToString(),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    lista = new List<Permiso>(); // Retornar lista vacía en caso de error
                }
            }

            return lista;
        }

        // Método para listar todos los permisos
        public List<Permiso> ListarTodos()
        {
            List<Permiso> lista = new List<Permiso>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT p.IdPermiso, p.IdRol, r.Descripcion AS RolDescripcion, p.NombreMenu");
                    query.AppendLine("FROM PERMISO p");
                    query.AppendLine("INNER JOIN ROL r ON r.IdRol = p.IdRol");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Permiso()
                            {
                                IdPermiso = Convert.ToInt32(dr["IdPermiso"]),
                                oRol = new Rol()
                                {
                                    IdRol = Convert.ToInt32(dr["IdRol"]),
                                    Descripcion = dr["RolDescripcion"].ToString()
                                },
                                NombreMenu = dr["NombreMenu"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    lista = new List<Permiso>(); // Retornar lista vacía en caso de error
                }
            }

            return lista;
        }
        public List<Permiso> Listar(int idusuario)
        {
            List<Permiso> lista = new List<Permiso>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT p.IdPermiso, p.IdRol, r.Descripcion, p.NombreMenu");
                    query.AppendLine("FROM PERMISO p");
                    query.AppendLine("INNER JOIN ROL r ON r.IdRol = p.IdRol");
                    query.AppendLine("INNER JOIN USUARIO u ON u.IdRol = r.IdRol");
                    query.AppendLine("WHERE u.IdUsuario = @idusuario");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idusuario", idusuario);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Permiso()
                            {
                                IdPermiso = Convert.ToInt32(dr["IdPermiso"]),
                                oRol = new Rol()
                                {
                                    IdRol = Convert.ToInt32(dr["IdRol"]),
                                    Descripcion = dr["Descripcion"].ToString()
                                },
                                NombreMenu = dr["NombreMenu"].ToString(),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Permiso>(); // Retornar lista vacía en caso de error
                }
            }

            return lista;
        }

        // Método para registrar un nuevo permiso
        public int Registrar(Permiso obj, out string Mensaje)
        {
            int idPermisoGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARPERMISO", oconexion);
                    cmd.Parameters.AddWithValue("IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("NombreMenu", obj.NombreMenu);
                    cmd.Parameters.Add("IdPermisoResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    idPermisoGenerado = Convert.ToInt32(cmd.Parameters["IdPermisoResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idPermisoGenerado = 0;
                Mensaje = ex.Message;
            }

            return idPermisoGenerado;
        }

        // Método para editar un permiso existente
        public bool Editar(Permiso obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARPERMISO", oconexion);
                    cmd.Parameters.AddWithValue("IdPermiso", obj.IdPermiso);
                    cmd.Parameters.AddWithValue("IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("NombreMenu", obj.NombreMenu);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }

        // Método para eliminar un permiso existente
        public bool Eliminar(Permiso obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARPERMISO", oconexion);
                    cmd.Parameters.AddWithValue("IdPermiso", obj.IdPermiso);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }
    }
}