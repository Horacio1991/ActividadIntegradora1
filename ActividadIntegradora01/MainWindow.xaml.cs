using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Persona> listaDePersonas = new List<Persona>();
        public MainWindow()
        {
            listaDePersonas.Add(new Persona("36084790", "Horacio", "Ortiz"));
            listaDePersonas.Add(new Persona("36168344", "Belen", "Uronich"));
            listaDePersonas.Add(new Persona("53815805", "Charo", "Ortiz"));
            listaDePersonas.Add(new Persona("33132843", "Yamila", "Uronich"));
            listaDePersonas.Add(new Persona("33453123", "Maxi", "Trabado"));
            listaDePersonas.Add(new Persona("50.123.321", "Zoe", "Freancia"));



            InitializeComponent();
            LlenarDataGridConLista();
        }

        private void LlenarDataGridConLista()
        {
            // Llenar el DataGrid con los datos de la lista de personas
            dgPersonas.ItemsSource = listaDePersonas;
        }

        private void LimpiarFormulario()
        {
            txtDNI.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
        }

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

            // Crear una nueva persona con los datos del formulario
            Persona nuevaPersona = new Persona(txtDNI.Text, txtNombre.Text, txtApellido.Text);

            // Agregar la persona al DataGrid y a la lista
            
            listaDePersonas.Add(nuevaPersona);
            dgPersonas.ItemsSource = listaDePersonas;
            dgPersonas.Items.Refresh();




            // Limpiar los campos de texto del formulario
            LimpiarFormulario();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Obtener el DNI ingresado en el campo txtDNI
            string dniABuscar = txtDNI.Text;

            // Crear una nueva instancia de Persona con el DNI ingresado
            Persona personaABuscar = new Persona(dniABuscar, "", "");

            // Verificar si se encuentra la persona en el DataGrid
            if (personaABuscar.BuscarEnGrilla(dgPersonas))
            {
                // Obtener la persona encontrada en el DataGrid
                Persona personaEncontrada = dgPersonas.Items.Cast<Persona>().FirstOrDefault(persona => persona.DNI == dniABuscar);

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
    }
}
