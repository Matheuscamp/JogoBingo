using System;
using System.Text;
using System.IO;

namespace Jogo_Bingo
{
    internal class Program
    {
        static void ExibirRanking(Jogador[] jogadores, int qtdJogadores, StreamWriter arq)
        {
            Console.WriteLine("\n--- Ranking Final ---");
            arq.WriteLine("\n--- Ranking Final ---");

            Jogador[] jogadoresAtivos = new Jogador[qtdJogadores];
            string[] jogadoresEliminados = new string[qtdJogadores];
            int ativosCount = 0, eliminadosCount = 0;

            for (int i = 0; i < jogadores.Length; i++)
            {
                if (jogadores[i] != null)
                {
                    jogadoresAtivos[ativosCount] = jogadores[i];
                    ativosCount++;
                }
                else
                {
                    jogadoresEliminados[eliminadosCount] = $"Jogador {i + 1}";
                    eliminadosCount++;
                }
            }

            for (int i = 0; i < ativosCount - 1; i++)
            {
                for (int j = i + 1; j < ativosCount; j++)
                {
                    if (jogadoresAtivos[i].Nome.CompareTo(jogadoresAtivos[j].Nome) > 0)
                    {
                        var temp = jogadoresAtivos[i];
                        jogadoresAtivos[i] = jogadoresAtivos[j];
                        jogadoresAtivos[j] = temp;
                    }
                }
            }

            int rank = 1;
            for (int i = 0; i < ativosCount; i++)
            {
                Jogador jogador = jogadoresAtivos[i];
                string mensagem = $"{rank}° Lugar: Nome: {jogador.Nome}, Idade: {jogador.Idade}, Sexo: {jogador.Sexo}";
                Console.WriteLine(mensagem);
                arq.WriteLine(mensagem);
                rank++;
            }

            for (int i = 0; i < eliminadosCount; i++)
            {
                string mensagem = $"{rank}° Lugar: Nome: {jogadoresEliminados[i]} (Eliminado)";
                Console.WriteLine(mensagem);
                arq.WriteLine(mensagem);
                rank++;
            }

            Console.WriteLine("----------------------");
            arq.WriteLine("----------------------");
        }


        static void GritarBingo(Jogador[] jogadores, Bingo bingo, StreamWriter arq)
        {
            Console.Write($"Qual jogador deseja gritar Bingo? (1 a {jogadores.Length}): ");
            arq.WriteLine($"Qual jogador deseja gritar Bingo? (1 a {jogadores.Length}): ");
            int jogadorSelecionado = ValidarEntradaJogador(jogadores.Length);

            if (jogadores[jogadorSelecionado] == null)
            {
                string mensagem = "Jogador não está mais na partida!";
                Console.WriteLine(mensagem);
                arq.WriteLine(mensagem);
                return;
            }

            string tentativa = $"Jogador {jogadores[jogadorSelecionado].Nome} está tentando cantar Bingo!";
            Console.WriteLine(tentativa);
            arq.WriteLine(tentativa);

            bool ganhou = false;
            for (int i = 0; i < jogadores[jogadorSelecionado].QuantidadeCartelas; i++)
            {
                if (VerificarBingo(jogadores[jogadorSelecionado].Cartelas[i]))
                {
                    ganhou = true;
                    string mensagem = $"Parabéns, Jogador {jogadores[jogadorSelecionado].Nome}! Você completou o padrão de Bingo na cartela {i + 1}.";
                    Console.WriteLine(mensagem);
                    arq.WriteLine(mensagem);
                    break;
                }
            }

            if (!ganhou)
            {
                if (jogadores[jogadorSelecionado].QuantidadeCartelas > 1)
                {
                    string mensagem = $"Bingo inválido! Uma das cartelas do Jogador {jogadores[jogadorSelecionado].Nome} será anulada.";
                    Console.WriteLine(mensagem);
                    arq.WriteLine(mensagem);
                    jogadores[jogadorSelecionado].QuantidadeCartelas--;
                }
                else
                {
                    string mensagem = $"Jogador {jogadores[jogadorSelecionado].Nome} foi eliminado por gritar Bingo incorretamente!";
                    Console.WriteLine(mensagem);
                    arq.WriteLine(mensagem);
                    jogadores[jogadorSelecionado] = null;
                }
            }
        }


        static bool VerificarBingo(Cartela cartela)
        {
            for (int i = 0; i < 5; i++)
            {
                bool linhaCompleta = true;
                for (int j = 0; j < 5; j++)
                {
                    if (cartela.MatrizCartela[i, j] != -1)
                    {
                        linhaCompleta = false;
                        break;
                    }
                }
                if (linhaCompleta) return true;
            }

            for (int j = 0; j < 5; j++)
            {
                bool colunaCompleta = true;
                for (int i = 0; i < 5; i++)
                {
                    if (cartela.MatrizCartela[i, j] != -1)
                    {
                        colunaCompleta = false;
                        break;
                    }
                }
                if (colunaCompleta) return true;
            }

            return false;
        }

