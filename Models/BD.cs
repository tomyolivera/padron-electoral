using System;
using System.Data.SqlClient;
using Dapper;

namespace padron_electoral.Models
{
    public static class BD
    {
        private static string _connectionString = @"Server=DESKTOP-68PON6U\SQLEXPRESS;Database=BDPadron;Trusted_Connection=True;";

        public static Persona ConsultarPadron(int DNI)
        {
            Persona persona = null;

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Personas WHERE DNI = @DNI";
                var result = connection.QueryFirstOrDefault(query, new { DNI });

                if(result != null){
                    if(result.FechaVotacion == null){
                        persona = new Persona(result.DNI,
                                              result.Apellido,
                                              result.Nombre,
                                              result.NumeroTramite,
                                              result.IdEstablecimiento,
                                              result.Voto
                                            );
                    }else{
                        persona = new Persona(result.DNI,
                                              result.Apellido,
                                              result.Nombre,
                                              result.NumeroTramite,
                                              result.IdEstablecimiento,
                                              result.Voto,
                                              result.FechaVotacion
                                            );
                    }
                }
            }

            return persona;
        }

        public static Establecimiento ConsultarEstablecimiento(int id)
        {
            Establecimiento establecimiento = null;

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Establecimiento WHERE IdEstablecimiento = @id";
                var result = connection.QueryFirstOrDefault(query, new { id });

                if(result != null)
                    establecimiento = new Establecimiento(result.IdEstablecimiento, result.Nombre, result.Direccion, result.Localidad);
                
            }

            return establecimiento;
        }

        public static void ObtenerReporte()
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"   SELECT E.IdEstablecimiento, E.Nombre, P.Nombre, P.Voto
                                    FROM Personas P
                                    INNER JOIN Establecimiento E ON E.IdEstablecimiento = P.IdEstablecimiento
                                    GROUP BY E.IdEstablecimiento, E.Nombre, P.Nombre, P.Voto
                                    HAVING P.Voto = 1
                                ";
                var result = connection.Query(query);
                Console.WriteLine("Resultado: " + result);
            }
        }

        public static bool Votar(int DNI, int IdEstablecimiento)
        {
            bool status = false;

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Personas SET Voto = 1, FechaVotacion = @FechaVotacion WHERE DNI = @DNI";
                var affectedRows = connection.Execute(query, new { DNI, FechaVotacion = DateTime.Now });

                if(affectedRows > 0)
                    status = true;
            }

            return status;
        }
    }
}
