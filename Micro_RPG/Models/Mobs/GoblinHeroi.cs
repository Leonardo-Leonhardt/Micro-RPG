using System;
using System.Text;
using Micro_RPG.Models.Mobs;

public class GoblinHeroi : Mob
{
    public GoblinHeroi()
    {
        _vida = 50;
        _dano = 10;
        _chanCritico = 0.15;
        _chanEvasao = 0.25;
    }

    protected override void AdicionarNome(StringBuilder sb)
    {
        sb.AppendLine($"Enemy: {_nome}");
    }

}