        static void AcaoUsuario(int acao, Random r, Bingo bingo, Jogador[] jogadores, int qtdJogadores, out int jogadorSelecionado, out int linha, out int coluna, out int cartelaSelecionada, StreamWriter arq)
        {
            jogadorSelecionado = 0;
            linha = 0;
            coluna = 0;
            cartelaSelecionada = 0;

            switch (acao)
            {
                case 1:
                    bingo.Sortear(r, arq);
                    break;
                case 2:
                    for (int j = 0; j < qtdJogadores; j++)
                    {
                        ImprimirCartelasJogador(jogadores[j], arq);
                    }
                    Console.WriteLine("------------------------------------------");
                    arq.WriteLine("------------------------------------------");
                    break;
                case 3:
                    if (bingo.QuantidadeSorteada == 0)
                    {
                        string mensagem = "Nenhum número ainda foi sorteado!";
                        Console.WriteLine(mensagem);
                        arq.WriteLine(mensagem);
                    }
                    else
                    {
                        Console.WriteLine();
                        bingo.ImprimirSorteados(arq);
                        arq.WriteLine();
                    }
                    break;
                case 4:
                    Console.Write($"Qual jogador deseja marcar (1 a {qtdJogadores}): ");
                    jogadorSelecionado = ValidarEntradaJogador(qtdJogadores);
                    arq.WriteLine($"Jogador {jogadorSelecionado}");

                    Console.Write($"Qual cartela deseja marcar (1 a {jogadores[jogadorSelecionado].QuantidadeCartelas}): ");
                    cartelaSelecionada = ValidarEntradaCartela(jogadores[jogadorSelecionado]);
                    arq.WriteLine($"Cartela {cartelaSelecionada}");

                    Console.Write("Digite a linha (0 a 4): ");
                    linha = ValidarEntradaPosicao();
                    arq.WriteLine($"Linha {linha}");

                    Console.Write("Digite a coluna (0 a 4): ");
                    coluna = ValidarEntradaPosicao();
                    arq.WriteLine($"Coluna {coluna}");
                    break;
                case 5:
                    GritarBingo(jogadores, bingo, arq);
                    break;
                default:
                    Console.WriteLine("Opção inválida! Será sorteado um novo número!");
                    arq.WriteLine("Opção inválida! Será sorteado um novo número!");
                    bingo.Sortear(r, arq);
                    break;
            }
        }
        static int ValidarEntradaJogador(int qtdJogadores)
        {
            int jogadorSelecionado;
            do
            {
                jogadorSelecionado = int.Parse(Console.ReadLine()) - 1;
            } while (jogadorSelecionado < 0 || jogadorSelecionado >= qtdJogadores);
            return jogadorSelecionado;
        }

        static int ValidarEntradaCartela(Jogador jogador)
        {
            int cartelaSelecionada;
            do
            {
                cartelaSelecionada = int.Parse(Console.ReadLine()) - 1;
            } while (cartelaSelecionada < 0 || cartelaSelecionada >= jogador.QuantidadeCartelas);
            return cartelaSelecionada;
        }

        static int ValidarEntradaPosicao()
        {
            int posicao;
            do
            {
                posicao = int.Parse(Console.ReadLine());
            } while (posicao < 0 || posicao > 4);
            return posicao;
        }

        static int ContarJogadoresAtivos(Jogador[] jogadores)
        {
            int contador = 0;
            foreach (var jogador in jogadores)
            {
                if (jogador != null)
                    contador++;
            }
            return contador;
        }

