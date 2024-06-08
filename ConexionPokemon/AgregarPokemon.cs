using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;
using System.IO;
using System.Configuration;

namespace ConexionPokemon
{
    public partial class frmAgregarPokemon : Form
    {
        private Pokemon pokemon = null;
        private OpenFileDialog archivo = null;
        public frmAgregarPokemon(Pokemon pokemon)
        {
            InitializeComponent();
            Text = "Modificar Pokemon";
            this.pokemon = pokemon;
        }

        public frmAgregarPokemon()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                if (pokemon == null)
                    pokemon = new Pokemon();
                pokemon.Numero = int.Parse(txtNumero.Text);
                pokemon.Nombre = txtNombre.Text;
                pokemon.Descripcion = txtDescripcion.Text;
                pokemon.UrlImagen = txtUrlImagen.Text;
                pokemon.Tipo = (Elemento) cboTipo.SelectedItem;
                pokemon.Debilidad = (Elemento) cboDebilidad.SelectedItem;
                if (pokemon.Id != 0)
                {
                    negocio.Modificar(pokemon);
                    MessageBox.Show("Pokemon modificado exitosamente!");
                }
                else
                {
                    negocio.AgregarPokemon(pokemon);
                    MessageBox.Show("Pokemon agregado con éxito");
                }
                if (archivo != null && !(txtUrlImagen.Text.ToUpper().Contains("HTTP")))
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName);
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAgregarPokemon_Load(object sender, EventArgs e)
        {
           ElementoNegocio elementoNegocio = new ElementoNegocio();
            try
            {
                cboTipo.DataSource = elementoNegocio.listar();
                cboTipo.ValueMember = "Id";
                cboTipo.DisplayMember = "Descripcion";
                cboDebilidad.DataSource = elementoNegocio.listar();
                cboDebilidad.ValueMember = "Id";
                cboDebilidad.DisplayMember = "Descripcion";
                if (pokemon != null)
                {
                    txtNumero.Text = pokemon.Numero.ToString();
                    txtNombre.Text = pokemon.Nombre;
                    txtDescripcion.Text = pokemon.Descripcion;
                    txtUrlImagen.Text = pokemon.UrlImagen;
                    cargarImagen(pokemon.UrlImagen);
                    cboTipo.SelectedValue = pokemon.Tipo.Id;
                    cboDebilidad.SelectedValue = pokemon.Debilidad.Id;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxAgregarPokemon.Load(imagen);
            }
            catch (Exception ex)
            {

                pbxAgregarPokemon.Load("https://www.came-educativa.com.ar/upsoazej/2022/03/placeholder-4.png");
            }
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg;|png|*.png";
            try
            {
                if (archivo.ShowDialog() == DialogResult.OK)
                {
                    txtUrlImagen.Text = archivo.FileName;
                    cargarImagen(archivo.FileName);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
