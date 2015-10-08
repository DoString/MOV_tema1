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
            get { return _seleccionColor; }
            set
            {
                if (_clicks % 2 == 0)
                    _seleccionColor = TipoColor.INVERTIDO;
                else if (_clicks % 2 != 0)
                    _seleccionColor = TipoColor.NORMAL;
                else
                    _seleccionColor = value;
            }
        }


        // Constructor
        public MainPage()
        {
            //Se rellenan los arrays de colores:
            this.coloresNormales = new Color[MAX_COL_ROW, MAX_COL_ROW];
            this.coloresInvertidos = new Color[MAX_COL_ROW, MAX_COL_ROW];
            CrearMatrizColores();
            InitializeComponent();
        }



        private void CrearMatrizColores()
        {
            this.coloresArbitrarios = new Color[MAX_COL_ROW, MAX_COL_ROW];


            switch (TipoColores)
            {
                case TipoColor.NORMAL:
                    this.colorA = Colors.White;
                    this.colorB = Colors.Black;
                    break;
                case TipoColor.INVERTIDO:
                    this.colorA = Colors.Black;
                    this.colorB = Colors.White;
                    break;
                case TipoColor.ARBITRARIO: //poner colores.
                    this.colorA = Colors.White;
                    this.colorB = Colors.Black;
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
            this.tablero = this.grdTablero;
            //Se añaden 8 columnas y 8 filas al tablero:
            AnadirColumnasYFilas();
            //Se vacia la lista de hijos:
            this.tablero.Children.Clear();
            //Se añade un rectangulo a cada celda de la rejilla:
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
            this._clicks = this._clicks >= (int.MaxValue - 1) ? 1 : (this._clicks + 1);
            this.CrearMatrizColores();
            this.PhoneApplicationPage_Loaded(sender, e);
        }
    }
}