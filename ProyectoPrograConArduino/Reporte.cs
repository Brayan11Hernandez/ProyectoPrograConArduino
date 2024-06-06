using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoPrograConArduino
{
    public partial class Reporte : Form
    {
        List<Abastecimiento> abastes = new List<Abastecimiento>();
        List<Abastecimiento> reportes = new List<Abastecimiento>();
        List<Abastecimiento> rep1 = new List<Abastecimiento>();
        public Reporte()
        {
            InitializeComponent();
        }
        public void LeerAbastecimientos()
        {

            string fileName = "Abastecimientos.txt";
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            while (reader.Peek() > -1)
            {
                //Se crea una lista temporal para agreagar propietarios
                Abastecimiento abastecimiento = new Abastecimiento();
                abastecimiento.NoBomba = int.Parse(reader.ReadLine());
                abastecimiento.FechaAbas = DateTime.Parse(reader.ReadLine());
                abastecimiento.NombreCliente = reader.ReadLine();
                abastecimiento.ApellidoCliente = reader.ReadLine();
                abastecimiento.TipoAbas = reader.ReadLine();
                abastecimiento.Importe = decimal.Parse(reader.ReadLine());
                abastecimiento.Formallenado = reader.ReadLine();
                //agregar al propietario a la lista de propietarios
                abastes.Add(abastecimiento);
            }
            reader.Close();
        }
        public void CargarCierreCaja()
        {



            // Limpiar la lista de reportes antes de agregar nuevos datos
            rep1.Clear();

            // Agregar todos los registros filtrados a la lista de reportes
            foreach (var abastecimiento in abastes)
            {
                Abastecimiento reporte = new Abastecimiento
                {
                    NombreCliente = abastecimiento.NombreCliente,
                    ApellidoCliente = abastecimiento.ApellidoCliente,
                    FechaAbas = abastecimiento.FechaAbas,
                    TipoAbas = abastecimiento.TipoAbas,
                    Importe = abastecimiento.Importe,
                    Formallenado = abastecimiento.Formallenado,
                };
                rep1.Add(reporte);
            }

            dgvCierre.DataSource = null;
            dgvCierre.DataSource = rep1;
            dgvCierre.Refresh();

            // Ocultar la columna "NoBomba"
            if (dgvCierre.Columns["NoBomba"] != null)
            {
                dgvCierre.Columns["NoBomba"].Visible = false;
            }
        }
        public void CargarPrepago()
        {
            string busqueda = "Pre-pago";

            // Filtrar los registros que cumplen la condición de "Pre-pago"
            var prepagos = abastes.Where(v => v.Formallenado == busqueda);

            // Limpiar la lista de reportes antes de agregar nuevos datos
            reportes.Clear();

            // Agregar todos los registros filtrados a la lista de reportes
            foreach (var abastecimiento in prepagos)
            {
                Abastecimiento reporte = new Abastecimiento
                {
                    NombreCliente = abastecimiento.NombreCliente,
                    ApellidoCliente = abastecimiento.ApellidoCliente,
                    FechaAbas = abastecimiento.FechaAbas,
                    TipoAbas = abastecimiento.TipoAbas,
                    Importe = abastecimiento.Importe,
                    Formallenado = abastecimiento.Formallenado,
                };
                reportes.Add(reporte);
            }

            dgvPrepago.DataSource = null;
            dgvPrepago.DataSource = reportes;
            dgvPrepago.Refresh();

            // Ocultar la columna "NoBomba"
            if (dgvPrepago.Columns["NoBomba"] != null)
            {
                dgvPrepago.Columns["NoBomba"].Visible = false;
            }
        }
        private void btnMostrar_Click(object sender, EventArgs e)
        {
            CargarPrepago();
            CargarCierreCaja();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Reporte_Load(object sender, EventArgs e)
        {
            LeerAbastecimientos();
        }
    }
}
