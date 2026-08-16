using System;
using System.Collections.Generic;
using Micro_RPG.Models;
using Micro_RPG.Models.Factory;
using Micro_RPG.Models.Personagem;

namespace Micro_RPG.Controllers;

public class GerenciadorJogo
{
    private Personagem _Personagem;
    private List<Mobs> _HordaInimigo;
    private int _Turno;

    public GerenciadorJogo(string tipoPersonagem)
    {
        _Personagem = PersonagemFactory.CriarPersonagem(tipoPersonagem);
        _HordaInimigo = new List<Mobs>();
        _Turno = 0;
    }

    public bool IniciarJogo()
    {
        
    }

    public void ExjecutarTurno()
    {

    }

    private bool VerificarGamerOver()
    {
        return _Personagem.Vida <= 0;
    }

    public int Turno => _Turno;

    private List<Mobs> CriarMobs()
    {

    }

}
