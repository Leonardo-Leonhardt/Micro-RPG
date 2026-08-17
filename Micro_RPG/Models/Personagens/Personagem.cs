using System;
using System.Text;
using Micro_RPG.Models;

namespace Micro_RPG.Models.Personagens;

public abstract class Personagem : Entidade
{
    protected int _vidaMax;
    protected double _recuperaVida;
    protected double _CuraMilagrosa = 0.0001;

    protected Personagem(string nome, int vida, int dano, double chanCritico, double chanEvasao, double recuperaVida)
        : base(
            nome,
            vida,
            dano,
            chanCritico,
            chanEvasao)
    {
        _vidaMax = vida;
        _recuperaVida = recuperaVida;
    }  

    public bool RecuperarVida()
    {
        if (_vida < _vidaMax)
        {
            int cura;

            if (Random.Shared.NextDouble() < _CuraMilagrosa)
            {
                cura = _vidaMax;
            }
            else
            {
                cura = (int)(_vidaMax * _recuperaVida);
            }

            cura = Math.Max(1, cura);
            _vida = Math.Min(_vidaMax, _vida + cura);
            return true;
        }

        return false;
    }

    public bool AumentarVida(int upVida)
    {
        _vidaMax += upVida;

        return true;
    }

    public bool AumentarDano(int upDano)
    {
        _dano += upDano;

        return true;
    }

    protected override void AdicionarMaxHp(StringBuilder sb)
    {
        sb.AppendLine($"Max HP: {_vidaMax}");
    }

    protected override void AdicionarNome(StringBuilder sb)
    {
        sb.AppendLine($"Class: {_nome}");
    }
}

