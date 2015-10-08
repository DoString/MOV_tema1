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

namespace Ejercicio1
{
    public partial class MainPage : PhoneApplicationPage
    {
        /*
         * CONSTANTE QUE ALMACENA EL NUMERO DE BOTONES A GENERAR
         */
        private const int N_BOTONES = 4;
        /*
         * VARIABLE CONTADOR DE CLICKS:
         */
        private int contadorClicks = 0;
        /*
         * VARIABLE CONTADOR BOTNOES:
         */
        private int contadorBotones = 0;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            //Si la cuenta de clicks es mayor que 0, comprobamos si existen 5 botones en el StackPanel:
            if (this.contadorClicks >= 1)
                if (this.contadorBotones >= 4)
                {
                    PonerColorFondoAleatorio(this.pnlVertical);
                    return;
                }
                
            //Se transforma sender a boton, no debería ser nulo nunca, pq solo tenemos un botón.
            Button bot = sender as Button;

            //Se generan n botones debajo de este dentro del StackPanel:
            GenerarBotones(this.pnlVertical, bot);

            //Se ha clickado el boton:
            this.contadorClicks++;
        }

        private void GenerarBotones(StackPanel stk, Button bot)
        {
            for (int i = 0; i < N_BOTONES; i++)
            {
                stk.Children.Add(new Button()
                {
                    Name = "Boton_" + (i + 1),
                    Content = "Boton_" + (i + 1),
                    Height = bot.Height,
                    Width = bot.Width,
                    //Obtenemos los margenes del boton principal y se los establecemos a los demas botones:
                    Margin = bot.Margin
                });
                this.contadorBotones++;
            }
        }

        private void PonerColorFondoAleatorio(StackPanel stk)
        {
            byte[] rgb = new byte[3];
            Random rand = new Random();
            const int ALFA_FULL = 0xFF;
            //Se rellena el vector rgb con 3 bytes aleatorios en [0](rojo),[1](verde),[2](azul):
            rand.NextBytes(rgb);
            foreach (UIElement boton in stk.Children)
                if (boton is Button)
                {
                    Button btn = boton as Button;
                    btn.Background = new SolidColorBrush(Color.FromArgb(ALFA_FULL, rgb[0], rgb[1], rgb[2]));
                }
        }
    }
}