using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo_Bingo
{
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

        public void PreencheCartelas(Random r)
        {
            for (int i = 0; i < quantidadeCartelas; i++)
            {
                cartelas[i] = new Cartela();
                cartelas[i].PreencherCartela(r);
            }
        }
    }
}
