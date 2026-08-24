using System;
using System.Text;

namespace Micro_RPG.Models.Mobs;

public class Esqueleto : Mob
{
    /// <summary>
    /// Inicializa uma nova instância do Esqueleto.
    /// </summary>
    /// <param name="turnoAtual">O turno atual.</param>
    public Esqueleto(int turnoAtual) 
        : base(
            nome: "Esqueleto",
            vida: GeraVida(turnoAtual, vidaBase: 25, bonusVida: 5),
            dano: GeraDano(turnoAtual, danoBase: 6, bonusDano: 7),
            chanCritico: 0.05,
            chanEvasao: 0.10)
    {
    }
}
