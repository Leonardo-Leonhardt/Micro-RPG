using System;
using System.Text;
using Micro_RPG.Models;

namespace Micro_RPG.Models.Personagens;

public abstract class Personagem : Entidade
{
    protected string _Class;
    protected int _vidaMax;
    protected double _recuperaVida;
    protected double _CuraMilagrosa = 0.0001;

    /// <summary>
    /// Inicializa uma nova instância do Personagem.
    /// </summary>
    /// <param name="nome">O nome do personagem.</param>
    /// <param name="vida">A vida inicial do personagem.</param>
    /// <param name="dano">O dano inicial do personagem.</param>
    /// <param name="chanCritico">A chance de crítico do personagem.</param>
    /// <param name="chanEvasao">A chance de evasão do personagem.</param>
    /// <param name="recuperaVida">A taxa de recuperação de vida do personagem.</param>
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

    /// <summary>
    /// Tenta recuperar vida do personagem.
    /// </summary>
    /// <returns>Uma tupla com o resultado da recuperação de vida.</returns>
    public (bool Recuperou, int Cura) RecuperarVida()
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
            return (Recuperou: true, Cura: cura);
        }

        return (Recuperou: false, Cura: 0);
    }

    /// <summary>
    /// Aumenta a vida máxima do personagem.
    /// </summary>
    /// <param name="upVida">O valor a ser adicionado à vida máxima.</param>
    /// <returns>true se a vida máxima foi aumentada, false caso contrário.</returns>
    public bool AumentarVida(int upVida)
    {
        _vidaMax += upVida;

        return true;
    }

    /// <summary>
    /// Aumenta o dano do personagem.
    /// </summary>
    /// <param name="upDano">O valor a ser adicionado ao dano.</param>
    /// <returns>true se o dano foi aumentado, false caso contrário.</returns>
    public bool AumentarDano(int upDano)
    {
        _dano += upDano;

        return true;
    }

    /// <summary>
    /// Adiciona informações sobre a vida máxima do personagem ao StringBuilder.
    /// </summary>
    /// <param name="sb">O StringBuilder ao qual adicionar as informações.</param>
    protected override void AdicionarMaxHp(StringBuilder sb)
    {
       sb.AppendLine($"Max HP: {_vidaMax}");
    }

    /// <summary>
    /// Adiciona informações sobre o nome do personagem ao StringBuilder.
    /// </summary>
    /// <param name="sb">O StringBuilder ao qual adicionar as informações.</param>
    protected override void AdicionarNome(StringBuilder sb)
    {
        sb.AppendLine($"Nome: {Nome}");
    }

    /// <summary>
    /// Adiciona informações sobre a classe do personagem ao StringBuilder.
    /// </summary>
    /// <param name="sb">O StringBuilder ao qual adicionar as informações.</param>
    protected override void AdicionarClass(StringBuilder sb)
    {
        sb.AppendLine($"Class: {_Class}");
    }
}