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

    private List<Mob> CriarMobs()
    {
        List<Mob> mobs = new List<Mob>();

        return mobs;
    }

    private bool VerificarGameOver()
    {
        return _Personagem.Vida <= 0;
    }

    public int Turno => _Turno;

}
