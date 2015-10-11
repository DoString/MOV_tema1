using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ejercicio7
{


    public class Calculadora
    {

        public struct Operacion
        {
            private char operador;
            private int opr1Len;
            private double opr1;
            private int opr2Len;
            private double opr2;

            public Operacion(char operador, int len1, int len2):this()
            {
                this.operador = operador;
                this.opr1Len = len1;
                this.opr2Len = len2;
            }
        };
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
        };
        /// <summary>
        /// La pantalla de la calculadora
        /// </summary>
        private string _pantalla;

        private int _operando_1_len;
        private int _operando_2_len;
        private string[] _operaciones;
        private const int MAX_OPERACIONES = 100;


        public void LeerPantalla(string[] operaciones)
        {
            for (int i = 0; i < operaciones.Length; i++)
            {
                char operacion = operaciones[i][0];
                switch (operacion)
                {
                    case 'S':
                        break;
                    case 'R':
                        break;
                    case 'M':
                        break;
                    case 'D':
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public class StringBuffer
    {
        private int _bufferLen;
      
        public StringBuffer(string charBuffer)
        {
            _bufferLen = charBuffer.Length;
        }

        public void LeerPosicion(string buffer, int nChars)
        {

        }
    }
}
