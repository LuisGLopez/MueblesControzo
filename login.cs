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
            /*
            using (Ventas nuevaVentana = new Ventas())
            {
                this.Visible = false;
                nuevaVentana.ShowDialog();
                this.Visible = true;
            }
            */
            ingresarCuenta();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ingresarCuenta()
        {
            string nombre = txtNombre.Text;
            string password = txtContrasena.Text;

            string queryBuscar = "select * from empleados;";

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
                    while (reader.Read())
                    {
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8) };
                        Console.WriteLine(row.GetValue(2));
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron entradas.");
                }

                dataBaseConnection.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
