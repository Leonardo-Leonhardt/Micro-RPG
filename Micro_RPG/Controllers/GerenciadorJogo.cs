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
    private string[] _nomeMobs = { "Goblin", "Esqueleto" };

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
    /// <returns>O número de pontos de ganhos obtidos.</returns>
    private int EscolherBonus(string escolha)
    {
        int up = 2;
        double valor = Random.Shared.NextDouble();

        int pontosGanhos = valor switch
        {
            < 0.0001 => up + 2,
            < 0.05 => up + 1,
            _ => up
        };

        escolha = escolha.ToLower();

        if (escolha == "vida")
        {
            _personagem.AumentarVida(pontosGanhos);
        }
        else if (escolha == "dano")
        {
            _personagem.AumentarDano(pontosGanhos);
        }

        return pontosGanhos;

    }

    /// <summary>
    /// Cria os inimigos para o turno.
    /// </summary>
    /// <returns>0 se um boss foi criado, -1 caso contrário.</returns>
    private int CriarMobs()
    {
        _turno++;

        if (_turno % 5 == 0)
        {
            _hordaInimigo.Add(MobFactory.CriarMob("Goblin Hero", _turno));

            return 0;
        }

        int quantidadeMobs = Math.Min(1 + ((_turno - 1) / 10), 5);

        for (int i = 0; i < quantidadeMobs; i++)
        {
            _hordaInimigo.Add(MobFactory.CriarMob(_nomeMobs[Random.Shared.Next(0, _nomeMobs.Length)], _turno));
        }

        return -1;
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
    /// <returns>Uma tupla com o resultado da recuperação de vida.</returns>
    private (bool Recuperou, int Cura) RecuperarVida()
    {
        return _personagem.RecuperarVida();
    }

    /// <summary>
    /// Ataca um inimigo e retorna se o ataque acertou e o dano causado.
    /// </summary>
    /// <returns>Uma tupla com o resultado do ataque.</returns>
    private (bool Acertou, int Dano) AtacarMob()
    {
        return Atacar(_personagem);
    }

    /// <summary>
    /// Ataca o personagem e retorna se o ataque acertou e o dano causado.
    /// </summary>
    /// <returns>Uma tupla com o resultado do ataque.</returns>
    private (bool Acertou, int Dano) AtacarPersonagem()
    {
        return Atacar(_hordaInimigo[0]);
    }

    /// <summary>
    /// Ataca uma entidade e retorna se o ataque acertou e o dano causado.
    /// </summary>
    /// <param name="entidade">A entidade a ser atacada.</param>
    /// <returns>Uma tupla com o resultado do ataque.</returns>
    private (bool Acertou, int Dano) Atacar(Entidade entidade)
    {
        int dano = entidade.Atacar();
        bool recebeuDano = true;

        if (entidade is Personagem)
        {
            recebeuDano = _hordaInimigo[0].ReceberDano(dano);
        }
        else if (entidade is Mob)
        {
            recebeuDano = _personagem.ReceberDano(dano);
        }

        return (recebeuDano, dano);
    }

    /// <summary>
    /// Remove um mob derrotado da horda.
    /// </summary>
    /// <returns>true se um mob foi removido, false caso contrário.</returns>
    private bool RemoverMobDerrotado()
    {
        if (_hordaInimigo.Count > 0 && _hordaInimigo[0].Vida <= 0)
        {
            _hordaInimigo.RemoveAt(0);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Retorna o Turno atual do jogo.
    /// </summary>
    public int Turno => _turno;
}