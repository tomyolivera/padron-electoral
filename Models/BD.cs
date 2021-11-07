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
                persona = connection.QueryFirstOrDefault<Persona>(query, new { DNI });
            }

            return persona;
        }

        public static Establecimiento ConsultarEstablecimiento(int id)
        {
            Establecimiento establecimiento = null;

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Establecimiento WHERE IdEstablecimiento = @id";
                establecimiento = connection.QueryFirstOrDefault<Establecimiento>(query, new { id });
            }

            return establecimiento;
        }

        public static dynamic ObtenerReporte()
        {
            dynamic result = null;

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"   SELECT E.Nombre, COUNT(P.Voto) AS VotosTotales, COUNT(ISNULL(NULL, P.FechaVotacion)) AS VotosRealizados, E.Imagen
                                    FROM Establecimiento E
                                    INNER JOIN Personas P ON P.IdEstablecimiento = E.IdEstablecimiento
                                    GROUP BY E.Nombre, E.Imagen
                                    HAVING E.Nombre IS NOT NULL AND COUNT(P.Voto) != 0
                                ";
                result = connection.Query(query);
            }

            return result;
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
