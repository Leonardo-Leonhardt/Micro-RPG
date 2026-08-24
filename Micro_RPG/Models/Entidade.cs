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

    /// <summary>
    /// Faz a entidade atacar.
    /// </summary>
    /// <returns>O dano causado pelo ataque.</returns>
    public virtual int Atacar()
    {
        if (Critico())
        {
            return (_dano + _dano);
        }

        return _dano;
    }

    /// <summary>
    /// Faz a entidade receber dano.
    /// </summary>
    /// <param name="dano">O dano a ser recebido.</param>
    /// <returns>true se a entidade recebeu dano, false caso contrário.</returns>
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

    /// <summary>
    /// Verifica se a entidade acertou um golpe crítico.
    /// </summary>
    /// <returns>true se a entidade acertou um golpe crítico, false caso contrário.</returns>
    protected bool Critico()
    {
        if (Random.Shared.NextDouble() < _chanCritico)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Verifica se a entidade conseguiu evadir o ataque.
    /// </summary>
    /// <returns>true se a entidade evitou o ataque, false caso contrário.</returns>
    protected bool Evasao()
    {
        if (Random.Shared.NextDouble() < _chanEvasao)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Obtém o valor da vida da entidade.
    /// </summary>
    public int Vida => _vida;

    /// <summary>
    /// Obtém o nome da entidade.
    /// </summary>
    public string Nome => _nome;

    /// <summary>
    /// Adiciona informações adicionais sobre a entidade ao StringBuilder. Este método pode ser sobrescrito em classes derivadas para incluir informações específicas.
    /// </summary>
    /// /// <param name="sb">O StringBuilder ao qual adicionar o nome.</param>
    protected virtual void AdicionarMaxHp(StringBuilder sb) { }

    /// <summary>
    /// Adiciona o nome da entidade ao StringBuilder. Este método pode ser sobrescrito em classes derivadas para incluir informações específicas.
    /// </summary>
    /// <param name="sb">O StringBuilder ao qual adicionar o nome.</param>
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
