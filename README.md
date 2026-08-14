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
---

## ⚖️ Mecânicas de Jogo & Escalabilidade

- **Evolução por Turno (Jogador):** A cada turno comum vencido, o jogador escolhe aprimorar **HP Máximo** ou **Dano**. O ganho base é **+2**, com chances de sorte crítica (*RNG*):
  - **Ganho Padrão:** +2 (Base)
  - **Sorte Rara (5% de chance - `0.05`):** +3
  - **Sorte Lendária (0.01% de chance - `0.0001`):** +4

- **Regeneração por Turno:** 
  - **Padrão:** Restaura **50% do HP atual**.
  - **Cura Milagrosa (0.01% de chance - `0.0001`):** Restaura **100% da Vida Máxima**!

- **Recompensa de Chefe (Ao derrotar o GoblinBoss a cada 5 turnos):**
  - ❤️‍🩹 **Cura Total Garantida:** Restaura **100% do HP Máximo** instantaneamente.
  - 🎁 **Drop Duplo:** O jogador recebe **ambos os aprimoramentos** (+HP Máximo E +Dano) no mesmo turno, rodando as chances de sorte crítica para cada um.

- **Identidade e Escalonamento dos Inimigos (Mobs):**
  - 👺 **Goblin (Tank):** Foco em resistência. A cada turno ganha **+10 de Vida Máxima**.
  - 💀 **Esqueleto (Ataque/DPS):** Foco em dano. A cada turno ganha **+5 de Vida Máxima** e **+1 de Dano**.
  - 👑 **GoblinBoss (Chefe):** Combina alta vida, dano elevado e 20% de chance de acerto crítico.

- **Multiplicação de Inimigos (Horda):** A cada **10 turnos**, surge +1 inimigo no combate (limite máximo de 5 monstros simultâneos):
  - **Turnos 1–9:** 1 inimigo
  - **Turnos 10–19:** 2 inimigos
  - **Turnos 20–29:** 3 inimigos
  - **Turnos 30–39:** 4 inimigos
  - **Turnos 40+:** 5 inimigos
 
---

## 👾 Elenco de Inimigos	

- **Monstros Comuns:** 💀 **Esqueleto** e 👺 **Goblin**.
- **Chefe (A cada 5 turnos):** 👑 **Goblin Herói** (surge no primeiro slot da horda com atributos elevados).

  ---

  ## 📐 Estrutura de Classes (UML)

### `Entidade` (Classe Abstrata Base)
Classe fundamental que centraliza os atributos e regras de combate compartilhados por todas as criaturas do jogo.
- **Atributos Protegidos (`protected`):**
  - `_vida : int`
  - `_dano : int`
  - `_chanCritico : float`
  - `_chanEvasao : float`
- **Métodos:**
  - `Atacar() : int` — Calcula o dano base e aplica multiplicador de dano crítico caso ativado.
  - `Critico() : bool` — Valida se o ataque atual resultou em acerto crítico com base em `_chanCritico`.
  - `Evasao() : bool` — Checa se a entidade conseguiu se esquivar do golpe com base em `_chanEvasao`.
  - `ReceberDano(dano : int) : bool` — Processa o dano recebido após testar a evasão (retorna `true` se desviou ou `false` se foi atingida).

---

### 🛡️ Hierarquia do Jogador

#### `Personagem : Entidade` (Classe Base de Heróis)
Especialização de `Entidade` voltada exclusivamente para entidades controladas pelo jogador, contendo mecânicas de sustentação e evolução contínua.
- **Atributos Protegidos (`protected`):**
  - `_vidaMax : int`
  - `_recuperaVida : float` (taxa percentual de cura por turno, padrão: `0.50f`)
- **Métodos:**
  - `AumentarVidaMax(upVida : int) : void` — Incrementa a vida máxima com chance de bônus de sorte extra (0.10%).
  - `RecuperaVida() : void` — Regenera a vida atual com base no multiplicador `_recuperaVida`.

#### `Guerreiro : Personagem` (Classe Filha)
- **Construtor:** `Guerreiro(nome : string)` — Define os atributos base do herói inicial (vida máxima, variação de dano de 2 a 5, 10% de crítico e 5% de evasão).

---

### 👾 Hierarquia dos Monstros

#### `Mob : Entidade` (Classe Base dos Inimigos)
Especialização de `Entidade` para criaturas hostis geradas pelo jogo.
- **Construtor:** `Mob(nome : string, vida : int, dano : int, chanCritico : float, chanEvasao : float)` — Repassa os valores ajustados pelo turno para a base `Entidade`.

#### `Goblin : Mob` (Classe Filha)
- **Construtor:** `Goblin(multiplicadorVida : double)` — Instancia um monstro focado em velocidade, com HP moderado, dano leve e maior chance de evasão (`15%`).

#### `Esqueleto : Mob` (Classe Filha)
- **Construtor:** `Esqueleto(multiplicadorVida : double)` — Instancia um monstro equilibrado, com HP intermediário e dano consistente.

#### `GoblinBoss : Mob` (Classe Filha - BOSS)
- **Construtor:** `GoblinBoss(multiplicadorVida : double)` — Instancia o chefe da horda (aparece a cada 5 turnos), com multiplicador expressivo de vida, dano elevado e 20% de chance de acerto crítico.

---

### 🎮 Controle de Jogo

#### `GerenciadorJogo` (Controlador da Partida)
Responsável por gerenciar o ciclo de vida do jogo, agregação das entidades e execução dos turnos.
- **Atributos Privados (`private`):**
  - `_jogador : Guerreiro` (ou `Personagem`)
  - `_hordaInimigos : List<Mob>`
  - `_turnoAtual : int`
  - `_multiplicadorVida : double`
- **Métodos:**
  - `IniciarPartida() : void` — Inicializa o herói, zera o contador de turnos e roda o loop principal.
  - `ExecutarTurno() : void` — Executa o ciclo de evolução do herói, instanciação da horda, rodada de combates e cura final.
  - `CriarMobs() : List<Mob>` — Instancia e retorna a lista de inimigos do turno, calculando a quantidade (até o teto de 5) e escalonando o HP (+10% por turno).
  - `VerificarGameOver() : bool` — Retorna `true` caso o HP do jogador seja $\le 0$.
  - `VerificarTurno() : int` — Retorna o número do turno atual.

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
