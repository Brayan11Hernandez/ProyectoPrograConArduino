using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograConArduino
{
    internal class Abastecimiento
    {
        int noBomba;
        DateTime fechaAbas;
        string nombreCliente;
        string apellidoCliente;
        string tipoAbas;
        decimal importe;
        string formallenado;

        public int NoBomba { get => noBomba; set => noBomba = value; }
        public DateTime FechaAbas { get => fechaAbas; set => fechaAbas = value; }
        public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
        public string ApellidoCliente { get => apellidoCliente; set => apellidoCliente = value; }
        public string TipoAbas { get => tipoAbas; set => tipoAbas = value; }
        public decimal Importe { get => importe; set => importe = value; }
        public string Formallenado { get => formallenado; set => formallenado = value; }
    }
}
