using System;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;
using System.Text;

namespace CapaDatos
{
    public class CD_Pago
    {
        // Método para registrar un pago en la base de datos
        public bool RegistrarPago(Pago pago, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("INSERT INTO PAGO (NumeroDocumento, DocumentoCliente, NombreCliente, MetodoPago, TotalAPagar, FechaRegistro, ComprobanteQR, EstadoPago)");
                    query.AppendLine("VALUES (@NumeroDocumento, @DocumentoCliente, @NombreCliente, @MetodoPago, @TotalAPagar, @FechaRegistro, @ComprobanteQR, @EstadoPago)");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@NumeroDocumento", pago.NumeroDocumento);
                    cmd.Parameters.AddWithValue("@DocumentoCliente", pago.DocumentoCliente);
                    cmd.Parameters.AddWithValue("@NombreCliente", pago.NombreCliente);
                    cmd.Parameters.AddWithValue("@MetodoPago", pago.MetodoPago);
                    cmd.Parameters.AddWithValue("@TotalAPagar", pago.TotalAPagar);
                    cmd.Parameters.AddWithValue("@FechaRegistro", pago.FechaRegistro);
                    cmd.Parameters.AddWithValue("@ComprobanteQR", pago.ComprobanteQR ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@EstadoPago", pago.EstadoPago);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = true;
                }
                catch (Exception ex)
                {
                    mensaje = "Error al registrar el pago: " + ex.Message;
                    respuesta = false;
                }
            }

            return respuesta;
        }

        // Método para obtener un pago por su ID
        public Pago ObtenerPago(int idPago)
        {
            Pago pago = null;

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT * FROM PAGO WHERE IdPago = @IdPago");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@IdPago", idPago);
                    conexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            pago = new Pago()
                            {
                                IdPago = Convert.ToInt32(dr["IdPago"]),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                DocumentoCliente = dr["DocumentoCliente"].ToString(),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                MetodoPago = dr["MetodoPago"].ToString(),
                                TotalAPagar = Convert.ToDecimal(dr["TotalAPagar"]),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]),
                                ComprobanteQR = dr["ComprobanteQR"] != DBNull.Value ? (byte[])dr["ComprobanteQR"] : null,
                                EstadoPago = Convert.ToBoolean(dr["EstadoPago"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener el pago: " + ex.Message);
                }
            }

            return pago;
        }

        // Método para obtener un pago por el número de la venta (IdVenta)
        public Pago ObtenerPagoPorVenta(int idVenta)
        {
            Pago pago = null;
            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT P.IdPago, P.MetodoPago, P.ComprobanteQR, P.EstadoPago, P.FechaRegistro AS FechaPago,");
                    query.AppendLine("       V.TipoDocumento, V.NumeroDocumento, V.DocumentoCliente, V.NombreCliente, V.MontoTotal, V.FechaRegistro AS FechaVenta");
                    query.AppendLine("FROM PAGO P");
                    query.AppendLine("INNER JOIN VENTA V ON P.IdVenta = V.IdVenta");
                    query.AppendLine("WHERE P.IdVenta = @IdVenta");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@IdVenta", idVenta);
                    conexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            pago = new Pago()
                            {
                                IdPago = Convert.ToInt32(dr["IdPago"]),
                                MetodoPago = dr["MetodoPago"].ToString(),
                                ComprobanteQR = dr["ComprobanteQR"] != DBNull.Value ? (byte[])dr["ComprobanteQR"] : null,
                                EstadoPago = Convert.ToBoolean(dr["EstadoPago"]),
                                FechaRegistro = Convert.ToDateTime(dr["FechaPago"]),
                                oVenta = new Venta()
                                {
                                    IdVenta = idVenta,
                                    TipoDocumento = dr["TipoDocumento"].ToString(),
                                    NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                    DocumentoCliente = dr["DocumentoCliente"].ToString(),
                                    NombreCliente = dr["NombreCliente"].ToString(),
                                    MontoTotal = Convert.ToDecimal(dr["MontoTotal"]),
                                    FechaRegistro = Convert.ToDateTime(dr["FechaVenta"]).ToString("yyyy-MM-dd")
                                }
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener el pago por venta: " + ex.Message);
                }
            }
            return pago;
        }

    }
}


