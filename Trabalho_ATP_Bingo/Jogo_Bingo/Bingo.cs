using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo_Bingo
{
    internal class Bingo
    {
        int[] numerosSorteados = new int[75];
        int quantidadeSorteada = 0;

        public int[] NumerosSorteados
        {
            get { return numerosSorteados; }
            set { numerosSorteados = value; }
        }
        public int QuantidadeSorteada
        {
            get { return quantidadeSorteada; }
            set { quantidadeSorteada = value; }
        }

        public void Sortear(Random r)
        {
            int numeroGerado;

            do
            {
                numeroGerado = r.Next(1, 76);
            }
            while (verificarSorteio(numeroGerado));

            numerosSorteados[quantidadeSorteada] = numeroGerado;
            quantidadeSorteada++;
            Console.WriteLine($"Número sorteado: {numeroGerado}\n");


        }
        private bool verificarSorteio(int numero)
        {
            for (int i = 0; i < quantidadeSorteada; i++)
            {
                if (numerosSorteados[i] == numero)
                {
                    return true;
                }
            }
            return false;
        }
        public void ImprimirSorteados()
        {
            for(int i = 0; i < quantidadeSorteada; i++)
            {
                Console.Write($"{numerosSorteados[i]} ");
                
            }
            Console.WriteLine();
        }
    }
}
