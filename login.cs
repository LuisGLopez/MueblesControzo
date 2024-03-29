﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BancoControzo
{
    public partial class Login : Form
    {
        string tipoUsuarios = "";

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
                        switch (tipoUsuarios)
                        {
                            case "Administrador":
                                {
                                    using (Administrar nuevaVentana = new Administrar())
                                    {
                                        this.Visible = false;
                                        nuevaVentana.ShowDialog();
                                        this.Close();
                                    }
                                    break;
                                }
                            case "Ventas":
                                {
                                    using (Ventas nuevaVentana = new Ventas())
                                    {
                                        this.Visible = false;
                                        nuevaVentana.ShowDialog();
                                        this.Close();
                                    }
                                    break;
                                }
                            case "Inventario":
                                {
                                    using (Inventario nuevaVentana = new Inventario())
                                    {
                                        this.Visible = false;
                                        nuevaVentana.ShowDialog();
                                        this.Close();
                                    }
                                    break;
                                }
                            default:
                                MessageBox.Show("Sucedio un error\nIntentar nuevamente.", "Error.", botones);
                                break;
                        }
                        
                        break;
                    }
                case 2:
                    {
                        MessageBox.Show("Falto poner usuario/contraseña\nO la longitud de los datos es muy largo.", "Campos vacios o exceden la longitud..", botones);
                        break;
                    }
                case 3:
                    {
                        MessageBox.Show("El nombre de usuario cuenta con caracteres diferentes a letras", "Nombre con caracteres especiales.", botones);
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
            if (txtNombre.Text == "" || txtContrasena.Text == "" || txtNombre.Text.Length > 127 || txtContrasena.Text.Length > 99)
            {
                return 2;
            }

            if ( !Regex.IsMatch(txtNombre.Text, @"^[\p{L}]+$") )
            {
                return 3;
            }

            string nombre = txtNombre.Text;
            string password = txtContrasena.Text;

            string queryBuscar = "SELECT tipo_usuario FROM usuarios WHERE (username='" + nombre + "'AND passwd=sha2('" + password + "', 384))";

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
                        tipoUsuarios = reader.GetString(0);
                    }

                    dataBaseConnection.Close();

                    return 1;
                    /*
                    
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
