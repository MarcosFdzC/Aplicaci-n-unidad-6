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
        private Disco disco = null;
        public frmAltaEdicion()
        {
            InitializeComponent();
        }
        public frmAltaEdicion(Disco disco)
        {
            InitializeComponent();
            this.disco = disco;
            Text = "Modificar disco";

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            DiscoNegocio negocio = new DiscoNegocio();
            try
            {
                disco.Titulo = txtbTitulo.Text;
                disco.FechaLanzamiento = dtmpLanzamiento.Value;
                disco.ImgUrl = txtbUrlImagen.Text;
                disco.CantCanciones = int.Parse(txtbCanciones.Text);
                disco.Edicion = (Edicion)cboEdicion.SelectedItem;
                disco.Estilo = (Estilo)cboEstilo.SelectedItem;

                if (disco.Id != 0)
                {
                    negocio.modificar(disco);
                    MessageBox.Show("Modificado exitosamente");
                }
                else 
                {
                    negocio.agregar(disco);
                    MessageBox.Show("Agregado exitosamente");
                }

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
                cboEdicion.ValueMember = "Id";
                cboEdicion.DisplayMember = "Descripcion";
                cboEstilo.DataSource = estiloNegocio.Listar();
                cboEstilo.ValueMember = "Id";
                cboEstilo.DisplayMember = "Descripcion";

                if(disco != null)
                {
                    txtbTitulo.Text = disco.Titulo;
                    dtmpLanzamiento.Value = disco.FechaLanzamiento;
                    txtbUrlImagen.Text = disco.ImgUrl;
                    cargaImagen(disco.ImgUrl);
                    txtbCanciones.Text = disco.CantCanciones.ToString();
                    cboEstilo.SelectedValue = disco.Estilo.Id;
                    cboEdicion.SelectedValue = disco.Edicion.Id;
                }
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
