# Cyber Dominion: Guerra das IAs

Protótipo de um jogo 2D de estratégia, construção de cidade e combate entre robôs, desenvolvido em C# com Unity. O projeto possui identidade própria e usa formas geométricas geradas em tempo de execução, portanto não depende de artes ou pacotes pagos.

## Funcionalidades

- Cidade com grade de construção 10 × 7.
- Quartel-general, usina, mina, centro de treinamento e laboratório.
- Créditos, energia e metais.
- Produção passiva e coleta manual de recursos.
- Herói Nexus e robôs Guardiões.
- Inimigos com busca de alvo, movimentação e ataque automáticos.
- Três missões com dificuldade progressiva.
- Recompensas por vitória.
- Salvamento automático em JSON.
- Nova partida, continuar, pausa, retirada e retorno ao menu.
- Interface criada inteiramente por código.
- Cena principal gerada automaticamente ao abrir o projeto.

## Requisitos

- Unity Hub.
- Unity `2022.3.62f1` LTS ou uma versão mais recente da linha 2022.3 LTS.
- Módulo de compilação para a plataforma desejada, caso queira gerar um executável.

## Como abrir e jogar

1. Clone ou baixe este repositório.
2. No Unity Hub, escolha **Add project from disk**.
3. Selecione a pasta `Cyber-Dominion`.
4. Aguarde a importação e compilação dos scripts.
5. A cena `Assets/Scenes/CyberDominion.unity` será criada automaticamente.
6. Abra essa cena e pressione **Play**.

Se a cena não aparecer, use o menu **Cyber Dominion > Recriar cena principal**.

## Controles

| Ação | Controle |
| --- | --- |
| Mover câmera | `WASD` ou setas |
| Zoom | Roda do mouse |
| Posicionar construção | Clique esquerdo na grade |
| Pausar/continuar | `Esc` |
| Combate | Automático |

## Como jogar

1. Inicie uma nova partida.
2. Escolha um edifício no menu inferior.
3. Clique em uma célula livre da cidade.
4. Construa usinas e minas para ampliar a produção.
5. Construa centros de treinamento para levar mais Guardiões às missões.
6. Clique em **Iniciar missão**.
7. Sobreviva às forças corrompidas e avance pelas três missões.

## Estrutura principal

```text
Assets/
├── Editor/                  # Geração automática da cena
├── Scenes/                  # Cena criada pelo Unity
└── Scripts/
    ├── Buildings/           # Catálogo, grade e construção
    ├── Combat/              # Missões e combate
    ├── Core/                # Inicialização, câmera, áudio e estado
    ├── Resources/           # Economia do jogo
    ├── Save/                # Persistência JSON
    ├── UI/                  # Menus e HUD
    └── Units/               # Dados e comportamento das unidades
Documentation/              # Design e plano de testes
Packages/                   # Dependências Unity
ProjectSettings/            # Versão do Editor
```

## Arquitetura

O `GameBootstrap` instala os componentes principais em um objeto persistente da cena. O `GameManager` coordena os estados Menu, Cidade, Batalha e Pausa. Os demais sistemas são independentes e comunicam mudanças por referências controladas e eventos simples.

O arquivo salvo fica em `Application.persistentDataPath/cyber_dominion_save.json`.

## Build

### Método simples para Windows

1. Abra o projeto no Unity e aguarde o fim da compilação.
2. Abra a cena `Assets/Scenes/CyberDominion.unity`.
3. No menu superior, clique em **Cyber Dominion > Gerar jogo para Windows**.
4. Aguarde a mensagem **Build concluída**.
5. Abra a pasta `Builds/Windows`.
6. Dê dois cliques em `CyberDominion.exe` para jogar.

O arquivo `.exe` precisa permanecer ao lado da pasta `CyberDominion_Data`.

### Método tradicional

1. Abra **File > Build Settings**.
2. Confirme que `CyberDominion` está na lista de cenas.
3. Escolha **Windows, Mac, Linux** e selecione Windows 64-bit.
4. Clique em **Build** e escolha a pasta `Builds/Windows`.

## Instalação no computador gamer

### 1. Programas necessários

- Instale o [Git para Windows](https://git-scm.com/download/win).
- Instale o [Unity Hub](https://unity.com/download).
- Pelo Unity Hub, instale o Unity `2022.3.62f1` LTS com o módulo **Windows Build Support (IL2CPP)**.

### 2. Baixar pelo terminal

Abra o **PowerShell** e execute:

```powershell
cd "$env:USERPROFILE\Desktop"
git clone https://github.com/Abnerrum/Cyber-Dominion.git
cd Cyber-Dominion
```

Para abrir o projeto pelo terminal, ajuste a versão do caminho caso tenha instalado outra versão 2022.3 LTS:

```powershell
& "C:\Program Files\Unity\Hub\Editor\2022.3.62f1\Editor\Unity.exe" -projectPath (Get-Location)
```

Também é possível abrir o Unity Hub, clicar em **Add > Add project from disk** e selecionar a pasta `Cyber-Dominion` da Área de Trabalho.

### 3. Gerar e instalar o jogo

1. No Unity, clique em **Cyber Dominion > Gerar jogo para Windows**.
2. Entre na pasta `Builds\Windows`.
3. Clique com o botão direito em `CyberDominion.exe`.
4. Escolha **Mostrar mais opções > Enviar para > Área de trabalho (criar atalho)**.
5. Renomeie o atalho para **Cyber Dominion**.

Depois disso, basta clicar duas vezes no atalho para jogar. O Unity não precisa estar aberto para executar a versão gerada.

### Atualizar o jogo no futuro

Abra o PowerShell dentro da pasta do projeto e execute:

```powershell
git pull origin main
```

Depois, abra o Unity e gere novamente o jogo pelo menu **Cyber Dominion > Gerar jogo para Windows**.

## Roadmap

- Melhorias e níveis de edifícios.
- Árvore tecnológica funcional.
- Produção de unidades com tempo.
- Campanha territorial.
- Chefes, equipamentos e habilidades.
- Animações, trilha e sprites autorais.
- Progresso em nuvem e modo multiplayer em uma etapa futura.

## Licença

Distribuído sob a licença MIT. Consulte [LICENSE](LICENSE).

## Autor

**Abner Luiz Pascoal de Oliveira** — [GitHub @Abnerrum](https://github.com/Abnerrum)
