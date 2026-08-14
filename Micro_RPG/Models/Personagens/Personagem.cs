using System;
using System.Text;
using Micro_RPG.Models;

namespace Micro_RPG.Models.Personagens;

public class Personagem : Entidade
{

    protected int _vidaMax;
    protected double _recuperaVida = 0.50;


    public Personagem()
    {
        _vidaMax = 10;
        _dano = 20;
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

    public void AumentarVidaMax(int quantidade)
    {
        _vidaMax += quantidade;
    }

    public void AumentarVida()
    {
        _vida += (int)(_vidaMax * _recuperaVida);

        if (_vida > _vidaMax)
        {
            _vida = _vidaMax;
        }
    }

    public override string ToString()
    {
        
    }
}