# StreamApiChallenge

## Descrição

Esta API foi escrita em C# em .NET 6.0 com banco de dados SQL Server e verificação dos endpoints com Swagger.
A plicação se trata de uma API CRUD de gestão de filmes, onde pode-se criar, alterar, listar e deletar filmes do catálogo.
A API também inclui funcionalidades para filtrar filmes por avaliações, comentários e ano de lançamento, e suporta a paginação dos resultados.

## Funcionalidades

- **Gerenciamento de Filmes**:
  - **Listar Filmes**: Obter uma lista completa de todos os filmes, tambem pode entrar com a paginação.
  - **Obter Filme por ID**: Buscar informações de um filme através da entrada de seu Id
  - **Adicionar Novo Filme**: Criar um novo filme com avaliação e as plataformas associadas.
  - **Atualizar Filme**: Atualizar dados de um filme existente.
  - **Excluir Filme**: Remover um filme do banco de dados.

- **Filtragem de Filmes**:
  - **Filtrar por Avaliação Média**: Obter filmes cuja média das avaliações seja maior ou igual a um valor específico.
  - **Filtrar por Comentário**: Buscar filmes que contenham um termo específico em suas avaliações.
  - **Filtrar por Ano de Lançamento**: Listar filmes lançados em um ano específico.
 
## Iniciando

Para começar basta clonar o repositório:

  `git clone https://github.com/henriquedeantoni/stream-api-challenge.git`
