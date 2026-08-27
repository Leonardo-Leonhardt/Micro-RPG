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
    public GerenciadorJogo(string tipoPersonagem, string nomePersonagem)
    {
        _personagem = PersonagemFactory.CriarPersonagem(tipoPersonagem, nomePersonagem);
        _hordaInimigo = new List<Mob>();
        _turno = 0;
    }

    /// <summary>
    /// Escolhe o bônus para o personagem com base na escolha.
    /// </summary>
    /// <param name="escolha">A escolha do personagem.</param>
    /// <returns>O número de pontos de ganhos obtidos.</returns>
    public int EscolherBonus(string escolha)
    {
        int up = 1;
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
    /// Escolhe o bônus para o personagem após derrotar um boss.
    /// </summary>
    /// <returns>Uma tupla com os pontos de vida e dano ganhos.</returns>
    private (int vida, int dano) BonusDerrotaBoss()
    {
        return (vida: EscolherBonus("vida"), dano: EscolherBonus("dano"));
    }

    /// <summary>
    /// Cria mobs para o turno atual. A cada 5 turnos, um "Goblin Hero" é criado. Nos outros turnos, uma quantidade de mobs é criada com base no número do turno.
    /// </summary>
    /// <returns>A quantidade de mobs criados.</returns>
    public int CriarMobs()
    {
        ++_turno;

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

        return quantidadeMobs;
    }

    /// <summary>
    /// Verifica se o jogo terminou.
    /// </summary>
    /// <returns>true se o jogo terminou, false caso contrário.</returns>
    public bool VerificarGameOver()
    {
        return _personagem.Vida <= 0;
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
    public (bool Acertou, int Dano, bool Critico) AtacarMob()
    {
        return Atacar(_personagem);
    }

    /// <summary>
    /// Ataca o personagem e retorna se o ataque acertou e o dano causado.
    /// </summary>
    /// <returns>Uma tupla com o resultado do ataque.</returns>
    public (bool Acertou, int Dano, bool Critico) AtacarPersonagem()
    {
        return Atacar(_hordaInimigo[0]);
    }

    /// <summary>
    /// Ataca uma entidade e retorna se o ataque acertou e o dano causado.
    /// </summary>
    /// <param name="entidade">A entidade a ser atacada.</param>
    /// <returns>Uma tupla com o resultado do ataque.</returns>
    private (bool Acertou, int Dano, bool Critico) Atacar(Entidade entidade)
    {
        var (dano, critico) = entidade.Atacar();
        bool recebeuDano = true;

        if (entidade is Personagem)
        {
            recebeuDano = _hordaInimigo[0].ReceberDano(dano);
        }
        else if (entidade is Mob)
        {
            recebeuDano = _personagem.ReceberDano(dano);
        }

        return (recebeuDano, dano, critico );
    }

    /// <summary>
    /// Remove um mob derrotado da horda.
    /// </summary>
    /// <returns>true se um mob foi removido, false caso contrário.</returns>
    private Mob? RemoverMobDerrotado()
    {
        if (_hordaInimigo.Count > 0)
        {
            Mob mobEliminado = _hordaInimigo[0];

            _hordaInimigo.RemoveAt(0);

            return mobEliminado;
        }

        return null;
    }

    /// <summary>
    /// Verifica se o turno foi completo, ou seja, se todos os inimigos foram derrotados. Se a horda de inimigos estiver vazia e o nome do mob for "Goblin Hero", um bônus é concedido ao personagem.
    /// </summary>
    /// <param name="nome">O nome do mob derrotado.</param>
    /// <returns>true se o turno foi completo, false caso contrário.</returns>
    public bool TurnoCompleto(String? nome)
    {
        if (_hordaInimigo.Count <= 0)
        {
            if (nome == "Goblin Hero")
            {
                BonusDerrotaBoss();
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// Verifica se o primeiro mob da horda foi derrotado. Se sim, ele é removido da horda e retornado. Caso contrário, retorna null.
    /// </summary>
    /// <returns>o mob derrotado ou null se nenhum mob foi derrotado.</returns>
    public Mob? VerificarMobDerrotado()
    {
        if (_hordaInimigo[0].Vida <= 0)
        {
            return RemoverMobDerrotado();
        }

        return null;
    }

    /// <summary>
    /// Verifica se o turno foi concluído, ou seja, se todos os inimigos foram derrotados. Se a horda de inimigos estiver vazia, o personagem tenta recuperar vida.
    /// </summary>
    /// <returns>true se o turno foi concluído, false caso contrário.</returns>
    public bool TurnoConcluido()
    {
        if(_hordaInimigo.Count == 0)
        {
            RecuperarVida();
        }

        return _hordaInimigo.Count == 0;
    }

    /// <summary>
    /// Retorna o Turno atual do jogo.
    /// </summary>
    public int Turno => _turno;

    /// <summary>
    /// Retorna o nome do personagem criado.
    /// </summary>
    public string NomePersonagem => _personagem.Nome;

    /// <summary>
    /// Retorna o personagem criado.
    /// </summary>
    public Personagem Personagem => _personagem;

    /// <summary>
    /// Retorna a lista de mobs inimigos da horda.
    /// </summary>
    public List<Mob> HordaInimigo => _hordaInimigo;
}