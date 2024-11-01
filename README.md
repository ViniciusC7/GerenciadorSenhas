# Gerenciador de Senhas

Este é um gerenciador de senhas simples em C# que permite gerar, verificar e sugerir senhas seguras. O aplicativo utiliza algoritmos de hash para armazenar senhas de forma segura e oferece funcionalidades para ajudar os usuários a manter suas senhas seguras.

## Funcionalidades

- **Gerar Hash da Senha**: Cria um hash SHA-256 de uma senha fornecida e armazena em um arquivo.
- **Verificar Senha**: Verifica se uma senha fornecida corresponde a um hash armazenado.
- **Sugerir Senha**: Gera uma senha aleatória forte, com opções de incluir caracteres especiais.
- **Validação de Senha**: As senhas devem ter pelo menos 8 caracteres, incluir uma letra maiúscula e um dígito.
- **Armazenamento em Arquivo**: Os hashes gerados são salvos em um arquivo `hashes.txt`.

## Tecnologias Usadas

- C#
- .NET Framework
- Algoritmo de hash SHA-256


## Uso

Após executar o aplicativo, você verá um menu com as seguintes opções:

1. **Gerar Hash da Senha**: Digite uma senha para gerar seu hash e salvá-lo em um arquivo.
2. **Verificar Senha**: Insira uma senha e um hash para verificar a correspondência.
3. **Sugerir Senha**: Digite o comprimento da senha desejada e se deseja incluir caracteres especiais para gerar uma senha aleatória.
4. **Sair**: Encerra o aplicativo.
