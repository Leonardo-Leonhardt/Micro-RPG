# ⚔️ Mini RPG de Terminal

Um jogo de combate por turnos simples desenvolvido em **C#** via Console App, focado em sobrevivência, progressão contínua e gerenciamento de múltiplos inimigos simultâneos.

---

## 📑 Sobre o Projeto

O objetivo do jogador é sobreviver ao maior número de turnos possível. O jogo conta com um sistema de escolha de classes e evolução por sorte (RNG), enfrentando hordas que ficam progressivamente mais populosas e resistentes. O projeto adota uma arquitetura Orientada a Objetos (POO).

---

## 🛡️ Classe Inicial: Guerreiro

O Guerreiro é a classe inicial jogável que herda todas as características de `Personagem`:

- **Vida Máxima Inicial:** 15.
- **Dano Base:** 2 a 5 (calculado a cada ataque).
- **Atributos Especiais:**
  - **Chance de Crítico (`chanCritico`):** 10% de chance de dobrar o dano.
  - **Chance de Evasão (`chanEvasao`):** 5% de chance de se esquivar totalmente de um ataque recebido.
- **Bônus de Sorte:** Ao evoluir vida, há **0.5% de chance** de ganhar **+2 de HP Máximo** em vez de +1.
---

  ## ⚖️ Mecânicas de Jogo & Escalabilidade

- **Evolução por Turno:** O jogador escolhe a cada turno aumentar **+1 HP Máximo** ou **+1 Dano**.
- **Regeneração:** Método `RecuperaVida()` restaura **50% do HP atual** após vencer o turno.
- **Escalonamento de Inimigos:** Inimigos ganham **+10% de HP por turno**.
- **Multiplicação de Inimigos (Horda):** A cada **10 turnos**, surge +1 inimigo no combate (máximo 5 simultâneos):
  - Turnos 1–9: 1 inimigo
  - Turnos 10–19: 2 inimigos
  - Turnos 20–29: 3 inimigos
  - Turnos 30–39: 4 inimigos
  - Turnos 40+: 5 inimigos
---

## 👾 Elenco de Inimigos	

- **Monstros Comuns:** 💀 **Esqueleto** e 👺 **Goblin**.
- **Chefe (A cada 5 turnos):** 👑 **Goblin Herói** (surge no primeiro slot da horda com atributos elevados).

  ---

  ## 📐 Estrutura de Classes (UML)

### `Personagem` (Classe Base)
- **Atributos Protegidos (`protected`):**
  - `vidaMax : int`
  - `vida : int`
  - `dano : int`
  - `chanCritico : float`
  - `chanEvasao : float`
- **Métodos:**
  - `Atacar() : int` — Calcula e retorna o dano causado.
  - `Critico() : bool` — Valida se o ataque atual é crítico.
  - `ReceberDano(dano : int) : bool` — Aplica dano considerando a chance de evasão (retorna `true` se desviou).
  - `AumentarVidaMax() : void` — Incrementa a vida máxima (com checagem de sorte).
  - `RecuperaVida() : void` — Regenera 50% da vida atual do personagem.

### `Guerreiro : Personagem` (Classe Filha)
- Instancia o herói definindo os atributos base iniciais via construtor `Guerreiro(nome : string)`.

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
