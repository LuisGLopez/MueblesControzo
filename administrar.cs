using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BancoControzo
{
    public partial class Administrar : Form
    {
        public Administrar()
        {
            InitializeComponent();
        }

        private void btnSalirUsuarios_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            MessageBoxButtons botones = MessageBoxButtons.OK;
            switch (agregarUsuario())
            {
                case 0:
                    {
                        MessageBox.Show("Favor de llenar los campos necesarios.", "Datos incompletos!", botones);
                        break;
                    }
                case 1:
                    {
                        MessageBox.Show("Usuario agregado con exito!", "Agregado con exito!", botones);
                        break;
                    }
                case 2:
                    {
                        MessageBox.Show("Error intente de nuevo", "Error!", botones);
                        break;
                    }
                default:
                    MessageBox.Show("Error intente de nuevo", "Error!", botones);
                    break;
            }
        }

        private int agregarUsuario()
        {
            string nombre = txtNombreUsuario.Text;
            string passwd = txtContraseñaUsuario.Text;

            if (nombre == "" || passwd == "" || comboTipoUsuario.SelectedIndex <= -1)
            {
                return 0;
            }
            string tipo = comboTipoUsuario.SelectedItem.ToString();

            string queryAgregar = "INSERT INTO usuarios(username, passwd, tipo_usuario) VALUES('" + nombre + "', SHA2('" + passwd + "', 384), '" + tipo + "');";

            string MySQLConectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=muebleria";

            MySqlConnection dataBaseConnection = new MySqlConnection(MySQLConectionString);
            MySqlCommand commandDatabase = new MySqlCommand(queryAgregar, dataBaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                dataBaseConnection.Open();
                MySqlDataReader reader = commandDatabase.ExecuteReader();

                dataBaseConnection.Close();

                txtNombreUsuario.Text = "";
                txtContraseñaUsuario.Text = "";
                comboTipoUsuario.SelectedIndex = -1;
                
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 2;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            MessageBoxButtons botones = MessageBoxButtons.OK;
            switch (agregarProducto())
            {
                case 0:
                    {
                        MessageBox.Show("Favor de llenar los campos necesarios.", "Datos incompletos!", botones);
                        break;
                    }
                case 1:
                    {
                        MessageBox.Show("Producto agregado con exito!", "Agregado exitosamente!", botones);
                        break;
                    }
                case 2:
                    {
                        MessageBox.Show("Error intente de nuevo", "Error!", botones);
                        break;
                    }
                default:
                    MessageBox.Show("Error intente de nuevo", "Error!", botones);
                    break;
            }
        }

        public int agregarProducto()
        {
            string nombreProd = txtNombreProducto.Text.ToUpper();
            string precioProd = numPrecioProducto.Value.ToString();
            string existenciasProd = numExistenciasProducto.Value.ToString();
            string colorProd = txtColor.Text.ToUpper();
            string modeloProd = txtModelo.Text.ToUpper();

            // Revisar si todo tiene datos
            if (nombreProd == "" || precioProd == "" || existenciasProd  == "" || colorProd == "" || modeloProd == "" || comboTipoProducto.SelectedIndex <= -1 || precioProd == "0" || existenciasProd == "0")
            {
                return 0;
            }
            
            string tipoProd = comboTipoProducto.SelectedItem.ToString();

            string queryAgregar = "INSERT INTO producto(nombre_producto, precio_producto, existencias_producto, color, tipo_producto, modelo)" +
                " VALUES ('" + nombreProd + "', " + precioProd + ", " + existenciasProd + ", '" + colorProd + "', '" + tipoProd + "', '" + modeloProd + "');";

            string MySQLConectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=muebleria";

            MySqlConnection dataBaseConnection = new MySqlConnection(MySQLConectionString);
            MySqlCommand commandDatabase = new MySqlCommand(queryAgregar, dataBaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                dataBaseConnection.Open();
                MySqlDataReader reader = commandDatabase.ExecuteReader();

                dataBaseConnection.Close();

                limpiarCampos();

                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 2;
            }
        }

        private void btnBuscarModeloProducto_Click(object sender, EventArgs e)
        {
            MessageBoxButtons botones = MessageBoxButtons.OK;
            switch (buscarProducto())
            {
                case 0:
                    {
                        MessageBox.Show("Favor de llenar el campo de busqueda.", "Datos incompletos!", botones);
                        break;
                    }
                case 1:
                    {
                        btnAgregarProducto.Enabled = false;
                        btnEliminarProducto.Enabled = true;
                        btnModificarProducto.Enabled = true;
                        btnSalirBusquedaProducto.Enabled = true;
                        MessageBox.Show("Producto encontrado!", "Encontrado exitosamente!", botones);
                        break;
                    }
                case 2:
                    {
                        MessageBox.Show("Error intente de nuevo", "Error!", botones);
                        break;
                    }
                default:
                    MessageBox.Show("Error intente de nuevo", "Error!", botones);
                    break;
            }
            
        }

        private int buscarProducto()
        {
            if (txtBuscarProducto.Text == "")
            {
                return 0;
            }

            string productoBuscar = txtBuscarProducto.Text.ToUpper();
            //string[] productoEncontrado;
            string queryBuscar = "SELECT * FROM producto WHERE modelo = '" + productoBuscar + "';";
            string MySQLConectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=muebleria";

            MySqlConnection dataBaseConnection = new MySqlConnection(MySQLConectionString);
            MySqlCommand commandDatabase = new MySqlCommand(queryBuscar, dataBaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                dataBaseConnection.Open();
                MySqlDataReader reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtNombreProducto.Text = reader.GetString(1);
                        numPrecioProducto.Value = Decimal.Parse( reader.GetString(2) );
                        numExistenciasProducto.Value = int.Parse( reader.GetString(3) );
                        txtColor.Text = reader.GetString(4);
                        txtModelo.Text = reader.GetString(6);

                        comboTipoProducto.SelectedIndex = comboTipoProducto.FindString( reader.GetString(5) );

                        txtBuscarProducto.Text = "";

                    }
                }

                dataBaseConnection.Close();

                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 2;
            }
        }

        private void btnSalirBusqueda_Click(object sender, EventArgs e)
        {
            btnAgregarProducto.Enabled = true;
            btnEliminarProducto.Enabled = false;
            btnModificarProducto.Enabled = false;
            btnSalirBusquedaProducto.Enabled = false;

            limpiarCampos();
        }

        private void limpiarCampos()
        {
            txtNombreProducto.Text = "";
            numPrecioProducto.Value = 0;
            numExistenciasProducto.Value = 0;
            txtColor.Text = "";
            txtModelo.Text = "";
            comboTipoProducto.SelectedIndex = -1;
        }

    }
}
