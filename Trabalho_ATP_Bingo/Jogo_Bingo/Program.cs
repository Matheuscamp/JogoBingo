using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Jogo_Bingo
{
    internal class Program
    {
        static void ImprimirSorteio(Random r, Bingo bingo)
        {
            for (int i = 0; i <= 75; i++)
            {
                if (i < 75)
                {
                    Console.Write($"Precione enter para sortear o {i + 1}° número:");
                    Console.ReadLine();
                    bingo.Sortear(r);
                }
                if (i == 75)
                {
                    Console.WriteLine("Todos os números foram sorteados");
                }
            }
        }

        static void ImprimirCartelasJogador(Jogador jogador)
        {
            int cont = 1;
            foreach (var cartela in jogador.Cartelas)
            {
                Console.WriteLine($"Cartela {cont}° do jogador {jogador.Nome}");
                cartela.ImprimirMatriz();
                Console.WriteLine();
                cont++;
            }
        }
        static void PreencherJogador(out string nome, out int idade, out char sexo, out int qtdCartela)
        {
            Console.Write("Digite seu nome: ");
            nome = Console.ReadLine();

            Console.Write("Digite a idade: ");
            idade = int.Parse(Console.ReadLine());

            Console.Write("Digite o sexo: ");
            sexo = char.Parse(Console.ReadLine());

            Console.Write("Digite a quantidade de cartela(s): ");
            qtdCartela = int.Parse(Console.ReadLine());

            if(qtdCartela < 1 || qtdCartela > 4)
            {
                Console.WriteLine("Quantidade de cartela deve ser 1 ou 4. Será definido como 1");
                qtdCartela = 1;
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Bingo bingo = new Bingo();
            Random r = new Random();

            string nome;
            int idade;
            char sexo;
            int qtdCartela;
            int qtdJogadores = 1;

            Console.Write("Digite a quantidade de jogadores: ");
            qtdJogadores = int. Parse(Console.ReadLine());

            if(qtdJogadores < 1 || qtdJogadores > 4)
            {
                Console.WriteLine("Quantidade de jogadores deve ser entre 1 ou 4. Será definido como 1");
                qtdJogadores = 1;
            }

            PreencherJogador(out nome, out idade, out sexo, out qtdCartela);
            Jogador jogador1 = new Jogador(nome, idade, sexo, qtdCartela);

            PreencherJogador(out nome, out idade, out sexo, out qtdCartela);
            Jogador jogador2 = new Jogador(nome, idade, sexo, qtdCartela);

            jogador1.PreencheCartelas(r);
            jogador2.PreencheCartelas(r);

            ImprimirCartelasJogador(jogador1);
            ImprimirCartelasJogador(jogador2);

            ImprimirSorteio(r, bingo);
        }
    }
}
