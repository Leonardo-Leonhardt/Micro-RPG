using System;
using System.Text;
using Micro_RPG.Models.Personagens;

namespace Micro_RPG.Models.Personagens;

public class Guerreiro : Personagem
{
    /// <summary>
    /// Inicializa uma nova instância do Guerreiro.
    /// </summary>
    public Guerreiro(string nome)
        : base(
            nome: nome,
            vida: 40,
            dano: 5,
            chanCritico: 0.10,
            chanEvasao: 0.05,
            recuperaVida: 0.50)
    {
        _Class = "Guerreiro";
    }

    /// <summary>
    /// Faz o guerreiro atacar.
    /// </summary>
    /// <returns>O dano causado pelo ataque.</returns>
    public override int Atacar()
    {
        int danoMinimo = Math.Max(1, _dano - 3);
        int danoMaximo = _dano + 1;
        int danoDoTurno = Random.Shared.Next(danoMinimo, danoMaximo);

        if (Critico())
        {
            return (danoDoTurno + danoDoTurno);
        }

        return danoDoTurno;
    }
}
