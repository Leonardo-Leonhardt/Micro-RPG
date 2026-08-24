using Micro_RPG.Models.Personagens;

namespace Micro_RPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        /// <summary>
        /// Exibe o menu principal do jogo, permitindo ao jogador escolher sua classe ou sair do jogo.
        /// </summary>
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("=========================================");
            Console.WriteLine("⚔️  BEM-VINDO AO MINI RPG DE TERMINAL  ⚔️");
            Console.WriteLine("=========================================");

            int opcao = 0;

            Console.WriteLine("\nEscolha sua classe");
            Console.WriteLine("1 - Guerreiro");
            Console.WriteLine("0 - Sair\n");

            opcao = Convert.ToInt32(Console.ReadLine());

            switch (opcao)
            {
                case 0:
                    Console.Clear();
                    Console.WriteLine("Saindo do jogo...");
                    Thread.Sleep(2000);
                    break;
                case 1:
                    Console.WriteLine("\nVocê escolheu a classe Guerreiro!");
                    Thread.Sleep(2000);
                    IniciarJogo();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    Thread.Sleep(2000);
                    Menu();
                    break;
            }
            ;
        }

        static void IniciarJogo()
        {


        }




    }
}


