using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Ejercicio7
{
    public class ExpresionMat
    {
        /// <summary>
        /// Valida una expresion regular contra el texto introducido en el parámetro.
        /// </summary>
        /// <param name="text">
        /// Un string que contiene la información a comprobar.
        /// </param>
        /// <returns>
        /// Devuelve true si la expresión ha sido validada con éxito.
        /// </returns>
        /// <exception cref="ExpresionMalFormadaException">
        /// Si la expresión no ha sido validada o está mal formada.
        /// </exception>
        public static bool CheckRegex(string text)
        {            
            string regex = @"^(-?\d{1,24},?\d{0,8}[*/+-]?)*-?\d{1,24},?\d{0,8}$";
            Match match = Regex.Match(text, regex);
            if (!match.Success)
                throw new ExpresionMalFormadaException(string.Format("La expresion `{0}` es inválida", text));
            return true;
        }
        /// <summary>
        /// Comprueba la operación matemática y resuelve la expresión.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string CheckMatOp(string text)
        {
            string negRegex = @"^-?\d+,?\d*$"; //Si es -digito ya hemos terminado.

            //string notacion = text.Replace("E+"+'\d{2}'

            string regex = "[+*/-]"; //buscamos los operadores.
            MatchCollection matches = Regex.Matches(text, regex); //almacenamos los aciertos en una colección
            foreach (Match op in matches)
                switch (op.Value[0])//para cada operador que parece en la expresión. Transformamos a char con [0]
                {
                    case '+': Sum(ref text);
                        break;
                    case '-': if (Regex.IsMatch(text, negRegex)) return text; Rest(ref text);
                        break;
                    case '*': Mul(ref text);
                        break;
                    case '/': Div(ref text);
                        break;
                    default:
                        break;
                }
            return string.Format("{0,0}", text);
        }

        private static void Sum(ref string text)
        {
            string[] oprs = text.Split(new char[] { '+', '-', '/', '*' });
            if (text.StartsWith("-")) //Si empieza por - el operador izquierdo es ""; la expresion es ""-5+5 oprs[0],[1],[2] = "", 5, 5
            {
                if (oprs.Length >= 2) //Comprobamos si la expresion es del tipo: -5+-5; oprs[0],[1],[2],[3] = "", 5, "", 5
                {
                    oprs[0] = "-" + oprs[1];
                    if (oprs.Length > 2 && oprs[2] == "")
                        oprs[2] = "-" + oprs[3];
                    oprs[1] = oprs[2];
                }
            }
            else if (oprs.Length > 2 && oprs[1] == "")//Comprobamos si la expresión es del tipo: 5+-5 oprs[0],[1],[2] = 5, "", 5            
                oprs[1] = "-" + oprs[2];

            double op1 = double.Parse(oprs[0]);
            double op2 = double.Parse(oprs[1]);
            double res = op1 + op2;
            text = text.Replace(oprs[0] + "+" + oprs[1], res.ToString());
        }
        private static void Div(ref string text)
        {
            string[] oprs = text.Split(new char[] { '+', '/', '-', '*' });
            if (text.StartsWith("-")) //Si empieza por - el operador izquierdo es ""; la expresion es ""-5/5 oprs[0],[1],[2] = "", 5, 5
            {
                if (oprs.Length >= 2) //Comprobamos si la expresion es del tipo: -5/-5; oprs[0],[1],[2],[3] = "", 5, "", 5
                {
                    oprs[0] = "-" + oprs[1];
                    if (oprs.Length > 2 && oprs[2] == "")
                        oprs[2] = "-" + oprs[3];
                    oprs[1] = oprs[2];
                }
            }
            else if (oprs.Length > 2 && oprs[1] == "") //Comprobamos si la expresión es del tipo: 5/-5 oprs[0],[1],[2] = 5, "", 5            
                oprs[1] = "-" + oprs[2];


            double op1 = double.Parse(oprs[0]);
            double op2 = double.Parse(oprs[1]);
            double res = op1 / op2;
            text = text.Replace(oprs[0] + "/" + oprs[1], res.ToString());
        }
        private static void Mul(ref string text)
        {
            string[] oprs = text.Split(new char[] { '+', '-', '/', '*' });
            if (text.StartsWith("-")) //Si empieza por - el operador izquierdo es ""; la expresion es ""-5*5 oprs[0],[1],[2] = "", 5, 5
            {
                if (oprs.Length >= 2) //Comprobamos si la expresion es del tipo: -5*-5; oprs[0],[1],[2],[3] = "", 5, "", 5
                {
                    oprs[0] = "-" + oprs[1];
                    if (oprs.Length > 2 && oprs[2] == "")
                        oprs[2] = "-" + oprs[3];
                    oprs[1] = oprs[2];
                }
            }
            else if (oprs.Length > 2 && oprs[1] == "") //Comprobamos si la expresión es del tipo: 5*-5 oprs[0],[1],[2] = 5, "", 5            
                oprs[1] = "-" + oprs[2];

            double op1 = double.Parse(oprs[0]);
            double op2 = double.Parse(oprs[1]);
            double res = op1 * op2;
            text = text.Replace(oprs[0] + "*" + oprs[1], res.ToString());
        }
        private static void Rest(ref string text)
        {
            string[] oprs = text.Split(new char[] { '+', '-', '/', '*' });

            if (text.StartsWith("-")) //Si empieza por - el operador izquierdo es ""; la expresion es ""-5-5 oprs[0],[1],[2] = "", 5, 5
            {
                if (oprs.Length >= 2) //Comprobamos si la expresion es del tipo: -5--5; oprs[0],[1],[2],[3] = "", 5, "", 5
                {
                    oprs[0] = "-" + oprs[1];
                    if (oprs.Length > 2 && oprs[2] == "")
                        oprs[2] = "-" + oprs[3];
                    oprs[1] = oprs[2];
                }
            }
            else if (oprs.Length > 2 && oprs[1] == "") //Comprobamos si la expresión es del tipo: 5--5 oprs[0],[1],[2] = 5, "", 5            
                oprs[1] = "-" + oprs[2];

            double op1 = double.Parse(oprs[0]);
            double op2 = double.Parse(oprs[1]);
            double res = op1 - op2;
            text = text.Replace(oprs[0] + "-" + oprs[1], res.ToString());
        }
    }

    public class ExpresionMalFormadaException : Exception
    {
        public ExpresionMalFormadaException(string msg)
            : base(msg) { }
    }
}
