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
using System.Security.Cryptography;
using System.Text;

namespace ejercicio6
{
    public partial class MainPage : PhoneApplicationPage
    {
        /// <summary>
        /// El usuario 
        /// </summary>
        private const string USUARIO = "usuario";
        /// <summary>
        /// Es el password sin cifrar
        /// </summary>
        private const string PASS_SIN_CIFRAR = "1234";
        /// <summary>
        /// La contraseña, Sha1('PASS_SIN_CIFRAR')
        /// </summary>
        private const string PASS_CIFRADO = "7110eda4d09e062aa5e4a390b0a572ac0d2c0220";
        /// <summary>
        /// Numero maximo de intentos antes de bloquear la ip
        /// </summary>
        private const int MAX_INTENTOS = 3;
        /// <summary>
        /// Contador de intentos
        /// </summary>
        private int intentos = 0;
        /// <summary>
        /// Bandera que comprueba si el usuario puede entrar
        /// </summary>
        private const string MENSAJE_ERROR = "Se ha alcanzado el número máximo de intentos\nsu IP ha sido bloqueada\nintente de nuevo en ";
        /// <summary>
        /// Establece el tiempo de baneo en minutos
        /// </summary>
        private const double TIEMPO_BANEO_MINUTOS = 20;
        /// <summary>
        /// Almacena el tiempo donde se produjo el ultimo fallo de login permitido
        /// </summary>
        private TimeSpan tiempoDeError;
        /// <summary>
        /// Almacena el tiempo actual
        /// </summary>
        private TimeSpan tiempoActual;


        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void boton_Click(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            if (boton != null)
            {
                //Se ha intentado loggear:
                intentos++;
                //Se comprueba si puede loggear o no
                if (PuedeLoggear())
                    IntentoLogin();
            }
        }

        private bool PuedeLoggear()
        {
            if (intentos >= MAX_INTENTOS)
            {
                if (intentos == MAX_INTENTOS)
                    tiempoDeError = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                tiempoActual = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                TimeSpan dif = tiempoActual - tiempoDeError;
                double diferenciaMinutos = dif.TotalMinutes;
                double max_dif = Math.Round(TIEMPO_BANEO_MINUTOS - diferenciaMinutos, 0);
                if (max_dif <= 0)
                {
                    intentos = 0;
                    this.lblBan.Visibility = System.Windows.Visibility.Collapsed;
                    return true;
                }
                else
                {
                    this.lblError.Visibility = System.Windows.Visibility.Collapsed;
                    this.lblBan.Text = MENSAJE_ERROR + max_dif.ToString() + " Minuto" + (max_dif > 1 ? "s" : "");
                    this.lblBan.Visibility = System.Windows.Visibility.Visible;
                    return false;
                }
            }
            this.lblError.Visibility = System.Windows.Visibility.Collapsed;
            return true;
        }

        private void IntentoLogin()
        {
            //Se cifra la contraseña ingresada en Sha1:
            string cifrado = ObtenerSha1(this.pwdNombre.Password);

            //Se comprueba si coinciden:
            int cmp = string.Compare(cifrado, PASS_CIFRADO, StringComparison.OrdinalIgnoreCase);
            if (cmp == 0)
            {
                this.panelPrincipal.Visibility = System.Windows.Visibility.Visible;
                this.ContentPanel.Visibility = System.Windows.Visibility.Collapsed;
                intentos = 0;
            }
            else
            {
                this.lblError.Visibility = System.Windows.Visibility.Visible;
            }
        }
        /// <summary>
        /// Obtiene un hash Sha1 a partir de una cadena de texto
        /// </summary>
        /// <param name="pass">una cadena de texto</param>
        /// <returns></returns>
        private string ObtenerSha1(string pass)
        {
            Encoding utf8 = Encoding.UTF8;
            SHA1 cifrado = new SHA1Managed();
            byte[] buffer = utf8.GetBytes(pass);
            byte[] resultado = cifrado.ComputeHash(buffer);
            StringBuilder sr = new StringBuilder();
            foreach (byte b in resultado)
                sr.Append(string.Format("{0:x2}", b));
            return sr.ToString();
        }
    }
}