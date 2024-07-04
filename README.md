# StreamApiChallenge

## Descrição

Esta API foi escrita em C# em .NET 6.0 com banco de dados SQL Server e verificação dos endpoints com Swagger.
A plicação se trata de uma API CRUD de gestão de filmes, onde pode-se criar, alterar, listar e deletar filmes do catálogo.
A API também inclui funcionalidades para filtrar filmes por avaliações, comentários e ano de lançamento, e suporta a paginação dos resultados.



https://github.com/henriquedeantoni/stream-api-challenge/assets/147777120/a2b63d10-b185-4a6a-b28c-7499f666e4f9



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

Para começar basta clonar o repositório para a pasta do projeto:

  `git clone https://github.com/henriquedeantoni/stream-api-challenge.git`

## Endpoints da API

Com auxilio do Swagger para conferencia dos Endpoints

### 1. **Listar Filmes com Paginação**

- **Método:** `GET`
- **Endpoint:** `/api/movies?pageNumber={pageNumber}&pageSize={pageSize}`
- **Descrição:** Retorna uma lista de filmes com suporte para paginação.

  **Parâmetros:**
  - `pageNumber` (int): Número da página (começando de 1).
  - `pageSize` (int): Número de filmes por página.

### 2. **Obter Filme por ID**

- **Método:** `GET`
- **Endpoint:** `/api/movies/{id}`
- **Descrição:** Retorna informações detalhadas de um filme específico.

  **Parâmetros:**
  - `id` (int): ID do filme.

### 3. **Adicionar Novo Filme**

- **Método:** `POST`
- **Endpoint:** `/api/movies`
- **Descrição:** Cria um novo filme.

  **Corpo da Requisição:**

  ```json
  {
    "title": "Nome do Filme",
    "gender": "Action",
    "releaseDate": "yyyy-MM-ddTHH:mm:ssZ",
    "streamings": [
      {
        "streamingName": "Netflix"
      }
    ],
    "ratings": [
      {
        "comments": "Ótimo filme!",
        "rating": 5
      }
    ]
  }

### 4. **Atualizar filme**

- **Método:** `PUT`
- **Endpoint:** `/api/movies/{id}`
- **Descrição:** Atualiza as informações de um filme existente.

  **Corpo da Requisição:**

  ```json
  {
  "id": 1,
  "title": "Nome Atualizado do Filme",
  "gender": "Action",
  "releaseDate": "2024-07-03T19:46:46.9900856Z",
  "streamings": [
    {
      "id": 1,
      "streamingName": "Amazon Prime"
    }
  ],
  "ratings": [
    {
      "id": 1,
      "comments": "Atualização do comentário.",
      "rating": 4
    }
  ]
}

### 5. **Excluir Filme**

- **Método:** `DELETE`
- **Endpoint:** `/api/movies/{id}`
- **Descrição:** Remove um filme do banco de dados.
  
  
### 6. ** Filtrar Filmes por Avaliação Média**

- **Método:** `GET`
- **Endpoint:** `/api/movies/averageRating?minAverageRating={minAverageRating}`
- **Descrição:** Retorna filmes com média de avaliação maior ou igual a um valor específico.
- **Parametros:** minAverageRating (double): Valor mínimo da média de avaliação.
  

### 7. ** Filtrar Filmes por Comentário**

- **Método:** `GET`
- **Endpoint:** `/api/movies/comment?commentText={commentText}`
- **Descrição:** Retorna filmes que contêm um texto específico em algum dos comentários das avaliações.
- **Parametros:** commentText (string): Texto do comentário a ser pesquisado.

  
### 8. ** Filtrar Filmes por Ano de Lançamento**

- **Método:** `GET`
- **Endpoint:** `/api/movies/year/{year}`
- **Descrição:** Retorna filmes lançados em um ano específico.
- **Parametros:** year (int): Ano de lançamento do filme.
