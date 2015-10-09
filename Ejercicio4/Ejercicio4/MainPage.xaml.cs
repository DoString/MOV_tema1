using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Ejercicio4
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Grid tablero; //Rejilla
        private const int MAX_COL_ROW = 8; //Tablero 8x8
        private Rectangle rec; //una casilla es un rectangulo
        /// <summary>
        /// Colores para las casillas:
        /// </summary>
        private Color colorA;
        private Color colorB;
        /// <summary>
        /// Utilizamos dos matrices , una con los colores normales 
        /// y otra con los colores invertidos:
        /// </summary>
        private Color[,] coloresNormales;
        private Color[,] coloresInvertidos;
        private Color[,] coloresArbitrarios;
        /// <summary>
        /// Enumera los tipos de colores a elegir
        /// </summary>
        public enum TipoColor
        {
            NORMAL,
            INVERTIDO,
            ARBITRARIO,
        };
        /// <summary>
        /// Byte Alfa del color
        /// </summary>
        private const byte ALFA_FULL = 0xFF;
        private TipoColor _seleccionColor;
        private int _clicks = 0; // cuenta los clicks del botón.
        /// <summary>
        /// Obtiene o establece el tipo de color
        /// </summary>
        public TipoColor TipoColores
        {
            get
            {
                return _seleccionColor;
            }

            set
            {
                this._seleccionColor = value;
            }
        }

        // Constructor
        public MainPage()
        {
            //Se rellenan los arrays de colores:
            CrearMatrizColores();
            InitializeComponent();
        }

        private void IncrementarClicks()
        {
            this._clicks = this._clicks >= (int.MaxValue - 1) ? 1 : (this._clicks + 1);
        }

        private void VaciarColumnasYFilas()
        {
            this.tablero.ColumnDefinitions.Clear();
            this.tablero.RowDefinitions.Clear();
        }


        private void CrearMatrizColores()
        {
            this.coloresArbitrarios = new Color[MAX_COL_ROW, MAX_COL_ROW];
            Random rand = new Random();
            byte[] rgb = new byte[3]; //Bytes: Rojo,Verde,Azul.

            switch (TipoColores)
            {
                case TipoColor.NORMAL:
                    this.colorA = Colors.White;
                    this.colorB = Colors.Black;
                    break;
                case TipoColor.INVERTIDO:                    
                    Color temp = this.colorA;
                    this.colorA = this.colorB;
                    this.colorB = temp;
                    break;
                case TipoColor.ARBITRARIO:
                    //Se crea un color aleatorio
                    rand.NextBytes(rgb);
                    this.colorA = Color.FromArgb(ALFA_FULL, rgb[0], rgb[1], rgb[2]);
                    //Se crea un segundo color aleatorio.
                    rand.NextBytes(rgb);
                    this.colorB = Color.FromArgb(ALFA_FULL, rgb[0], rgb[1], rgb[2]);
                    break;
                default:
                    break;
            }


            for (int i = 0; i < MAX_COL_ROW; i++)
            {
                for (int j = 0; j < MAX_COL_ROW; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                            this.coloresArbitrarios[i, j] = this.colorB;
                        else
                            this.coloresArbitrarios[i, j] = this.colorA;
                    }
                    else
                    {
                        if (j % 2 == 0)
                            this.coloresArbitrarios[i, j] = this.colorA;
                        else
                            this.coloresArbitrarios[i, j] = this.colorB;
                    }
                }
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.CrearTablero();
        }

        private void CrearTablero()
        {
            this.tablero = this.grdTablero;
            //Se borran las filasy columnas;
            this.VaciarColumnasYFilas();
            //Se añaden 8 columnas y 8 filas al tablero:
            AnadirColumnasYFilas();
            //Se vacia la lista de hijos:
            this.tablero.Children.Clear();
            //Se añade un rectangulo a cada celda de la rejilla:
            AnadirRectangulos();
        }

        private void AnadirRectangulos()
        {
            for (int i = 0; i < this.tablero.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < this.tablero.ColumnDefinitions.Count; j++)
                {
                    this.rec = new Rectangle();
                    this.rec.SetValue(Grid.ColumnProperty, j);
                    this.rec.SetValue(Grid.RowProperty, i);
                    this.rec.Fill = new SolidColorBrush(this.coloresArbitrarios[i, j]);
                    this.tablero.Children.Add(this.rec);
                }
            }
        }

        private void AnadirColumnasYFilas()
        {
            for (int i = 0; i < MAX_COL_ROW; i++)
            {
                this.tablero.ColumnDefinitions.Add(new ColumnDefinition());
                this.tablero.RowDefinitions.Add(new RowDefinition());
            }
        }

        private void btnInvertir_Click(object sender, RoutedEventArgs e)
        {
            //Incrementa la cuenta de clicks:
            this.IncrementarClicks();
            this.TipoColores = TipoColor.INVERTIDO;
            this.CrearMatrizColores();
            this.CrearTablero();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.TipoColores = TipoColor.ARBITRARIO;
            this.CrearMatrizColores();
            this.CrearTablero();
        }

        private void btnResetearColor_Click(object sender, RoutedEventArgs e)
        {
            this.TipoColores = TipoColor.NORMAL;
            this.CrearMatrizColores();
            this.CrearTablero();
        }
    }
}