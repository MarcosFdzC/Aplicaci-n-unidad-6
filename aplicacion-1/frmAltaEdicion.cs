using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace aplicacion_1
{
    public partial class frmAltaEdicion : Form
    {
        public frmAltaEdicion()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Disco disc = new Disco();
            DiscoNegocio negocio = new DiscoNegocio();
            try
            {
                disc.Titulo = txtbTitulo.Text;
                disc.FechaLanzamiento = dtmpLanzamiento.Value;
                disc.ImgUrl = txtbUrlImagen.Text;
                disc.CantCanciones = int.Parse(txtbCanciones.Text);
                disc.Edicion = (Edicion)cboEdicion.SelectedItem;
                disc.Estilo = (Estilo)cboEstilo.SelectedItem;

                negocio.agregar(disc);
                MessageBox.Show("Agregado Exitosamente");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaEdicion_Load(object sender, EventArgs e)
        {
            EstiloNegocio estiloNegocio = new EstiloNegocio();
            EdicionNegocio edicionNegocio = new EdicionNegocio();
            try
            {
                cboEdicion.DataSource = edicionNegocio.Listar();
                cboEstilo.DataSource = estiloNegocio.Listar();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtbUrlImagen_Leave(object sender, EventArgs e)
        {
            cargaImagen(txtbUrlImagen.Text);
        }
        private void cargaImagen(string imagen)
        {
            try
            {
                pbxDisco.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxDisco.Load("https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png");
            }
        }
    }
}
