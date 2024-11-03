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
            if (dgvDiscos.CurrentRow != null)
            {
                Disco seleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
                cargaImagen(seleccionado.ImgUrl);
            }

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
                ocultarColumnas();
                cargaImagen(listaDisco[0].ImgUrl);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void ocultarColumnas()
        {
            dgvDiscos.Columns["ImgUrl"].Visible = false;
            dgvDiscos.Columns["Id"].Visible = false;
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

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void btnEliminarLogico_Click(object sender, EventArgs e)
        {
            eliminar(true);
        }
        private void eliminar(bool logico = false)
        {
            DiscoNegocio eliminado = new DiscoNegocio();
            Disco seleccionado = new Disco();
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Eliminar el disco?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
                    if (logico)
                        eliminado.eliminarLogico(seleccionado.Id);
                    else
                        eliminado.eliminar(seleccionado.Id);
                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            List<Disco> listaFiltrada;
            string filtro = txtFiltro.Text;
            if (filtro != "")
            { 
                //FindAll es un método que busca dentro de la lista, toupper pone en igualdad de minusculas/mayusculas el contenido de las dos cadenas.
                //Y el método Contains exije que le pasemos la otra cadena para comparar si contiene esa cadena de caracteres. Ej True si tiene tr, este método detectará a True ya que True contiene la cadena tr.
                
                listaFiltrada = listaDisco.FindAll(x => x.Titulo.ToUpper().Contains(filtro.ToUpper()) || x.Estilo.Descripcion.ToUpper().Contains(filtro.ToUpper()) );
                //despues del OR || le agrego otra condición para que filtre por Estilo tambien.
            }

            else
            {
                listaFiltrada = listaDisco;
            }

            dgvDiscos.DataSource = null;
            dgvDiscos.DataSource = listaFiltrada;
            ocultarColumnas();
        }
    }

   
}
