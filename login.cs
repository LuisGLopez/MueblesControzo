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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            MessageBoxButtons botones = MessageBoxButtons.OK;
            switch (ingresarCuenta())
            {
                case 0:
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos.", "Datos incorrectos", botones);
                        break;
                    }
                case 1:
                    {
                        using (Administrar nuevaVentana = new Administrar())
                        {
                            this.Visible = false;
                            nuevaVentana.ShowDialog();
                            this.Close();
                        }
                        break;
                    }
                case 2:
                    {
                        MessageBox.Show("Falto poner usuario o contraseña.", "Campos vacios.", botones);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Sucedio un error\nIntentar nuevamente.", "Error.", botones);
                        break;
                    }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int ingresarCuenta()
        {
            if (txtNombre.Text == "" || txtContrasena.Text == "")
            {
                return 2;
            }
            string nombre = txtNombre.Text;
            string password = txtContrasena.Text;

            string queryBuscar = "SELECT username, passwd FROM usuarios WHERE (username='" + nombre + "'AND passwd=sha2('" + password + "', 384))";

            string MySQLConectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=muebleria";

            MySqlConnection dataBaseConnection = new MySqlConnection(MySQLConectionString);
            MySqlCommand commandDatabase = new MySqlCommand(queryBuscar, dataBaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                dataBaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    return 1;
                    /*
                    while (reader.Read())
                    {
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8) };
                        Console.WriteLine(row.GetValue(2));
                    }
                    */
                }
                
                dataBaseConnection.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return 0;
            }
            return 0;
        }
    }
}
