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
    public partial class frmDisco : Form
    {
        private List<Disco> listaDisco;
        public frmDisco()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void dgvDiscos_SelectionChanged(object sender, EventArgs e)
        {
            Disco seleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
            cargaImagen(seleccionado.ImgUrl);
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

        private void cargar()
        {
            DiscoNegocio negocio = new DiscoNegocio();
            try
            {
                listaDisco = negocio.Listar();
                dgvDiscos.DataSource = listaDisco;
                dgvDiscos.Columns["ImgUrl"].Visible = false;
                dgvDiscos.Columns["Id"].Visible = false;
                cargaImagen(listaDisco[0].ImgUrl);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaEdicion alta = new frmAltaEdicion();
            alta.ShowDialog();
            cargar();
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            Disco seleccionado;
            seleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
            frmAltaEdicion modificacion = new frmAltaEdicion(seleccionado);
            modificacion.ShowDialog();
            cargar();
        }
    }

   
}
