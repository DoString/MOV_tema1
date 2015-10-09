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
using System.Windows.Threading;

namespace Ejercicio5
{
    public partial class MainPage : PhoneApplicationPage
    {
        /// <summary>
        /// Temporizador
        /// </summary>
        private DispatcherTimer _dt;
        /// <summary>
        /// nuestra pelota
        /// </summary>
        private Ellipse _pelota;
        /// <summary>
        /// Cuenta los clicks
        /// </summary>
        private int _cuentaClicks = 0;
        /// <summary>
        /// Velocidad eje x de la pelota
        /// </summary>
        private int velocidad_x = 10;
        /// <summary>
        /// Velocidad eje x de la pelota
        /// </summary>
        private int velocidad_y = 10;
        /// <summary>
        /// Intervalo de Timer
        /// </summary>
        private const int INTERVALO = 100000;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            //Colocamos la pelota en un punto aleatorio
            Canvas.SetLeft(_pelota, ((Double)new Random().Next((int)this.canvasPanel.Width)));
            Canvas.SetTop(_pelota, ((Double)new Random().Next((int)this.canvasPanel.Height)));
            _dt.Start();
            this.btnPausar.IsEnabled = true;
            ((Button)sender).IsEnabled = false; // se deshabilita a si mismo
            _pelota.Visibility = Visibility.Visible;
        }

        private void btnPausar_Click(object sender, RoutedEventArgs e)
        {
            this.btnIniciar.IsEnabled = false;
            if (_cuentaClicks++ % 2 != 0)
            {
                _dt.Start();
                return;
            }
            _dt.Stop();
            
        }

        private void btnParar_Click(object sender, RoutedEventArgs e)
        {
            _dt.Stop();
            this.btnPausar.IsEnabled = false;
            this.btnIniciar.IsEnabled = true;
            _pelota.Visibility = Visibility.Collapsed;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

            _pelota = this.ellipse;
            _dt = new DispatcherTimer();
            _dt.Interval = new TimeSpan(INTERVALO);
            _dt.Tick += new EventHandler(dt_Tick);
            _pelota.Visibility = System.Windows.Visibility.Collapsed;
        }

        void dt_Tick(object sender, EventArgs e)
        {
            if ((Canvas.GetLeft(_pelota) + (_pelota.ActualWidth)) >= this.canvasPanel.Width)
                velocidad_x *= -1;
            if ((Canvas.GetLeft(_pelota) - (_pelota.ActualWidth/2D)) <= 0)
                velocidad_x *= -1;
            if ((Canvas.GetTop(_pelota) + (_pelota.ActualHeight)) >= this.canvasPanel.Height)
                velocidad_y *= -1;
            if ((Canvas.GetTop(_pelota) + (_pelota.ActualHeight/2D)) <= 0)
                velocidad_y *= -1;

            Canvas.SetLeft(_pelota, (Canvas.GetLeft(_pelota) + velocidad_x));
            Canvas.SetTop(_pelota, (Canvas.GetTop(_pelota) + velocidad_y));
        }
    }
}