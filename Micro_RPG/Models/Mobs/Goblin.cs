using System;
using Micro_RPG.Models;

public class Goblin : Mob
{
	public Goblin()
	{
        _vida = 30;
        _dano = 3;
        _chanCritico = 0.10;
        _chanEvasao = 0.20;
    }
}
