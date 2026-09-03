using Micro_RPG.Controllers;
using Micro_RPG.Models.Mobs;
using Micro_RPG.Models.Factory;
using Micro_RPG.Models.Personagens;

namespace Micro_RPG
{
    internal class Program
    {
        static GerenciadorJogo jogo;

        #region Main
        static void Main(string[] args)
        {
            IniciarJogo();
        }
        #endregion

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
                int tempoEspera = (c == '.' || c == '!') ? 300 : 50;
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

        #region IniciarJogo
        static void IniciarJogo()
        {
            CriarPersonagem();

            do
            {
                CriarInimigo();
                Mob mob = ExecutarCombate();

                if (jogo.GameOver())
                {
                    GameOver();
                }
                else if (mob != null)
                {
                    jogo.TurnoConcluido(mob.Nome);

                    ShowCabecalho($"Turno {jogo.Turno} finalisado");

                    Console.WriteLine($"\nPersonagem:" +
                                      $"\n{jogo.Personagem.ToString()}");

                    EscolherBonus();

                }

            } while (!jogo.GameOver());
        }
        #endregion

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


            ShowLoading(critico ? $"\nAtaque crítico! \n{jogo.Personagem.Nome} acertou o inimigo e causou {dano} de dano!" : acertou ?
                                  $"\n{jogo.Personagem.Nome} acertou o inimigo e causou {dano} de dano!" :
                                  $"\n{jogo.Personagem.Nome} errou o ataque!");
        }

        static void AtacarPersonagem()
        {
            var (acertou, dano, critico) = jogo.AtacarPersonagem();

            ShowLoading(critico ? $"\nAtaque crítico! \nO {jogo.HordaInimigo[0].Nome} acertou {jogo.Personagem.Nome} e causou {dano} de dano!" : acertou ?
                                  $"\nO {jogo.HordaInimigo[0].Nome} acertou {jogo.Personagem.Nome} e causou {dano} de dano!" :
                                  $"\nO {jogo.HordaInimigo[0].Nome} errou o ataque!");
        }
        #endregion

        #region Combate
        static Mob? ExecutarCombate()
        {
            List<Mob> mobs;
            string cabecalho = $"COMBATE - TURNO {jogo.Turno}";

            do
            {
                Mob mob;
                mobs = jogo.HordaInimigo;

                ShowCabecalho(cabecalho);
                AtacarInimigo();

                if (mobs[0].Vida <= 0)
                {
                    return mob = jogo.VerificarMobDerrotado();
                }

                ShowCabecalho(cabecalho);
                AtacarPersonagem();

            } while (mobs.Count() != 0 && !jogo.GameOver());

            return null;
        }
        #endregion

        #region Bonus
        static void EscolherBonus()
        {
            int opcao = 0;
            string cabecalho = "ESCOLHA DE BONUS";
            string mensagem = $"Bonus de vida escolhido! Vida aumentada em";


            ShowCabecalho(cabecalho);
            VisualizarStatus(false);
            Console.WriteLine("\nEscolha seu bonus");
            Console.WriteLine("1 - Vida");
            Console.WriteLine("2 - Dano\n");
            opcao = Convert.ToInt32(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    var bonusVida = jogo.EscolherBonus("vida");
                    ShowLoading(bonusVida.critico ? $"Crítico!" : $"\nVida aumentada em {bonusVida.pontosGanhos}!", false);
                    break;
                case 2:
                    var bonusDano = jogo.EscolherBonus("dano");
                    ShowLoading(bonusDano.critico ? $"Crítico!" : $"\nDano aumentado em {bonusDano.pontosGanhos}!", false);
                    break;
            }
        }
        #endregion

        #region GameOver
        static void GameOver()
        {
            ShowCabecalho("GAME OVER");
            ShowLoading($"\nVocê morreu!!! " +
                        $"\nO jogo acabou!", false);
        }
        #endregion

        #region VisualizarStatus
        static void VisualizarStatus(bool visualizar)
        {
            if (visualizar)
            {
                ShowCabecalho("STATUS DO PERSONAGEM");
            }

            Console.WriteLine($"\n{jogo.Personagem.ToString()}");
        }
        #endregion
    }
}