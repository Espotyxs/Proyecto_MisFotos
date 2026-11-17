    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;
    using System.Diagnostics;
    namespace ProyectoGestorFotos
    {
        class Foto
        {
            public string fichero, fecha, album;
            public int likes;

        }
        class ListaFotos
        {
            public Foto[] biblioteca = new Foto[1000]; //decl. de array de objetos
            public int num = 0;
        }
    internal class Program
        {
            static void colorHaciaGris (ListaFotos lista, string nombreFichero)
            {
                try
                {
                    Color[,] imagen = CargaImagen(nombreFichero);
                    int altura = imagen.GetLength(0);
                    int anchura = imagen.GetLength(1);
                    for (int i = 0; i < altura; i++)
                    {
                        for (int j = 0; j < anchura; j++)
                        {
                            Color pixelActual = imagen[i, j];
                            double r = pixelActual.R;
                            double g = pixelActual.G;
                            double b = pixelActual.B;

                            int gris = Convert.ToInt32((r + g + b)/3);
                            int enteroR = Convert.ToInt32(r);
                            int enteroG = Convert.ToInt32(g);
                            int enteroB = Convert.ToInt32(b);
                            imagen[i, j] = Color.FromArgb(gris, gris, gris);
                        }
                    }
                    GuardarImagen(imagen, nombreFichero);
                    Console.WriteLine("Operacion realizada con exito!");
                }
                catch (FileNotFoundException)
                {
                    try
                    {
                        nombreFichero = nombreFichero + ".jpg";
                        Color[,] imagen = CargaImagen(nombreFichero);
                        int altura = imagen.GetLength(0);
                        int anchura = imagen.GetLength(1);
                        for (int i = 0; i < altura; i++)
                        {
                            for (int j = 0; j < anchura; j++)
                            {
                                Color pixelActual = imagen[i, j];
                                double r = pixelActual.R;
                                double g = pixelActual.G;
                                double b = pixelActual.B;

                                r = r * 0.299f;
                                g = g * 0.587f;
                                b = b * 0.114f;
                                int enteroR = Convert.ToInt32(r);
                                int enteroG = Convert.ToInt32(g);
                                int enteroB = Convert.ToInt32(b);
                                imagen[i, j] = Color.FromArgb(enteroR, enteroG, enteroB);
                            }
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("No se ha encontrado el fichero");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Formato de fichero incorrecto; Formato aceptado --> .JPG");
                }
            }
            static Bitmap CrearBitmap(Color[,] pixels)
            {
                int ancho = pixels.GetLength(1);
                int alto = pixels.GetLength(0);
                Bitmap bmp = new Bitmap(ancho, alto);

                for (int y = 0; y < alto; y++)
                {
                    for (int x = 0; x < ancho; x++)
                    {
                        bmp.SetPixel(x, y, pixels[y, x]);
                    }
                }

                return bmp;
            }
            static void GuardarImagen(Color[,] pixels, string nombreFichero) //preguntar a Miguel
            {
                Bitmap imagen = CrearBitmap(pixels);
                imagen.Save(nombreFichero, ImageFormat.Jpeg);
                imagen.Dispose();
            }

            static void MuestraImagenPixels(Color[,] pixels)
            {
                int filas = pixels.GetLength(0);
                int columnas = pixels.GetLength(1);
                Bitmap imagen = new Bitmap(columnas, filas);
                for (int fila = 0; fila < filas; fila++)
                    for (int columna = 0; columna < columnas; columna++)
                        imagen.SetPixel(columna, fila, pixels[fila, columna]);
                Form ventana = new Form();
                ventana.Text = "Imagen";
                ventana.Width = 800;
                ventana.Height = 600;
                PictureBox pictureBox = new PictureBox();
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.Image = imagen;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                ventana.Controls.Add(pictureBox);
                Application.Run(ventana);
                imagen.Dispose();
            }

            static Color[,] CargaImagen(string nombreFichero)
            {
                Bitmap imagen = new Bitmap(nombreFichero);
                Color[,] pixels = new Color[imagen.Height, imagen.Width];
                for (int fila = 0; fila < imagen.Height; fila++)
                    for (int columna = 0; columna < imagen.Width; columna++)
                        pixels[fila, columna] = imagen.GetPixel(columna, fila);

                imagen.Dispose();
                return pixels;

            }
            static void SalvarLista(ListaFotos lista, string fichero)
            {
                try
                {
                    StreamWriter w = new StreamWriter(fichero);
                    int i = 0;
                    while (i < lista.num)
                    {
                        Foto f = lista.biblioteca[i];
                        w.WriteLine("{0} {1} {2} {3}", f.fichero, f.fecha, f.album, f.likes);
                        i++;
                    }
                    w.Close();
                    Console.WriteLine("Operacion efecutada con exito!");
                }
                catch (FileNotFoundException)
                {
                    try
                    {
                        fichero = fichero + ".txt";
                        StreamWriter w = new StreamWriter(fichero);
                        int i = 0;
                        while (i < lista.num)
                        {
                            Foto f = lista.biblioteca[i];
                            w.WriteLine("{0} {1} {2} {3}", f.fichero, f.fecha, f.album, f.likes);
                            i++;
                        }
                        w.Close();
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("No se ha encontrado el fichero");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Fichero en formato incorrecto, formato aceptado --> .TXT");
                }
            }
            static int EliminaFoto(ListaFotos lista, string ficheroImagen)
            {
                bool encontrado = false;
                int i = 0;
                Foto f = new Foto();
                while (i < lista.num)
                {
                    f = lista.biblioteca[i];
                    if (!encontrado)
                    {
                        if (f.fichero == ficheroImagen)
                        {
                            encontrado = true;
                            f = lista.biblioteca[i + 1];
                        }
                        i++;
                    }
                    else
                    {
                        f = lista.biblioteca[i + 1];
                        i++;
                    }
                }
                if (encontrado)
                {
                    lista.num--;
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            static int MuestraImagen (ListaFotos lista, string ficheroImagen)
            {
                bool encontrado = false;
                int i = 0;
                Foto f = new Foto();
                while (i < lista.num && !encontrado)
                {
                    f = lista.biblioteca[i];
                    if (f.fichero == ficheroImagen)
                    {
                        encontrado = true;
                        Process.Start(ficheroImagen);
                        Console.ReadKey();
                        return 0;
                    }
                    i++;
                }
                return -1;
            }
            static int HazLike (ListaFotos lista, string ficheroImagen)
            {
                bool encontrado = false;
                int i = 0;
                Foto f = new Foto();
                while (i < lista.num && !encontrado)
                {
                    f = lista.biblioteca[i];
                    if (f.fichero == ficheroImagen)
                    {
                        f.likes = f.likes + 1;
                        encontrado = true;
                        return 0;
                    }
                    i++;
                }
                return -1;
            }
            static Foto DameFavorita(ListaFotos lista)
            {
                int pos = 0;
                int max = 0;
                int i = 0;
                Foto f = new Foto();
                while (i < lista.num)
                {
                    f = lista.biblioteca[i];
                    if(f.likes > max)
                    {
                        pos = i;
                        max = f.likes;
                    }
                    i++;
                }
                return f = lista.biblioteca[pos];
            }
            static void EscribeFoto(Foto foto)
            {
                Console.WriteLine("Fichero: "+foto.fichero +" Fecha: "+ foto.fecha +" Album: "+ foto.album + " Likes: " + foto.likes);
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
                if (lista == null)
            {
                Console.Write("No hay elementos en la lista");
                return;
            }
                int i = 0;
                Foto foto = new Foto();
                while (i < lista.num)
                {
                    foto = lista.biblioteca[i];
                    Console.WriteLine("Fichero: " + foto.fichero + " Fecha: " + foto.fecha + " Album: " + foto.album + " Likes: " + foto.likes);
                    i++;
                }
            }

            static ListaFotos DameFotosAlbum (ListaFotos lista, string album)
            {
                Foto[] pertenencientes = new Foto[lista.biblioteca.Length];
                int i = 0;
                int cont = 0;
                Foto foto = new Foto();
                while (i < lista.num)
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
                    int e = CargaLista(nuevaLista, fichero);
                    if (e == 0)
                    {
                        Console.WriteLine("Se ha efectuado con exito");
                    }
                    if (e == -1)
                    {
                        Console.WriteLine("No hay espacio en la lista");
                    }
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
                    string[] trozos = input.Split(' ');
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
                else if (input == "5") {
                    Foto f = DameFavorita(nuevaLista);
                    Console.WriteLine(f.fichero);
                }
                else if (input == "6")
                {
                    Console.WriteLine("Introduzca el nombre del fichero que quiere dar un like");
                    string fichero = Console.ReadLine();
                    int res = HazLike(nuevaLista, fichero);
                    if (res == -1)
                    {
                        Console.WriteLine("No se ha encontrado la foto en la lista");
                    }
                    else
                    {
                        Console.WriteLine("Operacion efectuada con exito!");
                    }
                }
                else if (input == "7")
                {
                    Console.WriteLine("Introduzca el nombre del fichero");
                    string fichero = Console.ReadLine();
                    int res = MuestraImagen(nuevaLista, fichero);
                    if (res == -1)
                    {
                        Console.WriteLine("No se ha encontrado la foto en la lista");
                    }
                }
                else if (input == "8")
                {
                    Console.WriteLine("Introduzca el nombre del fichero");
                    string fichero = Console.ReadLine();
                    int res = EliminaFoto(nuevaLista, fichero);
                    if (res == -1)
                    {
                        Console.WriteLine("No se ha encontrado la foto en la lista");
                    }
                    else
                    {
                        Console.WriteLine("Operacion efectuada con exito!");
                    }
                }
                else if (input == "9")
                {
                    Console.WriteLine("Introduzca el nombre del fichero");
                    string fichero = Console.ReadLine();
                    SalvarLista(nuevaLista, fichero);
                }
                else if (input == "10")
                {
                    Console.WriteLine("Introduzca el nombre del fichero");
                    string fichero = Console.ReadLine();
                    colorHaciaGris(nuevaLista, fichero);
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
                        f.fichero = trozos[0];
                        f.fecha = trozos[1];
                        f.album = trozos[2];
                        f.likes = Convert.ToInt32(trozos[3]);
                        lista.biblioteca[lista.num] = f;
                        lista.num = lista.num + 1;
                        linea = r.ReadLine();
                    }
                    r.Close();
                    return 0;
                }
                catch (IndexOutOfRangeException)
                {
                    return -1;
                }
                catch (FileNotFoundException)
                {
                    return -3;
                }
                catch (FormatException)
                {
                    return -2;
                }
            }
            static void Main(string[] args)
            {
                ListaFotos generica = new ListaFotos();
                string input = "";
                while (input != "0")
                {
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("---------------------Terminal de Fotos----------------------------");
                    Console.WriteLine("Introduzca la accion que quiere realizar");
                    Console.WriteLine("0 - Salir del Programa");
                    Console.WriteLine("1 - Cargar una lista de fotos con los datos de un fichero cuyo nombre introduce el usuario por teclado");
                    Console.WriteLine("2 - Escribir en consola los datos de todas las fotos de la lista");
                    Console.WriteLine("3 - Escribir en consola los datos de las fotos pertenecientes a un álbum cuyo nombre introduce el usuario por teclado");
                    Console.WriteLine("4 - Añadir a la lista una foto cuyos datos introduce el usuario por teclado");
                    Console.WriteLine("5 - Mostrar en consola los datos de la foto favorita");
                    Console.WriteLine("6 - Sumar 1 al número de likes de la foto identificada con el nombre del fichero que contiene su imagen, que introduce el usuario por teclado");
                    Console.WriteLine("7 - Mostrar al usuario la imagen de la foto identificada con el nombre del fichero que contiene su imagen, que introduce el usuario por teclado");
                    Console.WriteLine("8 - Eliminar la foto identificada con el nombre del fichero que contiene su imagen,que introduce el usuario por teclado");
                    Console.WriteLine("9 - Salvar la lista de fotos en el fichero cuyo nombre introduce el usuario por teclado");
                    Console.WriteLine("10 - Pasar foto de color a escala de grises");
                    input = Console.ReadLine();
                    Console.Clear();
                    Menu(generica, input);
                }
            }
        }
    }