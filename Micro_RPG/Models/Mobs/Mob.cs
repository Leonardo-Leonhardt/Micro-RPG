using System;
using System.Text;
using Micro_RPG.Models;

namespace Micro_RPG.Models.Mobs;

public abstract class Mob : Entidade
{
    protected Mob(string nome, int vida, int dano, double chanCritico, double chanEvasao)
        : base(
            nome,
            vida,
            dano,
            chanCritico,
            chanEvasao)
    {
    }

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

    protected static int GeraDano(int turnoAtual, int danoBase, int bonusDano)
    {
        int ciclo = (turnoAtual - 1) / 5;
        int bonusCiclo = ciclo > 0 ? (ciclo / bonusDano) : 0;

        return danoBase + bonusCiclo;
    }

    protected override void AdicionarNome(StringBuilder sb)
    {
        sb.AppendLine($"Enemy: {_nome}");
    }
}
