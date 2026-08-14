using System;

namespace Micro_RPG.Models;

public abstract class Entidade
{
    protected int _vida;
    protected int _dano;
    protected double _chanCritico = 0.10;
    protected double _chanEvasao = 0.05;

    public int Atacar()
    {
        if (Critico())
        {
            return (_dano + _dano);
        }

        return _dano;
    }

    public bool ReceberDano(int dano)
    {
        if (!Evasao())
        {
            _vida -= dano;
            return true;
        }

        return false;
    }

    protected bool Critico()
    {
        if (Random.Shared.NextDouble() < _chanCritico)
        {
            return true;
        }
        return false;
    }

    protected bool Evasao()
    {
        if (Random.Shared.NextDouble() < _chanEvasao)
        {
            return true;
        }
        return false;
    }
}
