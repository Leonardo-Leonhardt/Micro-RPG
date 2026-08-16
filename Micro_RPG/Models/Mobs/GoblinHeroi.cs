using System;
using System.Text;
using Micro_RPG.Models.Mobs;

public class GoblinHeroi : Mob
{
    public GoblinHeroi(int turnoAtual)
        : base(
            nome: "Goblin Heroi",
            vida: GeraVida(turnoAtual, vidaBase: 50, bonusVida: 10),
            dano: GeraDano(turnoAtual, danoBase: 10, bonusDano: 5),
            chanCritico: 0.15,
            chanEvasao: 0.25)
    {
    }

    protected override void AdicionarNome(StringBuilder sb)
    {
        sb.AppendLine($"Enemy: {_nome}");
    }

}
