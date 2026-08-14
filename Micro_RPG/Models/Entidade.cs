using System;
using System.Text;

namespace Micro_RPG.Models;

public abstract class Entidade
{
    protected string _nome;
    protected int _vida;
    protected int _dano;
    protected double _chanCritico;
    protected double _chanEvasao;

    protected Entidade(string nome, int vida, int dano, double chanCritico, double chanEvasao)
    {
        _nome = nome;
        _vida = vida;
        _dano = dano;
        _chanCritico = chanCritico;
        _chanEvasao = chanEvasao;
    }

    public virtual int Atacar()
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

            if (_vida < 0)
            {
                _vida = 0;
            }

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

    public int Vida => _vida;
    public string Nome => _nome;

    protected virtual void AdicionarMaxHp(StringBuilder sb) { }
    protected virtual void AdicionarNome(StringBuilder sb) { }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        AdicionarNome(sb);
        sb.AppendLine($"HP: {_vida}");
        AdicionarMaxHp(sb);
        sb.AppendLine($"Attack: {_dano}");
        sb.AppendLine($"Critical Chance: {_chanCritico * 100}%");
        sb.AppendLine($"Evasion Chance: {_chanEvasao * 100}%");

        return sb.ToString();
    }
}
