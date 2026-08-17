using System;
using System.Text;

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
}
