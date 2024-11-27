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
        //Método que recebe a variável "acao" e retorna 4 caminhos
        static void AcaoUsuario(int acao, Random r, Bingo bingo, Jogador[] jogadores, int qtdJogadores)
        {
            if (acao == 1)
            {
                // Chama o sorteio
                bingo.Sortear(r);

                // Após cada sorteio, marcar as cartelas
                MarcarCartela(jogadores, bingo.NumerosSorteados, bingo.QuantidadeSorteada);
            }
            else if(acao == 2)
            {
                // Chama o método para imprimir as cartelas
                for (int j = 0; j < qtdJogadores; j++)
                {
                    ImprimirCartelasJogador(jogadores[j]);
                }
                Console.WriteLine("------------------------------------------");
            }
            else if(acao == 3)
            {
                if(bingo.QuantidadeSorteada == 0)
                {
                    Console.WriteLine("Nenhum número foi sorteado!");
                }
                else
                {
                    Console.WriteLine();
                    bingo.ImprimirSorteados();
                }
            }
            else
            {
                Console.WriteLine("Opção inválida! Será sorteado um novo número e caso ele exista, será marcado na(s) tabela(s).");
                // Chama o sorteio e marca as cartelas
                bingo.Sortear(r);
                MarcarCartela(jogadores, bingo.NumerosSorteados, bingo.QuantidadeSorteada);
            }
        }

        //Método para imprimir cada cartela do jogador
        static void ImprimirCartelasJogador(Jogador jogador)
        {
            int cont = 1;
            foreach (Cartela cartela in jogador.Cartelas)
            {
                Console.WriteLine($"Cartela {cont}° do jogador {jogador.Nome}:\n");
                cartela.ImprimirMatriz();
                Console.WriteLine();
                cont++;
            }
        }


        //Método para preencher as informações do jogador
        static void PreencherJogador(out string nome, out int idade, out char sexo, out int qtdCartela)
        {
            Console.Write("Digite seu nome: ");
            nome = Console.ReadLine();

            Console.Write("Digite a idade: ");
            idade = int.Parse(Console.ReadLine());

            Console.Write("Digite o sexo. (M ou F): ");
            sexo = char.Parse(Console.ReadLine());

            if (sexo != 'M' && sexo != 'F' && sexo != 'm' && sexo != 'f')
            {
                Console.WriteLine($"Sexo deve ser M ou F. Será definido como Indefinido (I)!");
                sexo = 'I';
            }

            Console.Write("Digite a quantidade de cartela(s): ");
            qtdCartela = int.Parse(Console.ReadLine());

            if (qtdCartela < 1 || qtdCartela > 4)
            {
                Console.WriteLine("Quantidade de cartela deve ser 1 ou 4. Será definido como 1");
                qtdCartela = 1;
            }
            Console.WriteLine();
        }


        //Método que percorre todas as cartelas de todos os jogadores. Caso o número sorteado for igual ao número da posição atual da matriz é atualizado para -1
        static void MarcarCartela(Jogador[] jogadores, int[] numerosSorteados, int quantidadeSorteada)
        {
            // Itera pelos números já sorteados
            for (int n = 0; n < quantidadeSorteada; n++)
            {
                int numeroAtual = numerosSorteados[n];

                // Itera por todos os jogadores
                foreach (Jogador jogador in jogadores)
                {
                    // Itera por todas as cartelas do jogador
                    foreach (Cartela cartela in jogador.Cartelas)
                    {
                        // Itera por cada célula da matriz da cartela
                        for (int i = 0; i < 5; i++)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                if (cartela.MatrizCartela[i, j] == numeroAtual) // Verifica se o número está na cartela
                                {
                                    cartela.MatrizCartela[i, j] = -1; // Marca o número como -1
                                    Console.WriteLine($"Número {numeroAtual} marcado na cartela de {jogador.Nome}!\n\n");
                                }
                            }
                        }
                    }
                }
            }
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

            // Definir quantidade de Jogares e verificar se está dentro do padrão.
            Console.Write("Digite a quantidade de jogadores: ");
            qtdJogadores = int.Parse(Console.ReadLine());
            Console.Clear();

            if (qtdJogadores < 2 || qtdJogadores > 5)
            {
                Console.WriteLine("Quantidade de jogadores deve ser entre 1 ou 5. Será definido como 2");
                qtdJogadores = 2;
            }

            // Objeto da Classe jogador para armazenar as informações de cada jogador dentro de 1 posição do vetor
            Jogador[] jogadores = new Jogador[qtdJogadores];

            // Comando de repetição que executa o método PreencherJogador, cria um objeto que recebe as informações do método e chama o método PreencherCartelas em cada cartela do jogador
            for (int i = 0; i < qtdJogadores; i++)
            {
                Console.WriteLine($"Digite as infos do Jogador {i + 1}:");
                PreencherJogador(out nome, out idade, out sexo, out qtdCartela);
                jogadores[i] = new Jogador(nome, idade, sexo, qtdCartela);
                jogadores[i].PreencheCartelas(r);
            }

            Console.Clear();

            while(bingo.QuantidadeSorteada < 75)
            {
                
                Console.WriteLine("1. Sortear número e marcar na tabela.");
                Console.WriteLine("2. Imprimir tabelas de todos os jogadores.");
                Console.WriteLine("3. Imprimir números sorteados.");
                Console.Write("Digite uma das opções acima: ");
                int acao = int.Parse(Console.ReadLine());
                AcaoUsuario(acao, r, bingo, jogadores, qtdJogadores);
            }

        }
    }
}
