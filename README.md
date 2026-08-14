# ⚔️ Mini RPG de Terminal

Um jogo de combate por turnos desenvolvido em **C#** via Console App, focado em sobrevivência, progressão contínua, mecânicas de sorte crítica (RNG) e gerenciamento de hordas com múltiplos inimigos simultâneos.

---

## 📑 Sobre o Projeto

O objetivo do jogador é sobreviver ao maior número de turnos possível. O jogo conta com um sistema de evolução por sorte (RNG), progressão baseada em ciclos de 5 turnos e combates estratégicos contra hordas que ficam progressivamente mais resistentes e populosas. O projeto adota os princípios de Orientação a Objetos (POO), herança e polimorfismo.

---

## 🛡️ Classe Inicial: Guerreiro

O Guerreiro é a classe inicial jogável que herda de `Personagem`:

- **Vida Máxima Inicial:** 40 HP.
- **Dano Base:** 5 (calcula um valor dinâmico entre `Dano - 3` e `Dano` a cada ataque).
- **Recuperação Base:** Restaura 50% da vida máxima por turno vencido.
- **Atributos Especiais:**
  - **Chance de Crítico (`_chanCritico`):** 10% de chance de dobrar o dano final desferido.
  - **Chance de Evasão (`_chanEvasao`):** 5% de chance de esquiva completa de um golpe recebido.

---

## ⚖️ Mecânicas de Jogo & Escalabilidade

- **Evolução por Turno (Jogador):** A cada turno comum vencido, o jogador aprimora **HP Máximo** ou **Dano**. O valor base é **+2**, com chances de sorte crítica (*RNG*):
  - **Ganho Padrão:** +2 (Base)
  - **Sorte Rara (5% de chance - `0.05`):** +3
  - **Sorte Lendária (0.01% de chance - `0.0001`):** +4

- **Regeneração por Turno:** 
  - **Padrão:** Restaura **50% da Vida Máxima**.
  - **Cura Milagrosa (0.01% de chance - `0.0001`):** Restaura **100% da Vida Máxima**!

- **Recompensa de Chefe (Ao derrotar o GoblinBoss a cada 5 turnos):**
  - ❤️‍🩹 **Cura Total Garantida:** Restaura **100% do HP Máximo** instantaneamente.
  - 🎁 **Drop Duplo:** O jogador recebe **ambos os aprimoramentos** (+HP Máximo E +Dano) no mesmo turno.

- **Identidade e Escalonamento dos Inimigos (Ciclos de 5 Turnos):**
  - 👺 **Goblin (Tank):** Foco em vida e esquiva (20% de evasão). Possui vida base 15 e ganha bônus progressivo por ciclo e turno.
  - 💀 **Esqueleto (Ataque/DPS):** Foco em dano balanceado. Possui vida base 25, dano base 6 e menor evasão (10%).
  - 👑 **GoblinBoss (Chefe):** Surge no turno múltiplo de 5, com atributos massivos de HP e dano elevado.

- **Multiplicação de Inimigos (Horda):** A cada **10 turnos**, surge +1 inimigo no combate (limite máximo de 5 monstros simultâneos):
  - **Turnos 1–9:** 1 inimigo
  - **Turnos 10–19:** 2 inimigos
  - **Turnos 20–29:** 3 inimigos
  - **Turnos 30–39:** 4 inimigos
  - **Turnos 40+:** 5 inimigos

---

## 👾 Elenco de Inimigos	

- **Monstros Comuns:** 💀 **Esqueleto** e 👺 **Goblin**.
- **Chefe (A cada 5 turnos):** 👑 **Goblin Herói / GoblinBoss** (surge com status elevados e concede drop duplo).

---

## 📐 Estrutura de Classes (UML)

![Diagrama de Classes V1](docs/diagramas/diagrama-classes-v1.png)

### `Entidade` (Classe Abstrata Base)
Classe fundamental que centraliza os atributos e regras de combate compartilhados por todas as criaturas do jogo.
- **Atributos Protegidos (`protected`):**
  - `_nome : string`
  - `_vida : int`
  - `_dano : int`
  - `_chanCritico : double`
  - `_chanEvasao : double`
