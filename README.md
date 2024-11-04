# SmartCash-CSharp

## Integrantes

- **Camila Soares Pedra** - RM98246
- **Gustavo Bertti** - RM552243
- **Gustavo Macedo da Silva** - RM552333
- **Rafael Camargo** - RM551127

## Definição da Arquitetura

A arquitetura escolhida para a API é **monolítica**. Como o projeto envolve um sistema de gestão relativamente simples e focado em funcionalidades centrais, a abordagem monolítica oferece uma implementação mais ágil e direta.

Todos os recursos, como fluxo de caixa, empresas e usuários, estão interconectados, e a arquitetura monolítica permite gerenciar essas dependências de forma centralizada. Uma estrutura monolítica é mais fácil de manter em estágios iniciais do desenvolvimento, antes que o projeto escale. Além disso, proporciona uma transição mais tranquila para futuras refatorações, como a migração para microservices, caso necessário.

Essa arquitetura atende bem às necessidades atuais do projeto e permite que, no futuro, o sistema possa evoluir para microservices à medida que as funcionalidades e a complexidade aumentarem.

## Instruções para usar a API

Clonar o repositório e rodar o projeto.

## Testes Implementados

Foram implementados testes unitários, de integração e de sistema abrangentes utilizando o xUnit. Os testes visam garantir a funcionalidade correta da API e suas interações com o banco de dados, além de validar o fluxo de dados e as regras de negócio.

## Práticas de Clean Code Aplicadas

- **Nomes Claros**: Foram utilizados nomes descritivos para classes, métodos e variáveis, facilitando a compreensão do código.
- **Simplicidade**: O código foi mantido o mais simples possível, evitando complexidades desnecessárias.
- **Modularidade**: A estrutura do projeto foi organizada em módulos bem definidos, permitindo fácil manutenção e expansão.
- **Reutilização**: Foram aplicadas práticas que favorecem a reutilização de código, como a criação de repositórios para interações com o banco de dados.


