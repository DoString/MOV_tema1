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
using System.Globalization;
using System.Text;

namespace Ejercicio7
{
    public partial class MainPage : PhoneApplicationPage
    {
        /// <summary>
        /// Los botones de la calculadora
        /// </summary>
        private enum Botones
        {
            BOTON_0,
            BOTON_1,
            BOTON_2,
            BOTON_3,
            BOTON_4,
            BOTON_5,
            BOTON_6,
            BOTON_7,
            BOTON_8,
            BOTON_9,
            BOTON_SUMA,
            BOTON_RES,
            BOTON_MENOS,
            BOTON_X,
            BOTON_DIV,
            BOTON_CE,
            BOTON_MASMENOS,
            BOTON_DECIMALES
        };
        /// <summary>
        /// La pantalla de la calculadora
        /// </summary>
        private TextBox _pantalla;

        private int _operando_1_len;
        private int _operando_2_len;
        private string[] _operaciones;
        private const int MAX_OPERACIONES = 100;
        private const double TAMANIO_FUENTE = 72.0;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            _pantalla = this.textBox1;
            //
            _operaciones = new string[MAX_OPERACIONES];
        }

        private void LeerPantalla()
        {

        }

        private void Botones_Click(object sender, RoutedEventArgs e)
        {
            HacerAccionBoton(sender, _pantalla);
        }

        private string expresion = "";
        private char nuevo = ' ';
        private char[] operadores = { '+', '-', '*', '/' };
        private bool esNegativo = false;
        private bool signoDecimalPuesto = false;
        private bool signoMenosPuesto = false;
        /// <summary>
        /// Ejecuta la acción programada para cada botón
        /// </summary>
        /// <param name="sender"></param>
        private void HacerAccionBoton(object sender, object pantalla)
        {
            Button boton = sender as Button;

            if (boton != null)
            {
                Botones tags = (Botones)Enum.Parse(typeof(Botones), boton.Tag.ToString(), false);

                //Si existe el formato notación científica en el texto de la pantalla,
                //hay que pasarlo a decimal
                //para poder realizar las operaciones aritméticas
                PasarDeExponencialADecimal();

                //utilizaremos este char para comprobar repeticiones de signos etc..
                char ultimoChar;
                try
                {
                    ultimoChar = _pantalla.Text[_pantalla.Text.Length - 1];
                }
                catch (Exception)
                {
                    ultimoChar = '\0';
                }

                switch (tags)
                {
                    case Botones.BOTON_0:
                        _pantalla.Text += boton.Content;
                        break;
                    case Botones.BOTON_1:
                        _pantalla.Text += boton.Content;
                        break;
                    case Botones.BOTON_2:
                        _pantalla.Text += boton.Content;
                        break;
                    case Botones.BOTON_3:
                        _pantalla.Text += boton.Content;
                        break;
                    case Botones.BOTON_4:
                        _pantalla.Text += boton.Content;
                        break;
                    case Botones.BOTON_5:
                        _pantalla.Text += boton.Content;
                        break;
                    case Botones.BOTON_6:
                        _pantalla.Text += boton.Content;
                        break;
                    case Botones.BOTON_7:
                        _pantalla.Text += boton.Content;
                        break;
                    case Botones.BOTON_8:
                        _pantalla.Text += boton.Content;
                        break;
                    case Botones.BOTON_9:
                        _pantalla.Text += boton.Content;
                        break;
                    case Botones.BOTON_SUMA:
                        if (!char.IsNumber(ultimoChar) &&
                            ultimoChar == '+' ||
                            ultimoChar == '-' ||
                            ultimoChar == '/' ||
                            ultimoChar == '*')
                        {
                            _pantalla.Text = _pantalla.Text.Substring(0, (_pantalla.Text.Length - 1)) + '+';
                            signoDecimalPuesto = false;
                            signoMenosPuesto = false;
                            break;
                        }
                        _pantalla.Text += boton.Content;
                        signoDecimalPuesto = false;
                        signoMenosPuesto = false;
                        break;
                    case Botones.BOTON_X:
                        if (!char.IsNumber(ultimoChar) &&
                            ultimoChar == '+' ||
                            ultimoChar == '-' ||
                            ultimoChar == '/' ||
                            ultimoChar == '*')
                        {
                            _pantalla.Text = _pantalla.Text.Substring(0, (_pantalla.Text.Length - 1)) + '*';
                            signoDecimalPuesto = false;
                            signoMenosPuesto = false;
                            break;
                        }
                        _pantalla.Text += boton.Content;
                        signoDecimalPuesto = false;
                        signoMenosPuesto = false;
                        break;
                    case Botones.BOTON_DIV:
                        if (!char.IsNumber(ultimoChar) &&
                            ultimoChar == '/' ||
                            ultimoChar == '-' ||
                            ultimoChar == '+' ||
                            ultimoChar == '*')
                        {
                            _pantalla.Text = _pantalla.Text.Substring(0, (_pantalla.Text.Length - 1)) + '/';
                            signoDecimalPuesto = false;
                            signoMenosPuesto = false;
                            break;
                        }
                        _pantalla.Text += boton.Content;
                        signoDecimalPuesto = false;
                        signoMenosPuesto = false;
                        break;
                    case Botones.BOTON_MENOS:
                        if (!char.IsNumber(ultimoChar) &&
                            ultimoChar == '-' ||
                            ultimoChar == '+' ||
                            ultimoChar == '/' ||
                            ultimoChar == '*')
                        {
                            _pantalla.Text = _pantalla.Text.Substring(0, (_pantalla.Text.Length - 1)) + '-';
                            signoDecimalPuesto = false;
                            signoMenosPuesto = false;
                            break;
                        }
                        _pantalla.Text += boton.Content;
                        signoDecimalPuesto = false;
                        break;
                    case Botones.BOTON_CE: 
                        _pantalla.Text = ""; _pantalla.FontSize = TAMANIO_FUENTE; 
                        signoDecimalPuesto = false;
                        signoMenosPuesto = false;
                        break;
                    case Botones.BOTON_DECIMALES:
                        if (signoDecimalPuesto) break;
                        if (string.IsNullOrEmpty(_pantalla.Text) ||
                            !char.IsNumber(ultimoChar))
                            _pantalla.Text += "0,";
                        else if (ultimoChar == ',')
                            break;
                        else
                            _pantalla.Text += ",";
                        signoDecimalPuesto = true;
                        break;
                    case Botones.BOTON_MASMENOS:
                        if (signoMenosPuesto)
                            APositivo();
                        else
                            ANegativo();
                        break;
                    case Botones.BOTON_RES:
                        try
                        {
                            ExpresionMat.CheckRegex(_pantalla.Text);
                        }
                        catch (ExpresionMalFormadaException ex)
                        {
                            MessageBox.Show(ex.Message);
                            break;
                        }
                        _pantalla.Text = ExpresionMat.CheckMatOp(_pantalla.Text);
                        if (_pantalla.Text.StartsWith("-"))
                            signoMenosPuesto = true;
                        else
                            signoMenosPuesto = false;
                        signoDecimalPuesto = false;
                        break;
                    default:
                        break;
                }
            }
        }

