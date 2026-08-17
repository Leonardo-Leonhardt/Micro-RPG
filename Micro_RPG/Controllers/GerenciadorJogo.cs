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
        int valor = Random.Shared.NextDouble()

       int pontosGalhos = valor switch
       {
           < 0.05 => up + 1,
           < 0.0001 => up + 2,
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

    private bool Aumenta()
    {



    }

    private List<Mob> CriarMobs()
    {
        List<Mob> mobs = new List<Mob>();

        return mobs;
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
