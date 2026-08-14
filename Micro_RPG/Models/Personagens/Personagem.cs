using System;
using System.Text;
using Micro_RPG.Models;

public abstract class Personagem : Entidade
{

    protected int _vidaMax;
    protected double _recuperaVida;

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

    public void AumentarVidaMax(int upVida)
    {
        _vidaMax += upVida;
    }

    public void AumentarDano(int upDano)
    {
        _dano += upDano;
    }

    public void AumentarVida()
    {
        _vida += (int)(_vidaMax * _recuperaVida);

        if (_vida > _vidaMax)
        {
            _vida = _vidaMax;
        }
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