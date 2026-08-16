using System;
using Micro_RPG.Models.Personagem;

namespace Micro_RPG.Models.Factory;

public static class PersonagemFactory
{
	public static Personagem? CriarPersonagem(string tipo)
    {
        return tipo.ToLower() switch
        {
            "guerreiro" => new Guerreiro(),
            _ => throw new ArgumentException($"Tipo de personagem desconhecido: {tipo}")
        };
    } 
}
