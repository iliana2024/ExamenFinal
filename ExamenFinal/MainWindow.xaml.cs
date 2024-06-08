using ExamenFinal;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Windows;

namespace MantenimientoAlumnos
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Windows;

    namespace AlumnoApp
    {
        public partial class MainWindow : Window
        {
            private string connectionString = "your_connection_string_here";

            public MainWindow()
            {
                InitializeComponent();
                CargarUsuarios();
            }

            private void CargarUsuarios()
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SELECT usuario, nombre FROM usuarios", con);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            cmbUsuario.Items.Add(new { Usuario = reader["usuario"], Nombre = reader["nombre"] });
                        }
                        cmbUsuario.DisplayMemberPath = "Nombre";
                        cmbUsuario.SelectedValuePath = "Usuario";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al cargar usuarios: " + ex.Message);
                    }
                }
            }

            private void btnCrear_Click(object sender, RoutedEventArgs e)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO alumnos (carnet, nombre, telefono, grado, usuario) VALUES (@carnet, @nombre, @telefono, @grado, @usuario)", con);
                        cmd.Parameters.AddWithValue("@carnet", txtCarnet.Text);
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                        cmd.Parameters.AddWithValue("@grado", txtGrado.Text);
                        cmd.Parameters.AddWithValue("@usuario", ((dynamic)cmbUsuario.SelectedItem).Usuario);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Alumno creado exitosamente");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al crear alumno: " + ex.Message);
                    }
                }
            }

            private void btnLeer_Click(object sender, RoutedEventArgs e)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SELECT * FROM alumnos WHERE carnet = @carnet", con);
                        cmd.Parameters.AddWithValue("@carnet", txtCarnet.Text);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtNombre.Text = reader["nombre"].ToString();
                            txtTelefono.Text = reader["telefono"].ToString();
                            txtGrado.Text = reader["grado"].ToString();
                            cmbUsuario.SelectedValue = reader["usuario"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Alumno no encontrado");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al leer alumno: " + ex.Message);
                    }
                }
            }

            private void btnActualizar_Click(object sender, RoutedEventArgs e)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE alumnos SET nombre = @nombre, telefono = @telefono, grado = @grado, usuario = @usuario WHERE carnet = @carnet", con);
                        cmd.Parameters.AddWithValue("@carnet", txtCarnet.Text);
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                        cmd.Parameters.AddWithValue("@grado", txtGrado.Text);
                        cmd.Parameters.AddWithValue("@usuario", ((dynamic)cmbUsuario.SelectedItem).Usuario);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Alumno actualizado exitosamente");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar alumno: " + ex.Message);
                    }
                }
            }

            private void btnBorrar_Click(object sender, RoutedEventArgs e)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM alumnos WHERE carnet = @carnet", con);
                        cmd.Parameters.AddWithValue("@carnet", txtCarnet.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Alumno borrado exitosamente");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al borrar alumno: " + ex.Message);
                    }
                }
            }
        }
    }

}
    }
}
