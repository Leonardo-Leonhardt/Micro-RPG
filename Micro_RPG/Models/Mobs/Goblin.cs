using System;
using System.Text;
using Micro_RPG.Models.Mobs;

namespace Micro_RPG.Models.Mobs;

public class Goblin : Mob
{

    public Goblin(int turnoAtual)
        : base(
            nome: "Goblin",
            vida: GeraVida(turnoAtual, vidaBase: 15, bonusVida: 8),
            dano: GeraDano(turnoAtual, danoBase: 3, bonusDano: 4),
            chanCritico: 0.10,
            chanEvasao: 0.20)
    {
    }

    protected override void AdicionarNome(StringBuilder sb)
    {
        sb.AppendLine($"Enemy: {_nome}");
    }
}
