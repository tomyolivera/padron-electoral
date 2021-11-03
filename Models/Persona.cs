using System;

namespace padron_electoral.Models
{
    public class Persona
    {
        private int _DNI;
        private string _Apellido;
        private string _Nombre;
        private int _NumeroTramite;
        private int _IdEstablecimiento;
        private bool _Voto;
        private DateTime _FechaVotacion;

        public Persona(int dni, string apellido, string nombre, int numeroTramite, int idEstablecimiento, bool voto, DateTime fechaVotacion)
        {
            _DNI = dni;
            _Apellido = apellido;
            _Nombre = nombre;
            _NumeroTramite = numeroTramite;
            _IdEstablecimiento = idEstablecimiento;
            _Voto = voto;
            _FechaVotacion = fechaVotacion;
        }
        public Persona(int dni, string apellido, string nombre, int numeroTramite, int idEstablecimiento, bool voto)
        {
            _DNI = dni;
            _Apellido = apellido;
            _Nombre = nombre;
            _NumeroTramite = numeroTramite;
            _IdEstablecimiento = idEstablecimiento;
            _Voto = voto;
        }

        public int DNI { get { return _DNI; } set { _DNI = value; } }
        public int IdEstablecimiento { get { return _IdEstablecimiento; } set { _IdEstablecimiento = value; } }
        public string Nombre { get { return _Nombre; } set { _Nombre = value; } }
        public string Apellido { get { return _Apellido; } set { _Apellido = value; } }
        public int NumeroTramite { get { return _NumeroTramite; } set { _NumeroTramite = value; } }
        public bool Voto { get { return _Voto; } set { _Voto = value; } }
        public DateTime FechaVotacion { get { return _FechaVotacion; } set { _FechaVotacion = value; } }
    }
}
