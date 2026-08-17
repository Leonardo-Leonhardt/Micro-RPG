using System;
using Micro_RPG.Models.Mobs;

namespace Micro_RPG.Models.Factory;

public static class MobFactory
{
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
