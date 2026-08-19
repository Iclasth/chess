# ♟️ Chess Game (Xadrez em C#)

Um jogo de xadrez completo executado no console, desenvolvido inteiramente em **C#** e **.NET**. 

Este projeto foi construído como o trabalho final do curso completo de C# do Nélio Alves, com o objetivo de consolidar os fundamentos da programação orientada a objetos (POO), modelagem de domínio, manipulação de matrizes e tratamento de exceções.

## 🚀 Funcionalidades

O jogo respeita todas as regras tradicionais da FIDE (Federação Internacional de Xadrez), incluindo:
- Movimentação baseada em coordenadas tradicionais do xadrez (ex: `c2` para `c4`).
- Validação estrita de movimentos (peças não podem pular outras, exceto o Cavalo).
- Prevenção de auto-xeque (jogadas que colocariam o próprio rei em perigo são bloqueadas).
- Identificação de **Xeque** e **Xeque-Mate**.
- **Jogadas Especiais implementadas:**
  - Roque (Maior e Menor)
  - *En Passant*
  - Promoção de Peão

## 🛠️ Tecnologias e Conceitos Aplicados

- **Linguagem:** C# (.NET)
- **Paradigma:** Programação Orientada a Objetos (Encapsulamento, Herança, Polimorfismo e Abstração).
- **Estruturas de Dados:** Uso intensivo de Matrizes (Arrays bidimensionais) e Listas.
- **Tratamento de Exceções:** Criação de exceções personalizadas (`ChessBoardException`) para regras de negócio e validações do tabuleiro.
- **Clean Code:** Separação clara de responsabilidades (Camada de Tabuleiro, Camada de Jogo e Interface de Console).

## 💻 Como Executar

1. Certifique-se de ter o [.NET SDK](https://dotnet.microsoft.com/download) instalado na sua máquina.
2. Clone este repositório:
   ```bash
   git clone [https://github.com/SEU-USUARIO/NOME-DO-REPOSITORIO.git](https://github.com/SEU-USUARIO/NOME-DO-REPOSITORIO.git)
3. Navegue até a pasta do projeto:
  ```Bash
    cd NOME-DO-REPOSITORIO/DiretorioDoProjeto
  ```

4. Execute o programa através do terminal:
  ```Bash
    dotnet run
  ```
  
## 👨‍💻 Sobre o Desenvolvedor
Desenvolvido por Iclei Arthur.
Como um desenvolvedor back-end focado no ecossistema .NET, este projeto reflete minha busca por escrever códigos limpos, arquiteturas bem definidas e dominar os fundamentos da linguagem C#.

Este projeto faz parte do portfólio de estudos contínuos em desenvolvimento de software.
