using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto3
{
    [System.Serializable]
    class Ebook : Produto, IEstoque
    {
        public string autor;
        private int vendas;

        public Ebook(string nome, float valor, string autor)
        {
            this.nome = nome;
            this.valor = valor;
            this.autor = autor;
        }

        public void AdicionarEntrada()
        {
            Console.WriteLine($"Não é possivel dar entrada no estoque de um E-book pois é um produto Digital");

            Console.ReadLine();
        }

        public void AdicionarSaida()
        {
            Console.WriteLine($"Adicionar venda no E-book {nome}");
            Console.WriteLine("Digite a Qtd. de vendas você quer dar entrada: ");
            int entrada = int.Parse(Console.ReadLine());
            vendas += entrada;
            Console.WriteLine("Saida registrada");
            Console.ReadLine();
        }

        public void Exibir()
        {
            Console.WriteLine($"Nome: {nome}");
            Console.WriteLine($"Autor: {autor}");
            Console.WriteLine($"Valor: {valor}");
            Console.WriteLine($"Vendas: {vendas}");
            Console.WriteLine("================================");
        }
    }
}
