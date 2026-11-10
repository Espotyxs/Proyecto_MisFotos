namespace ProyectoGestorFotos
{
    class Foto
    {
        public string fichero, fecha, album;
        public int likes;

    }
    class ListaFotos
    {
        public Foto[] biblioteca = new Foto[100]; //decl. de array de objetos
        public int num = 0;
    }
internal class Program
    {
        static void EscribeFoto(Foto foto)
        {
            Console.WriteLine(foto.fichero +" "+ foto.fecha +" "+ foto.album + " " + foto.likes);
        }
        static int PonFoto (ListaFotos lista, Foto foto)
        {
            try
            {
                lista.biblioteca[lista.num] = foto;
                lista.num = lista.num + 1;
                return 0;
            }
            catch (IndexOutOfRangeException)
            {
                return -1;
            }
        }
        static void EscribeLista (ListaFotos lista)
        {
            int i = 0;
            Foto foto = new Foto();
            while (i < lista.biblioteca.Length)
            {
               foto = lista.biblioteca[i];
               Console.WriteLine(foto.fichero + " " + foto.fecha + " " + foto.album + " " + foto.likes);
                i++;
            }
        }

        static ListaFotos DameFotosAlbum (ListaFotos lista, string album)
        {
            Foto[] pertenencientes = new Foto[lista.biblioteca.Length];
            int i = 0;
            int cont = 0;
            Foto foto = new Foto();
            while (i < lista.biblioteca.Length)
            {
                foto = lista.biblioteca[i];
                if (foto.album == album)
                {
                    pertenencientes[cont] = foto;
                    cont++;
                }
                i++;
            }
            if (cont != 0)
            {
                ListaFotos filtrado = new ListaFotos();
                filtrado.biblioteca = pertenencientes;
                return filtrado;
            }
            else
            {
                return null;
            }
        }
        static void Menu(ListaFotos nuevaLista, string input)
        {
            
            if (input == "0")
            {

            }
            else if (input == "1")
            {
                Console.WriteLine("Introduzca el nombre del fichero");
                string fichero = Console.ReadLine();
                CargaLista(nuevaLista, fichero);
            }
            else if (input == "2")
            {
                EscribeLista(nuevaLista);
            }
            else if (input == "3")
            {
                Console.WriteLine("Introduzca el nombre del album");
                string album = Console.ReadLine();
                EscribeLista(DameFotosAlbum(nuevaLista, album));
            }
            else if (input == "4")
            {
                Foto f = new Foto();
                Console.WriteLine("Introduzca separado por espacios la siguiente informacion");
                Console.WriteLine("NombreFichero, fecha, album, likes");
                input = Console.ReadLine();
                string[] trozos = input.Split();
                f.fichero = trozos[0];
                f.fecha = trozos[1];
                f.album = trozos[2];
                f.likes = Convert.ToInt32(trozos[3]);
                if (PonFoto(nuevaLista, f) == 0)
                {
                    Console.WriteLine("Se ha efectuado la operacion");
                }
                else
                {
                    Console.WriteLine("No se ha podido efectuar la operacion correctamente");
                }

            }
            else
            {
                Console.WriteLine("Insercion incorrecta");
            }
        }
        static int CargaLista (ListaFotos lista, string nombreFichero)
        {
            try
            {
                StreamReader r = new StreamReader(nombreFichero);
                string linea = r.ReadLine();
                while(linea != null)
                {
                    string[] trozos = linea.Split(' ');
                    Foto f = new Foto();
                    f.fichero = nombreFichero;
                    f.fecha = trozos[0];
                    f.album = trozos[1];
                    f.likes = Convert.ToInt32(trozos[2]);
                    lista.biblioteca[lista.num] = f;
                    lista.num = lista.num + 1;
                    linea = r.ReadLine();
                }
                r.Close();
                return 0;
            }
            catch (IndexOutOfRangeException)
            {
                return -3;
            }
            catch (FileNotFoundException)
            {
                return -1;
            }
            catch (FormatException)
            {
                return -2;
            }
        }
        static void Main(string[] args)
        {
            /*ListaFotos galeria = new ListaFotos(); // Comprobacion PASO 1
            while (galeria.num < 3)
            {
                Console.WriteLine("Introduzca los datos de la foto separados por espacio (Nombre, Fecha, Album, Likes");
                string linea = Console.ReadLine();
                string[] trozos = linea.Split();
                Foto f = new Foto();
                f.fichero = trozos[0];
                f.fecha = trozos[1];
                f.album = trozos[2];
                f.likes = Convert.ToInt32(trozos[3]);
                galeria.biblioteca[galeria.num] = f;
                galeria.num = galeria.num + 1;  
            } 
            
            ListaFotos lista = new ListaFotos();
            Foto f = new Foto();
            string linea = "Fich 210 WAS 9";
            string[] trozos = linea.Split();
            f.fichero = trozos[0];
            f.fecha = trozos[1];
            f.album = trozos[2];
            f.likes = Convert.ToInt32(trozos[3]);
            lista.biblioteca[lista.num] = f;
            lista.num = lista.num + 1;
            linea = "Trr 821 Pdsa 4";
            trozos = linea.Split();
            f.fichero = trozos[0];
            f.fecha = trozos[1];
            f.album = trozos[2];
            f.likes = Convert.ToInt32(trozos[3]);
            lista.biblioteca[lista.num] = f;
            EscribeFoto(f);
            */
            ListaFotos generica = new ListaFotos();
            string input = "";
            while (input != "0")
            {
                Console.WriteLine("Introduzca la accion que quiere realizar");
                Console.WriteLine("0 - Salir del Programa");
                Console.WriteLine("1 - Cargar una lista de fotos con los datos de un fichero cuyo nombre introduce el usuario por teclado");
                Console.WriteLine("2 - Escribir en consola los datos de todas las fotos de la lista");
                Console.WriteLine("3 - Escribir en consola los datos de las fotos pertenecientes a un álbum cuyo nombre introduce el usuario por teclado");
                Console.WriteLine("4 - Añadir a la lista una foto cuyos datos introduce el usuario por teclado");
                input = Console.ReadLine();
                Menu(generica, input);

            }
        }
    }
}
