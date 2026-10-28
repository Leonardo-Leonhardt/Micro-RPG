using System;
using System.Text;

namespace Micro_RPG.Models.Mobs;

public class GoblinHero : Mob
{
    /// <summary>
    /// Inicializa uma nova instância do GoblinHero.
    /// </summary>
    /// <param name="turnoAtual">O turno atual.</param>
    /// <param name="num">O número do Goblin Hero.</param>
    public GoblinHero(int turnoAtual, int num)
        : base(
            nome: $"Goblin Hero",
            vida: GeraVida(turnoAtual, vidaBase: 50, bonusVida: 10),
            dano: GeraDano(turnoAtual, danoBase: 10, bonusDano: 5),
            chanCritico: 0.15,
            chanEvasao: 0.25)
    {
    }
}