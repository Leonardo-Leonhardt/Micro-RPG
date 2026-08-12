# ⚔️ Mini RPG de Terminal

Um jogo de combate por turnos simples desenvolvido em **C#** via Console App, focado na progressão contínua e no combate contra ondas de monstros que ficam mais fortes com o tempo.

---

## 📑 Sobre o Projeto

O objetivo do jogador é sobreviver ao maior número de combates possível. O jogo conta com um sistema de evolução rápida: a cada turno, o jogador melhora seus atributos, enquanto os inimigos ficam progressivamente mais resistentes.

### ⚖️ Mecânicas do Jogo

- **Evolução por Turno:** A cada turno, o jogador escolhe aumentar **+1 de HP Máximo** ou **+1 de Dano**.
- **Regeneração:** Ao final do turno, o jogador recupera **50% do seu HP atual**.
- **Escalonamento de Inimigos:** A vida base de todos os inimigos aumenta em **10% por turno**.
- **Multiplicação de Inimigos:** A cada **10 turnos**, um novo inimigo é adicionado ao combate simultaneamente, até o limite máximo de **5 inimigos por turno**:
  - Turnos 1–9: 1 inimigo
  - Turnos 10–19: 2 inimigos
  - Turnos 20–29: 3 inimigos
  - Turnos 30–39: 4 inimigos
  - Turnos 40+: **5 inimigos max!**

### 👾 Elenco de Inimigos

- **Monstros Comuns (Turnos 1-4, 6-9...):** 
  - 💀 **Esqueleto:** Dano moderado e vida base padronizada.
  - 👺 **Goblin:** Ataques rápidos e alta variabilidade.
- **Chefe (A cada 5 turnos):**
  - 👑 **Goblin Herói:** Surge com vida e dano significativamente elevados pelo escalonamento!

---

## 🛠️ Tecnologias Utilizadas

* **Linguagem:** C#
* **Plataforma:** .NET 8.0
* **Ambiente:** Console Application

---

## 🚀 Como Executar o Projeto

### Pré-requisitos
* [.NET SDK](https://dotnet.microsoft.com/download) instalado.

### Passo a Passo

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/Leonardo-Leonhardt/Micro-RPG.git

   
