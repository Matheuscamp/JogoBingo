using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo_Bingo
{
    internal class Cartela
    {
        int[,] cartela = new int[5, 5];
        int[] verifCartela = new int[75];
        int cont = 0;

        public void PreencherCartela(Random r)
        {
            for (int j = 0; j < cartela.GetLength(0); j++)
            {
                for (int i = 0; i < cartela.GetLength(1); i++)
                {
                    int numeroGerado;

                    do
                    {
                        if (j == 0)
                        {
                            numeroGerado = r.Next(1, 16);
                        }
                        else if (j == 1)
                        {
                            numeroGerado = r.Next(16, 31);
                        }
                        else if (j == 2)
                        {
                            numeroGerado = r.Next(31, 46);
                        }
                        else if (j == 3)
                        {
                            numeroGerado = r.Next(46, 61);
                        }
                        else
                        {
                            numeroGerado = r.Next(61, 76);
                        }
                    }
                    while (verificarDuplicado(numeroGerado));

                    verifCartela[cont] = numeroGerado;
                    cont++;

                    cartela[i, j] = numeroGerado;
                }
            }
            cartela[2, 2] = -1;
        }
        private bool verificarDuplicado(int numero)
        {
            for (int i = 0; i < cont; i++)
            {
                if (verifCartela[i] == numero)
                {
                    return true;
                }
            }
            return false;
        }

        //Métodos e impressão
        public void ImprimirMatriz()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write($"{cartela[i, j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
