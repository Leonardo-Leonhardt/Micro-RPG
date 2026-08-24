using System;
using System.Text;
using Micro_RPG.Models;

namespace Micro_RPG.Models.Mobs;

public abstract class Mob : Entidade
{
    /// <summary>
    /// Inicializa uma nova instância do Mob.
    /// </summary>
    /// <param name="nome">O nome do mob.</param>
    /// <param name="vida">A vida do mob.</param>
    /// <param name="dano">O dano do mob.</param>
    /// <param name="chanCritico">A chance de acerto crítico do mob.</param>
    /// <param name="chanEvasao">A chance de evasão do mob.</param>
    protected Mob(string nome, int vida, int dano, double chanCritico, double chanEvasao)
        : base(
            nome,
            vida,
            dano,
            chanCritico,
            chanEvasao)
    {
    }

    /// <summary>
    /// Gera a vida do mob com base no turno atual.
    /// </summary>
    /// <param name="turnoAtual">O turno atual.</param>
    /// <param name="vidaBase">A vida base do mob.</param>
    /// <param name="bonusVida">O bônus de vida por ciclo.</param>
    /// <returns>A vida gerada para o mob.</returns>
    protected static int GeraVida(int turnoAtual, int vidaBase, int bonusVida)
    {
        int ciclo = (turnoAtual - 1) / 5;

        int bonusPorCiclo = ciclo * bonusVida;

        int vidaNoCiclo = (turnoAtual % 5) switch
        {
            1 => 0,
            2 => 4,
            3 => 9,
            4 => 15,
            _ => 0
        };

        return vidaBase + bonusPorCiclo + vidaNoCiclo;
    }

    /// <summary>
    /// Gera o dano do mob com base no turno atual.
    /// </summary>
    /// <param name="turnoAtual">O turno atual.</param>
    /// <param name="danoBase">O dano base do mob.</param>
    /// <param name="bonusDano">O bônus de dano por ciclo.</param>
    /// <returns>O dano gerado para o mob.</returns>
    protected static int GeraDano(int turnoAtual, int danoBase, int bonusDano)
    {
        int ciclo = (turnoAtual - 1) / 5;
        int bonusCiclo = ciclo > 0 ? (ciclo / bonusDano) : 0;

        return danoBase + bonusCiclo;
    }

    /// <summary>
    /// Adiciona o nome do mob ao StringBuilder.
    /// </summary>
    /// <param name="sb">O StringBuilder ao qual adicionar o nome.</param>
    protected override void AdicionarNome(StringBuilder sb)
    {
        sb.AppendLine($"Enemy: {Nome}");
    }
}