        private void ANegativo()
        {
            int len = _pantalla.Text.Length;

            if (len <= 0 || 
                _pantalla.Text == "0")
                return;

            string texto = "-";
            string aux = string.Empty;

            int indiceF = len - 1;
            bool digito = char.IsDigit(_pantalla.Text[indiceF]);

            //Se buscan los últimos chars que sean digitos
            while (indiceF >= 0 && digito)
            {
                if (digito = char.IsDigit(_pantalla.Text[indiceF]) ||
                    _pantalla.Text[indiceF] == ',')
                    indiceF--;
            } indiceF++;

            //Si el indice no se ha movido es que no ha encontrado 
            //digitos
            if (indiceF == len) return;

            int parteNegativa = (len) - indiceF;
            int parteNormal = (len - parteNegativa);

            aux = _pantalla.Text.Substring(0, len - parteNegativa);
            texto += _pantalla.Text.Substring(len - parteNegativa);

            _pantalla.Text = aux == "" ? texto : aux + texto;
            signoMenosPuesto = true;
        }

        private void APositivo()
        {            
            char menos = '\0';
            int i = _pantalla.Text.Length - 1;
            while (menos != '-')
            {
                menos = _pantalla.Text[i--];
            } i++;
            string parteInicial = _pantalla.Text.Substring(0, i);
            _pantalla.Text = parteInicial + _pantalla.Text.Substring(i).Replace("-", ""); // Parte inicial + Parte final del string.
            signoMenosPuesto = false;
        }

        /// <summary>
        /// Establece el valor del texto de la pantalla en formato decimal
        /// desde formato Exponencial
        /// </summary>
        /// <param name="n"></param>
        private void PasarDeExponencialADecimal()
        {
            decimal n = 0;
            if (_pantalla.Text.Contains("E+"))
                decimal.TryParse(_pantalla.Text, NumberStyles.Float, NumberFormatInfo.CurrentInfo, out n);
            _pantalla.Text = n != 0 ? n.ToString() : _pantalla.Text;
        }

        //Constantes para tamaño de la letra
        private const int MAX_CHARS_VERTICAL = 10;
        private const double FACTOR_CONVERSION = 1.5;

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
            double tamanio_letra = t.FontSize;
            if (t != null)
                switch ((int)tamanio_letra)
                {
                    case 72:
                        if (t.Text.Length >= 10)
                            t.FontSize = tamanio_letra / FACTOR_CONVERSION;
                        break;
                    case 48:
                        if (t.Text.Length >= 16)
                            t.FontSize = tamanio_letra / FACTOR_CONVERSION;
                        break;
                    case 32:
                        if (t.Text.Length >= 24)
                            t.FontSize = tamanio_letra / FACTOR_CONVERSION;
                        break;
                    default:
                        t.TextWrapping = TextWrapping.Wrap;
                        break;
                }
        }

        private void CalcularOperacion(string o1, string o2, string op)
        {
            double opr1 = Double.Parse(o1);
            double opr2 = Double.Parse(o2);

            switch (op)
            {
                case "+": expresion = (opr1 + opr2).ToString();
                    break;
                default:
                    break;
            }
        }
    }
}