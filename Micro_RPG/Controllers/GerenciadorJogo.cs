using System;
using System.Collections.Generic;
using Micro_RPG.Models;
using Micro_RPG.Models.Mobs;
using Micro_RPG.Models.Factory;
using Micro_RPG.Models.Personagens;


namespace Micro_RPG.Controllers;

public class GerenciadorJogo
{
    private Personagem _personagem;
    private List<Mob> _hordaInimigo;
    private int _turno;
    private int _quantidadeMobs = 1;
    private string[] _nomeMobs = { "Goblin", "Orc", "Troll" };

    /// <summary>
    /// Inicializa uma nova instância do GerenciadorJogo com o tipo de personagem especificado.
    /// </summary>
    /// <param name="tipoPersonagem">O tipo de personagem a ser criado.</param>
    public GerenciadorJogo(string tipoPersonagem)
    {
        _personagem = PersonagemFactory.CriarPersonagem(tipoPersonagem);
        _hordaInimigo = new List<Mob>();
        _turno = 0;
    }

    public bool IniciarJogo()
    {

        return true;
    }

    /// <summary>
    /// Escolhe o bônus para o personagem com base na escolha.
    /// </summary>
    /// <param name="escolha">A escolha do personagem.</param>
    /// <returns>O número de pontos de galhos obtidos.</returns>
    private int EscolherBonus(string escolha)
    {
        int up = 2;
        double valor = Random.Shared.NextDouble();

        int pontosGalhos = valor switch
        {
            < 0.0001 => up + 2,
            < 0.05 => up + 1,
            _ => up
        };

        escolha = escolha.ToLower();

        if (escolha == "vida")
        {
            _personagem.AumentarVida(pontosGalhos);
        }
        else if (escolha == "dano")
        {
            _personagem.AumentarDano(pontosGalhos);
        }

        return up;

    }

    /// <summary>
    /// Cria os inimigos para o turno.
    /// </summary>
    /// <returns>0 se um boss foi criado, -1 caso contrário.</returns>
    private int CriarMobs()
    {
        if (_turno % 5 == 0)
        {
            _hordaInimigo.Add(MobFactory.CriarMob("Boss", _turno));
            _quantidadeMobs++;

            return 0;
        }
        else
        {
            for (int i = 0; i < _quantidadeMobs; i++)
            {
                _hordaInimigo.Add(MobFactory.CriarMob(_nomeMobs[Random.Shared.Next(0, _nomeMobs.Length)], _turno));
            }

            return -1;
        }
    }

    /// <summary>
    /// Verifica se o jogo terminou.
    /// </summary>
    /// <returns>true se o jogo terminou, false caso contrário.</returns>
    private bool VerificarGameOver()
    {
        return _personagem.Vida <= 0;
    }

    /// <summary>
    /// Verifica se o jogador venceu.
    /// </summary>
    /// <returns>true se o jogador venceu, false caso contrário.</returns>
    private bool VerificarVitoria()
    {
        return _hordaInimigo.Count == 0;
    }

    /// <summary>
    /// Tenta recuperar vida do personagem.
    /// </summary>
    /// <returns>true se a vida foi recuperada, false caso contrário.</returns>
    private bool RecuperarVida()
    {
        return _personagem.RecuperarVida();
    }


    private int AtacarMob()
    {
        return 0;
    }

    private int AtacarPersonagem()
    {
        return 0;
    }

    /// <summary>
    /// Retorna o Turno atual do jogo.
    /// </summary>
    public int Turno => _turno;

}
