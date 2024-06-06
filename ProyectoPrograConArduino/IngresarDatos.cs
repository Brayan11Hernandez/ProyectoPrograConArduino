using Newtonsoft.Json;
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
    public partial class IngresarDatos : Form
    {
        Contadores contadores = new Contadores();
        private List<Abastecimiento> abastecimientos = new List<Abastecimiento>();
        public IngresarDatos()
        {
            InitializeComponent();
            //serialPort1.Open();
        }
        private void Ingresar(Abastecimiento nuevoAbastecimiento)
        {
            // Guarda el nuevo abastecimiento en el archivo
            using (FileStream stream = new FileStream("Abastecimientos.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(nuevoAbastecimiento.NoBomba);
                    writer.WriteLine(nuevoAbastecimiento.FechaAbas);
                    writer.WriteLine(nuevoAbastecimiento.NombreCliente);
                    writer.WriteLine(nuevoAbastecimiento.ApellidoCliente);
                    writer.WriteLine(nuevoAbastecimiento.TipoAbas);
                    writer.WriteLine(nuevoAbastecimiento.Importe);
                    writer.WriteLine(nuevoAbastecimiento.Formallenado);
                }
            }

            // Guarda los contadores actualizados en el archivo
            using (FileStream stream = new FileStream("Contadores.txt", FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(contadores.Cont1);
                    writer.WriteLine(contadores.Cont2);
                }
            }
        }
        private void guardarDatos()
        {
            try
            {
                string json = JsonConvert.SerializeObject(abastecimientos, Formatting.Indented);
                string archivo = "Datos.json";
                File.WriteAllText(archivo, json);
                MessageBox.Show("Datos guardados correctamente en formato JSON.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar datos en JSON: {ex.Message}");
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Abastecimiento dato = new Abastecimiento();
            dato.NombreCliente = txtNombre.Text;
            dato.ApellidoCliente = txtApellido.Text;
            dato.FechaAbas = monthCalendar1.SelectionStart;

            if (rbSuper.Checked == true)
            {
                dato.NoBomba = 1;
                dato.TipoAbas = "Super";
                contadores.Cont1++;
            }
            else if (rbRegular.Checked == true) 
            {
                dato.NoBomba = 2;
                dato.TipoAbas = "Regular";
                contadores.Cont2++;
            }

            if (rbPrepago.Checked == true)
            {
                dato.Formallenado = "Pre-pago";
                dato.Importe = decimal.Parse(txtImporte.Text);
            }
            else if(rbTanqueLleno.Checked == true)
            {
                dato.Formallenado = "Tanque lleno";
            }
            abastecimientos.Add(dato);
            Ingresar(dato);
            MessageBox.Show("Registro Con Exito, Inicie el Llenado");
            guardarDatos();
            //Resetear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string m1 = "S" + txtImporte.Text;
            serialPort1.Write(m1);
        }

        private void rbTanqueLleno_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTanqueLleno.Checked)
            {
                txtImporte.Enabled = false;
                txtImporte.Text = string.Empty;
            }
            else
            {
                txtImporte.Enabled = true;
            }
        }
        private void Resetear()
        {
            txtNombre.Text = string.Empty;
            txtImporte.Text = string.Empty;
            rbPrepago.Checked = true;
            rbSuper.Checked = true;
            txtNombre.Focus();
        }
    }
}
