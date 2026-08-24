using System;
using Micro_RPG.Models.Mobs;

namespace Micro_RPG.Models.Factory;

public static class MobFactory
{
    /// <summary>
    /// Cria uma nova instância de mob com base no tipo especificado.
    /// </summary>
    /// <param name="tipo">O tipo de mob a ser criado.</param>
    /// <param name="turno">O turno atual.</param>
    /// <returns>A instância do mob criado.</returns>
    public static Mob CriarMob(string tipo, int turno)
    {
        return tipo.ToLower() switch
        {
            "goblin hero" => new GoblinHero(turno),
            "goblin" => new Goblin(turno),
            "esqueleto" => new Esqueleto(turno),
            _ => throw new ArgumentException($"Tipo de mob desconhecido: {tipo}")
        };
    }
}
