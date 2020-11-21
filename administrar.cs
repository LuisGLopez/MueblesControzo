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
    }
}
