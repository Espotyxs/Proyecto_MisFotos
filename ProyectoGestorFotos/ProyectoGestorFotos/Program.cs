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
            while (i < lista.biblioteca.Length)
            {
               // Foto foto = Foto(); ***HAY QUE REVISAR!!
               // Console.WriteLine(Console.WriteLine(foto.fichero + " " + foto.fecha + " " + foto.album + " " + foto.likes))
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
            } */

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

        }
    }
}