        static void ImprimirCartelasJogador(Jogador jogador, StreamWriter arq)
        {
            int cont = 1;
            foreach (Cartela cartela in jogador.Cartelas)
            {
                Console.WriteLine($"Cartela {cont}° do jogador {jogador.Nome}:\n");

                arq.WriteLine($"Cartela {cont}° do jogador {jogador.Nome}:\n");

                cartela.ImprimirMatriz(arq);
                Console.WriteLine();
                cont++;
            }
        }
        static void PreencherJogador(out string nome, out int idade, out char sexo, out int qtdCartela, StreamWriter arq)
        {
            Console.Write("Digite seu nome: ");
            nome = Console.ReadLine();

            arq.WriteLine("Digite seu nome: ");
            arq.WriteLine(nome);

            Console.Write("Digite a idade: ");
            idade = int.Parse(Console.ReadLine());

            arq.WriteLine("Digite a idade:");
            arq.WriteLine(idade);

            Console.Write("Digite o sexo. (M ou F): ");
            sexo = char.Parse(Console.ReadLine());

            arq.WriteLine("Digite o sexo. (M ou F): ");
            arq.WriteLine(sexo);

            if (sexo != 'M' && sexo != 'F' && sexo != 'm' && sexo != 'f')
            {
                Console.WriteLine($"Sexo deve ser M ou F. Será definido como Indefinido (I)!");
                sexo = 'I';

                arq.WriteLine($"Sexo deve ser M ou F. Será definido como Indefinido (I)!");
                arq.WriteLine(sexo);
            }

            Console.Write("Digite a quantidade de cartela(s): ");
            qtdCartela = int.Parse(Console.ReadLine());

            arq.WriteLine("Digite a quantidade de cartela(s): ");
            arq.WriteLine(qtdCartela);

            if ( qtdCartela < 1 || qtdCartela > 4)
            {
                do
                {
                    Console.Write("Quantidade de cartela deve ser entre 1 e 4. Digite novamente: ");
                    qtdCartela = int.Parse(Console.ReadLine());

                    arq.WriteLine("Quantidade de cartela deve ser entre 1 e 4. Digite novamente: ");
                    arq.WriteLine(qtdCartela);

                } while (qtdCartela < 1 || qtdCartela > 4);
            }
            Console.WriteLine();
        }
        static void MarcarCartela(Jogador[] jogadores, int jogadorSelecionado, int cartelaSelecionada, int linha, int coluna, StreamWriter arq)
        {
            int numeroAtual = jogadores[jogadorSelecionado].Cartelas[cartelaSelecionada].MatrizCartela[linha, coluna];

            jogadores[jogadorSelecionado].Cartelas[cartelaSelecionada].MatrizCartela[linha, coluna] = -1;
            Console.WriteLine($"Número {numeroAtual} marcado na cartela {cartelaSelecionada + 1} do jogador {jogadores[jogadorSelecionado].Nome}!");

            arq.WriteLine($"Número {numeroAtual} marcado na cartela {cartelaSelecionada + 1} do jogador {jogadores[jogadorSelecionado].Nome}!");
        }

        static void Main(string[] args)
        {
            Bingo bingo = new Bingo();
            Random r = new Random();

            string nome;
            int idade;
            char sexo;
            int qtdCartela;
            int qtdJogadores;
            int jogadorSelecionado;
            int linha;
            int coluna;
            int cartelaSelecionada;

            StreamWriter arq = new StreamWriter("Log.txt", true, Encoding.UTF8);

            Console.Write("Digite a quantidade de jogadores: ");
            qtdJogadores = int.Parse(Console.ReadLine());
            arq.WriteLine("Digite a quantidade de jogadores: ");
            arq.WriteLine(qtdJogadores);
            while (qtdJogadores < 2 || qtdJogadores > 5)
            {
                Console.Write("Quantidade de jogadores deve ser entre 2 e 5. Digite novamente: ");
                qtdJogadores = int.Parse(Console.ReadLine());

                arq.WriteLine("Quantidade de jogadores deve ser entre 2 e 5. Digite novamente: ");
                arq.WriteLine(qtdJogadores);
            }

            Jogador[] jogadores = new Jogador[qtdJogadores];

            for (int i = 0; i < qtdJogadores; i++)
            {
                Console.WriteLine($"Digite as infos do Jogador {i + 1}:");
                PreencherJogador(out nome, out idade, out sexo, out qtdCartela, arq);
                jogadores[i] = new Jogador(nome, idade, sexo, qtdCartela);
                jogadores[i].PreencheCartelas(r, arq);

                arq.WriteLine($"Digite as infos do Jogador {i + 1}:");
                arq.WriteLine( nome, idade, sexo,  qtdCartela);
            }
            Console.Clear();

            while (bingo.QuantidadeSorteada < 75 && ContarJogadoresAtivos(jogadores) > 1)
            {
                Console.WriteLine("1. Sortear número");
                Console.WriteLine("2. Imprimir tabelas de todos os jogadores.");
                Console.WriteLine("3. Imprimir números sorteados.");
                Console.WriteLine("4. Marcar um número");
                Console.WriteLine("5. Gritar Bingo.");
                Console.Write("Digite uma das opções acima: ");
                int acao = int.Parse(Console.ReadLine());

                arq.WriteLine($"Ação digitada: ");
                arq.WriteLine(acao);

                AcaoUsuario(acao, r, bingo, jogadores, qtdJogadores, out jogadorSelecionado, out linha, out coluna, out cartelaSelecionada, arq);

                if (acao == 4)
                {
                    MarcarCartela(jogadores, jogadorSelecionado, cartelaSelecionada, linha, coluna, arq);
                }
            }
            ExibirRanking(jogadores, qtdJogadores, arq);
        }
    }
}
