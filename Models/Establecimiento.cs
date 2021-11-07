using System;

namespace padron_electoral.Models
{
    public class Establecimiento
    {
        private int _IdEstablecimiento;
        private string _Nombre;
        private string _Direccion;
        private string _Localidad;
        public int IdEstablecimiento { get { return _IdEstablecimiento; } set { _IdEstablecimiento = value; } }
        public string Nombre { get { return _Nombre; } set { _Nombre = value; } }
        public string Direccion { get { return _Direccion; } set { _Direccion = value; } }
        public string Localidad { get { return _Localidad; } set { _Localidad = value; } }
    }
}
