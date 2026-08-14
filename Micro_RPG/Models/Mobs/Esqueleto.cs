using System;
using Micro_RPG.Models;


public class Esqueleto : Mob
{
	public Esqueleto()
	{
        _vida = 20;
        _dano = 5;
        _chanCritico = 0.05;
        _chanEvasao = 0.10;
    }
}
