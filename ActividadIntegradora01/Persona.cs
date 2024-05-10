using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ActividadIntegradora01
{
    public class Persona
    {
    
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public Persona(string dni, string nombre, string apellido)
        {
            this.DNI = dni;
            this.Nombre = nombre;
            this.Apellido = apellido;
        }

        ~Persona()
        {
            Console.WriteLine($"Persona con DNI {DNI} ha sido liberada.");
        }

        public bool BuscarEnGrilla(DataGrid dataGrid)
        {
            foreach (Persona persona in dataGrid.Items)
            {
                if (persona.DNI == DNI)
                {
                    return true;
                }
            }
            return false;
        }
    }

}
