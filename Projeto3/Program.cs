using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;


namespace Projeto3
{
    [System.Serializable]
    internal class Program
    {
        static List<IEstoque> produtos = new List<IEstoque>();
        enum Menu { Listar = 1, Adicionar, Remover, Entrada, Saida, Sair }
        static void Main(string[] args)
        {
            Carregar();
            bool escolheusair = false;
            while (!escolheusair) {
                Console.WriteLine("Sistema de estoque");
                Console.WriteLine("1-Listar\n2-Adicionar\n3-Remover\n4-Registrar Entrada\n5-Registrar Saida\n6-Sair");
                string opcaoesco = Console.ReadLine();
                
                if (int.TryParse(opcaoesco, out int opInt) && opInt> 0 && opInt < 7)
                {
                    Menu escolha = (Menu)opInt;
                    switch (escolha)
                    {

                        case Menu.Listar:
                            Listagem();
                            break;
                        case Menu.Adicionar:
                            Cadastro();
                            break;
                        case Menu.Remover:
                            Remover();
                            break;
                        case Menu.Entrada:
                            Entrada();
                            break;
                        case Menu.Saida:
                            Saida();
                            break;
                        case Menu.Sair:
                            escolheusair = true;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Opção invalida, programa irá fechar");
                    Console.ReadLine();
                    escolheusair = true;
                }
                Console.Clear();

                
            }

            
        }
        static void Listagem()
        {
            Console.WriteLine("Lista de produtos");
            int i = 0;
            foreach(IEstoque produto in produtos)
            {
                Console.WriteLine("ID: " + i);
                produto.Exibir();
                i++;
            }
            Console.ReadLine() ;
        }
        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do elemento que você quer remover: ");
            string idStr = Console.ReadLine();
            if (int.TryParse(idStr, out int id) && id >= 0 && id < produtos.Count)
            {
                produtos.RemoveAt(id);
                Salvar();
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }
        static void Entrada()
        {
            Listagem();
            Console.WriteLine("Digite o ID do elemento que você quer dar entrada: ");
            string idStr = Console.ReadLine();
            if (int.TryParse(idStr, out int id) && id >= 0 && id < produtos.Count)
            {
                produtos[id].AdicionarEntrada();
                Salvar();
            }
            else
            {
                Console.WriteLine("ID inválido");
            }
        }
        static void Saida()
        {
            Listagem();
            Console.WriteLine("Digite o ID do elemento que você quer dar baixa: ");
            string idStr = Console.ReadLine();
            if (int.TryParse(idStr, out int id) && id >= 0 && id < produtos.Count)
            {
                produtos[id].AdicionarSaida();
                Salvar();
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }
        static void Cadastro()
        {
            Console.WriteLine("Cadastro de Produto");
            Console.WriteLine("1-Produto Fisico\n2-Ebook\n3-Curso");
            string opStr = Console.ReadLine();
            if (int.TryParse(opStr, out int escolhaInt) && escolhaInt > 0 && escolhaInt < 4)


                switch (escolhaInt)
            {
                case 1:
                    CadastrarPFisico();
                    break;
                case 2:
                    CadastrarEbook();
                    break;
                case 3:
                    CadastrarCurso();
                    break;
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }
        }
        static void CadastrarPFisico()
        {
            Console.WriteLine("Cadastrando produto físico: ");
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Preço: ");
            float valor = float.Parse(Console.ReadLine());
            Console.WriteLine("Frete: ");
            float frete = float.Parse(Console.ReadLine());
            ProdutoFisico pf = new ProdutoFisico(nome, valor, frete);
            produtos.Add(pf);
            Salvar();
        }

        static void CadastrarEbook()
        {
            Console.WriteLine("Cadastrando Ebook: ");
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Preço: ");
            float valor = float.Parse(Console.ReadLine());
            Console.WriteLine("Autor: ");
            string autor = Console.ReadLine();

            Ebook eb = new Ebook(nome, valor, autor);
            produtos.Add(eb);
            Salvar();
        }
        static void CadastrarCurso()
        {
            Console.WriteLine("Cadastrando Curso: ");
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Preço: ");
            float valor = float.Parse(Console.ReadLine());
            Console.WriteLine("Autor: ");
            string autor = Console.ReadLine();

            Curso cs = new Curso(nome, valor, autor);
            produtos.Add(cs);
            Salvar();
        }

        static void Salvar()
        {
            FileStream stream = new FileStream("produtos.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, produtos);
            stream.Close();
        }

        static void Carregar()
        {
            FileStream stream = new FileStream("produtos.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            

            try
            {
                produtos = (List<IEstoque>)encoder.Deserialize(stream);
                if(produtos == null)
                {
                     produtos = new List<IEstoque>();
                }
            }
            catch (Exception e)
            {
                produtos = new List<IEstoque>();
            }

            stream.Close();
        }
    }
}
