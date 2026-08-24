using System;
using Micro_RPG.Models.Personagens;

namespace Micro_RPG.Models.Factory;

public static class PersonagemFactory
{
    /// <summary>
    /// Cria uma nova instância de personagem com base no tipo especificado.
    /// </summary>
    /// <param name="tipo">O tipo de personagem a ser criado.</param>
    /// <returns>A instância do personagem criado.</returns>
    public static Personagem CriarPersonagem(string tipo)
    {
        return tipo.ToLower() switch
        {
            "guerreiro" => new Guerreiro(),
            _ => throw new ArgumentException($"Tipo de personagem desconhecido: {tipo}")
        };
    }
}
