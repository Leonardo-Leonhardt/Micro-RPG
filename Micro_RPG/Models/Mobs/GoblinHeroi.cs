using System;
using Micro_RPG.Models;

public class GoblinHeroi : Mob
{
	public GoblinHeroi()
    {
        _vida = 50;
        _dano = 10;
        _chanCritico = 0.15;
        _chanEvasao = 0.25;
    }
}
