using Micro_RPG.Controllers;
using Micro_RPG.Models.Mobs;
using Micro_RPG.Models.Factory;
using Micro_RPG.Models.Personagens;

namespace Micro_RPG
{
    internal class Program
    {
        static GerenciadorJogo jogo;

        static void Main(string[] args)
        {
            List<Mob> mobs;
            jogo = new GerenciadorJogo("Guerreiro", "Herói");
            jogo.CriarMobs();
            //CriarPersonagem();
            //CriarInimigo();


            Mob mob = ExecutarCombate();

            if (jogo.GameOver())
            {
                ShowLoading(jogo.GameOver() == true ? $"\nVocê morreu!!! " +
                                                                 $"\nO jogo acabou!" :
                                                                 "\nO jogo continua...");
            }
            else if (mob != null)
            {
                jogo.TurnoConcluido(mob.Nome);

                ShowCabecalho($"Turno {jogo.Turno} finalisado");

                Console.WriteLine($"\nPersonagem:" +
                                  $"\n{jogo.Personagem.ToString()}");


                int opcao = 0;

                Console.WriteLine("\nEscolha seu bonus");
                Console.WriteLine("1 - Vida");
                Console.WriteLine("2 - Dano\n");

                opcao = Convert.ToInt32(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        jogo.EscolherBonus("vida");
                        break;
                    case 2:
                        jogo.EscolherBonus("dano");
                        break;
                }






                Console.WriteLine($"\nPersonagem:" +
                                  $"\n{jogo.Personagem.ToString()}");
                Console.WriteLine("Inimigos na horda:");
                foreach (var moob in jogo.HordaInimigo)
                {
                    Console.WriteLine(moob.ToString());
                }


            }
        }