- **Métodos:**
  - `Atacar() : int` — Calcula o dano base e aplica multiplicador de dano crítico caso ativado.
  - `Critico() : bool` — Valida se o ataque atual resultou em acerto crítico com base em `_chanCritico`.
  - `Evasao() : bool` — Checa se a entidade conseguiu se esquivar do golpe com base em `_chanEvasao`.
  - `ReceberDano(dano : int) : bool` — Processa o dano recebido após testar a evasão (retorna `true` se foi atingido ou `false` se desviou).
  - `ToString() : string` — Retorna os dados formatados da entidade via `StringBuilder`.

---

### 🛡️ Hierarquia do Jogador

#### `Personagem : Entidade` (Classe Abstrata de Heróis)
Especialização de `Entidade` voltada exclusivamente para o jogador, contendo mecânicas de sustentação e evolução contínua.
- **Atributos Protegidos (`protected`):**
  - `_vidaMax : int`
  - `_recuperaVida : double`
- **Métodos:**
  - `AumentarVidaMax(upVida : int) : void` — Incrementa a vida máxima e a vida atual.
  - `AumentarDano(upDano : int) : void` — Incrementa o dano base da entidade.
  - `AumentarVida(curaTotal : bool) : void` — Regenera a vida atual com base em `_recuperaVida` ou 100% se for cura total.

#### `Guerreiro : Personagem` (Classe Filha)
- **Construtor:** `Guerreiro(nome : string = "Guerreiro")` — Inicializa os atributos base (40 HP, 5 Dano, 10% Crítico, 5% Evasão e 50% de Recuperação).
- **Métodos:**
  - `Atacar() : int` — Sobrescreve o método sorteando um valor entre `_dano - 3` e `_dano` antes de aplicar crítico.

---

### 👾 Hierarquia dos Monstros

#### `Mob : Entidade` (Classe Abstrata dos Inimigos)
Especialização de `Entidade` para criaturas hostis, centralizando os cálculos matemáticos de escalonamento.
- **Construtor:** `Mob(nome : string, vida : int, dano : int, chanCritico : double, chanEvasao : double)`
- **Métodos Protegidos Estáticos:**
  - `GeraVida(turnoAtual : int, vidaBase : int, bonusVida : int) : int` — Calcula a vida escalonada por ciclo de 5 turnos.
  - `GeraDano(turnoAtual : int, danoBase : int, bonusDano : int) : int` — Calcula o dano progressivo com proteção contra divisão por zero.

#### `Goblin : Mob` (Classe Filha)
- **Construtor:** `Goblin(turnoAtual : int)` — Instancia um monstro focado em evasão (20%), com vida e dano escalonados por ciclo.

#### `Esqueleto : Mob` (Classe Filha)
- **Construtor:** `Esqueleto(turnoAtual : int)` — Instancia um monstro equilibrado e ofensivo (25 HP Base, 6 Dano Base).

#### `GoblinBoss : Mob` (Classe Filha - BOSS)
- **Construtor:** `GoblinBoss(turnoAtual : int)` — Instancia o chefe da horda com vida massiva e dano aumentado.

---

### 🎮 Controle de Jogo

#### `GerenciadorJogo` (Controlador da Partida)
Responsável por gerenciar o ciclo de vida do jogo, agregação das entidades e execução dos turnos.
- **Atributos Privados (`private`):**
  - `_jogador : Guerreiro`
  - `_hordaInimigos : List<Mob>`
  - `_turnoAtual : int`
- **Métodos:**
  - `IniciarPartida() : void` — Inicializa o herói, zera o contador de turnos e roda o loop principal.
  - `ExecutarTurno() : void` — Executa o ciclo de evolução do herói, instanciação da horda, rodada de combates e cura final.
  - `CriarMobs() : List<Mob>` — Instancia e retorna a lista de inimigos do turno, calculando a quantidade (até o teto de 5) e instanciando chefes nos turnos múltiplos de 5.
  - `VerificarGameOver() : bool` — Retorna `true` caso o HP do jogador seja $\le 0$.
  - `VerificarTurno() : int` — Retorna o número do turno atual.

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
   git clone [https://github.com/Leonardo-Leonhardt/Micro-RPG.git](https://github.com/Leonardo-Leonhardt/Micro-RPG.git)