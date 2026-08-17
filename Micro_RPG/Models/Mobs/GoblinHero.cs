using System;
using System.Text;

namespace Micro_RPG.Models.Mobs;

public class GoblinHero : Mob
{
    public GoblinHero(int turnoAtual)
        : base(
            nome: "Goblin Hero",
            vida: GeraVida(turnoAtual, vidaBase: 50, bonusVida: 10),
            dano: GeraDano(turnoAtual, danoBase: 10, bonusDano: 5),
            chanCritico: 0.15,
            chanEvasao: 0.25)
    {
    }
}