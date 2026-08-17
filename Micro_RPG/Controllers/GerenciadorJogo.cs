using System;
using System.Collections.Generic;
using Micro_RPG.Models;
using Micro_RPG.Models.Mobs;
using Micro_RPG.Models.Factory;
using Micro_RPG.Models.Personagens;


namespace Micro_RPG.Controllers;

public class GerenciadorJogo
{
    private Personagem _Personagem;
    private List<Mob> _HordaInimigo;
    private int _Turno;
    private int quantidadeMobs = 1;

    public GerenciadorJogo(string tipoPersonagem)
    {
        _Personagem = PersonagemFactory.CriarPersonagem(tipoPersonagem);
        _HordaInimigo = new List<Mob>();
        _Turno = 0;
    }

    public bool IniciarJogo()
    {
        return true;
    }

    public void ExecutarTurno()
    {

    }

    private int FinalizarTurno(string escolha)
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
            _Personagem.AumentarVida(pontosGalhos);
        }
        else if (escolha == "dano")
        {
            _Personagem.AumentarDano(pontosGalhos);
        }

        return up;

    }

    private List<Mob> CriarMobs()
    {
        List<Mob> mobs = new List<Mob>();

        if (_Turno % 5 == 0)
        {
            mobs.Add(MobFactory.CriarMob("Boss", _Turno));

            return mobs;
        }
        else
        {
            for (int i = 0; i < quantidadeMobs; i++)
            {
                mobs.Add(MobFactory.CriarMob("Inimigo", _Turno));
            }



            return mobs;
        }
    }

    private bool VerificarGameOver()
    {
        return _Personagem.Vida <= 0;
    }

    private bool RecuperarVida()
    {
        return _Personagem.RecuperarVida();
    }

    public int Turno => _Turno;

}