        #region Menus
        /// <summary>
        /// Exibe o menu principal do jogo, permitindo ao jogador escolher sua classe ou sair do jogo.
        /// </summary>
        static void Menu()
        {
            ShowCabecalho("BEM-VINDO AO MINI RPG DE TERMINAL");

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
        #endregion

        #region ShowLoading
        /// <summary>
        /// Exibe uma animação de carregamento no console, imprimindo o texto fornecido caractere por caractere com um atraso entre cada caractere. Pode limpar a tela antes de exibir a animação, se especificado.
        /// </summary>
        /// <param name="texto">O texto a ser exibido com o efeito de carregamento.</param>
        /// <param name="limparTela">Indica se a tela deve ser limpa antes de exibir a animação.</param>
        static void ShowLoading(string texto, bool limparTela = false)
        {
            if (limparTela)
            {
                Console.Clear();
            }

            foreach (char c in texto)
            {
                Console.Write(c);
                int tempoEspera = (c == '.' || c == '!') ? 60 : 10;
                Thread.Sleep(tempoEspera);
            }
        }
        #endregion

        #region cabecalho
        /// <summary>
        /// Exibe um cabecalho no console com o título fornecido, centralizado entre linhas de separação.
        /// </summary>
        /// <param name="titulo">O título a ser exibido no cabecalho.</param>
        static void ShowCabecalho(string titulo)
        {
            Console.Clear();
            Console.WriteLine("=========================================");
            Console.WriteLine($"⚔️  {titulo}  ⚔️");
            Console.WriteLine("=========================================");
        }
        #endregion
        static void IniciarJogo()
        {
            CriarPersonagem();
        }

        #region Criações
        /// <summary>
        /// Permite ao jogador criar um personagem, escolhendo sua classe e fornecendo um nome. O método exibe mensagens de carregamento durante o processo de criação do personagem.
        /// </summary>
        static void CriarPersonagem()
        {
            string cabecalho = "CRIAÇÃO DE PERSONAGEM";
            string mensagem = $"Criando personagem";
            string classPersonagem = EscolherPersonagem(cabecalho);

            ShowCabecalho(cabecalho);

            Console.WriteLine("\nDigite o nome do seu personagem:");
            string nome = Console.ReadLine();

            jogo = new GerenciadorJogo(classPersonagem, nome);


            ShowCabecalho(cabecalho);
            ShowLoading($"\n{mensagem} {jogo.NomePersonagem}...");
            ShowCabecalho(cabecalho);
            ShowLoading($"\nPersonagem {jogo.NomePersonagem} criado com sucesso!", false);
        }

        /// <summary>
        /// Permite ao jogador escolher o tipo de personagem que deseja criar, exibindo opções disponíveis e validando a entrada do usuário. Retorna o tipo de personagem escolhido como uma string.
        /// </summary>
        /// <param name="cabecalho">O cabecalho a ser exibido no menu de escolha.</param>
        /// <returns>O tipo de personagem escolhido.</returns>
        static string EscolherPersonagem(string cabecalho)
        {
            string mensagem = $"Personagem escolhido: ";

            while (true)
            {
                ShowCabecalho(cabecalho);
                Console.WriteLine("\nEscolha o tipo de personagem:");
                Console.WriteLine("1 - Guerreiro\n");

                if (int.TryParse(Console.ReadLine(), out int opcao))
                {
                    string resultado = opcao switch
                    {
                        1 => "Guerreiro",
                        _ => null
                    };

                    if (resultado != null)
                    {
                        ShowCabecalho(cabecalho);
                        ShowLoading($"\n{mensagem + resultado}!!!");

                        return resultado;
                    }
                }

                ShowCabecalho(cabecalho);
                ShowLoading("\nOpção inválida!!!", false);
                ShowLoading("\nTente novamente...", false);
            }
        }

        /// <summary>
        /// Cria inimigos para o jogo, exibindo mensagens de carregamento durante o processo.
        /// O método chama a função <see cref="GerenciadorJogo.CriarMobs"/> do GerenciadorJogo e exibe a quantidade de inimigos gerados ou uma mensagem indicando que um Boss foi criado.
        /// </summary>
        static void CriarInimigo()
        {
            string cabecalho = "CRIAÇÃO DE INIMIGO";

            ShowCabecalho(cabecalho);

            ShowLoading("\nGerando inimigo...");

            int quantidadeDeMobs = jogo.CriarMobs();

            if (quantidadeDeMobs > 0)
            {
                ShowCabecalho(cabecalho);
                ShowLoading($"\nQuantidade de inimigos gerados: {quantidadeDeMobs}!!!", false);
            }
            else
            {
                ShowCabecalho(cabecalho);
                ShowLoading("\n1 Boss criado.", false);
            }
        }
        #endregion

        #region Ataques
        static void AtacarInimigo()
        {
            var (acertou, dano, critico) = jogo.AtacarMob();


            ShowLoading(critico == true ? $"\nAtaque crítico! \nVocê acertou o inimigo e causou {dano} de dano!" : acertou ? $"\nVocê acertou o inimigo e causou {dano} de dano!" : "\nVocê errou o ataque!");
        }

        static void AtacarPersonagem()
        {
            var (acertou, dano, critico) = jogo.AtacarPersonagem();

            ShowLoading(critico == true ? $"\nAtaque crítico! \nO {jogo.HordaInimigo[0].Nome} acertou você e causou {dano} de dano!" : acertou ? $"\nO {jogo.HordaInimigo[0].Nome} acertou você e causou {dano} de dano!" : "\nO inimigo errou o ataque!");
        }
        #endregion

        #region Combate
        static Mob? ExecutarCombate()
        {
            List<Mob> mobs;

            do
            {
                Mob mob;
                mobs = jogo.HordaInimigo;

                AtacarInimigo();

                if (mobs[0].Vida <= 0)
                {
                    return mob = jogo.VerificarMobDerrotado();
                }

                AtacarPersonagem();

            } while (mobs.Count() != 0 && !jogo.GameOver());

            return null;
        }
        #endregion



    }
}