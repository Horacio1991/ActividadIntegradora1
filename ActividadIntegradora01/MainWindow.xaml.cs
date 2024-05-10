using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ActividadIntegradora01
{
   
    public partial class MainWindow : Window
    {
        // Lista donde se van a guardar las Personas
        private List<Persona> listaDePersonas = new List<Persona>();

        // Archivo para la persistencia de datos
        private string archivoDatos = "datos.txt";
     
        public MainWindow()
        {

            InitializeComponent();
            CargarDatosDesdeArchivo();
            LlenarDataGridConLista();

            //Al presionar escape se limpiaran los campos del formulario
            this.KeyDown += Window_KeyDown;

        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LlenarDataGridConLista()
        {
            // Llenar el DataGrid con los datos de la lista de personas
            dgPersonas.ItemsSource = listaDePersonas;
        }

        private void CargarDatosDesdeArchivo()
        {
            if (File.Exists(archivoDatos))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(archivoDatos))
                    {
                        string linea;
                        while ((linea = sr.ReadLine()) != null)
                        {
                            string[] partes = linea.Split(',');
                            if (partes.Length == 3)
                            {
                                listaDePersonas.Add(new Persona(partes[0], partes[1], partes[2]));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar datos desde el archivo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
           
        }

        private void GuardarDatosEnArchivo()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(archivoDatos))
                {
                    foreach (Persona persona in listaDePersonas)
                    {
                        sw.WriteLine($"{persona.DNI},{persona.Nombre},{persona.Apellido}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar datos en el archivo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void LimpiarFormulario()
        {
            txtDNI.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
        }

        //Boton Agregar
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si todos los campos están completos
            if (string.IsNullOrWhiteSpace(txtDNI.Text) || string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Campos Incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Verificar si el DNI contiene solo números
            if (!txtDNI.Text.All(char.IsDigit))
            {
                MessageBox.Show("El campo DNI solo puede contener números.", "DNI Inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Verificar si el nombre contiene solo letras
            if (!txtNombre.Text.All(char.IsLetter))
            {
                MessageBox.Show("El campo Nombre solo puede contener letras.", "Nombre Inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Verificar si el apellido contiene solo letras
            if (!txtApellido.Text.All(char.IsLetter))
            {
                MessageBox.Show("El campo Apellido solo puede contener letras.", "Apellido Inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Se instancia la clase persona con los datos ingresados en el formulario
            Persona nuevaPersona = new Persona(txtDNI.Text, txtNombre.Text, txtApellido.Text);

            // Agregar la persona al DataGrid y a la lista
            listaDePersonas.Add(nuevaPersona);
            // Carga la grilla con los datos de la lista listaDePersonas
            dgPersonas.ItemsSource = listaDePersonas;
            // Actualiza los items para mostrarlos en pantalla
            dgPersonas.Items.Refresh();
           
            GuardarDatosEnArchivo();

            LimpiarFormulario();
        }

        //Boton Buscar
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Obtener el DNI ingresado 
            string dniABuscar = txtDNI.Text;

            // Crear una nueva instancia de Persona con el DNI ingresado
            Persona personaABuscar = new Persona(dniABuscar, "", "");

            // Verificar si se encuentra la persona en la grilla
            if (personaABuscar.BuscarEnGrilla(dgPersonas))
            {
                // Obtener la persona encontrada en la grilla
                Persona personaEncontrada = dgPersonas.Items.Cast<Persona>().FirstOrDefault(persona => persona.DNI == dniABuscar);

                // Llenar los campos nombre y apellido de la persona encontrada
                txtNombre.Text = personaEncontrada.Nombre;
                txtApellido.Text = personaEncontrada.Apellido;

                // Mostrar un MessageBox con los detalles de la persona encontrada
                MessageBox.Show($"DNI: {personaEncontrada.DNI}\nNombre: {personaEncontrada.Nombre}\nApellido: {personaEncontrada.Apellido}",
                                "Persona Encontrada", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Mostrar un mensaje indicando que no se encontró la persona
                MessageBox.Show("No se encontró la persona con el DNI proporcionado.", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        // limpiar el fornulario con la tecla escape
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Verificar si se presionó la tecla Escape
            if (e.Key == Key.Escape)
            {
                // Limpiar los campos de texto
                LimpiarFormulario();
            }
        }
    }
}
