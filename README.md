# ⚔️ Mini RPG de Terminal

Um jogo de combate por turnos simples desenvolvido em **C#** via Console App, focado em sobrevivência, progressão contínua e gerenciamento de múltiplos inimigos simultâneos.

---

## 📑 Sobre o Projeto

O objetivo do jogador é sobreviver ao maior número de turnos possível. O jogo conta com um sistema de escolha de classes e evolução por sorte (RNG), enfrentando hordas que ficam progressivamente mais populosas e resistentes.

---

## 🛡️ Classe Inicial: Guerreiro

- **Dano Base:** 2 a 5 (gerado aleatoriamente a cada ataque).
- **Crítico:** 10% de chance de causar Dano Crítico.
- **Bônus de Sorte:** Ao escolher evoluir Vida, o jogador tem **0.10% de chance** (1 em 1000) de ganhar **+2 de HP Máximo** em vez de apenas +1.

---

## ⚖️ Mecânicas Gerais

- **Evolução por Turno:** A cada turno, o jogador escolhe:
  - `1` - **+1 HP Máximo** (com 0.10% de chance de virar +2).
  - `2` - **+1 Dano Base**.
- **Regeneração:** Ao final de cada turno vencido, o jogador recupera **50% do seu HP atual**.
- **Escalonamento de Inimigos:** A vida base de todos os monstros aumenta em **10% por turno**.
- **Multiplicação de Inimigos:** A cada **10 turnos**, um novo inimigo surge na horda (limite de **5 simultâneos**):
  - Turnos 1–9: 1 inimigo
  - Turnos 10–19: 2 inimigos
  - Turnos 20–29: 3 inimigos
  - Turnos 30–39: 4 inimigos
  - Turnos 40+: 5 inimigos

---

## 👾 Elenco de Inimigos

- **Monstros Comuns:** 
  - 💀 **Esqueleto:** Dano moderado e vida padronizada.
  - 👺 **Goblin:** Ataques rápidos e alta variabilidade.
- **Chefe (A cada 5 turnos):**
  - 👑 **Goblin Herói:** Surge com vida e dano significativamente elevados pelo escalonamento!

---

## 🛠️ Tecnologias Utilizadas

* **Linguagem:** C#
* **Plataforma:** .NET 10.0
* **Ambiente:** Console Application

---

## 🚀 Como Executar o Projeto

### Pré-requisitos
* [.NET SDK](https://dotnet.microsoft.com/download) instalado.

### Passo a Passo

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/Leonardo-Leonhardt/Micro-RPG.git
