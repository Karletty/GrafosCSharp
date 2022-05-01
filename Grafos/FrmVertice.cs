using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafos
{
    public partial class FrmVertice : Form
    {
        public bool control;
        //Sirve para controlar si ya se tiene un dato ingresado
        public string dato;
        //Almacena el dato ingresado

        public FrmVertice()
        {
            InitializeComponent();
            control = false;
            dato = "";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Verifica si no se ha ingresado ningún valor y si se ha ingresado un valor cambia la variable control y regresa al formulario
            string v = txtVertice.Text.Trim();

            if ((v == "") || (v == " "))
            {
                MessageBox.Show("Debe ingresar un valor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                control = true;
                Hide();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Determina que la acción se cancelo, regresa al formulario sin hacer nada
            control = false;
            Hide();
        }

        private void FrmVertice_Load(object sender, EventArgs e)
        {
            //Cuando el formulario carga se va a enfocar al texto donde se ingresa el vértice
            txtVertice.Focus();
        }

        private void FrmVertice_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Cuando se de en el botón de cerrar para el formulario solamente se va a ocultar, pero se mantendrá activo
            this.Hide();
            e.Cancel = true;
        }

        private void FrmVertice_Shown(object sender, EventArgs e)
        {
            //Cuando el formulario se muestre se va a limpiar el texto remanente y se enfocará el texto donde se ingresará el vértice
            txtVertice.Clear();
            txtVertice.Focus();
        }

        private void txtVertice_KeyDown(object sender, KeyEventArgs e)
        {
            //Al dar enter hará lo mismo que cuando se da aceptar
            if (e.KeyCode == Keys.Enter)
            {
                btnAceptar_Click(null, null);
            }
        }
    }
}