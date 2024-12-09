using Jogo_Bingo;
using System;
using System.IO;

internal class Jogador
{
    private string nome;
    private int idade;
    private char sexo;
    private int quantidadeCartelas;
    Cartela[] cartelas;

    public string Nome
    {
        get { return nome; }
        set { nome = value; }
    }
    public int Idade
    {
        get { return idade; }
        set { idade = value; }
    }
    public char Sexo
    {
        get { return sexo; }
        set { sexo = value; }
    }
    public int QuantidadeCartelas
    {
        get { return quantidadeCartelas; }
        set { quantidadeCartelas = value; }
    }
    public Cartela[] Cartelas
    {
        get { return cartelas; }
        set { cartelas = value; }
    }
    public Jogador(string nome, int idade, char sexo, int quantidadeCartelas)
    {
        this.nome = nome;
        this.idade = idade;
        this.sexo = sexo;
        this.quantidadeCartelas = quantidadeCartelas;
        cartelas = new Cartela[quantidadeCartelas];
    }

    public void PreencheCartelas(Random r, StreamWriter arq)
    {
        for (int i = 0; i < quantidadeCartelas; i++)
        {
            cartelas[i] = new Cartela();
            cartelas[i].PreencherCartela(r);
        }

        VerificarCartelasUnicas(r, arq);
    }

    public void VerificarCartelasUnicas(Random r, StreamWriter arq)
    {
        for (int i = 0; i < quantidadeCartelas; i++)
        {
            for (int j = 0; j < i; j++)
            {
                while (cartelas[i].CompararCartela(cartelas[j]))
                {
                    Console.WriteLine($"Cartela {i + 1} é igual à cartela {j + 1}. Gerando nova cartela...");
                    arq.WriteLine($"Cartela {i + 1} é igual à cartela {j + 1}. Gerando nova cartela...");
                    cartelas[i] = new Cartela();
                    cartelas[i].PreencherCartela(r);
                }
            }
        }
    }
}
