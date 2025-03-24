Um cliente tem necessidade de buscar livros em um catálogo. Esse cliente quer ler e buscar esse catálogo de um arquivo JSON, e esse arquivo não pode ser modificado. Então com essa informação, é preciso desenvolver:

    Criar uma API para buscar produtos no arquivo JSON disponibilizado.
    Que seja possível buscar livros por suas especificações(autor, nome do livro ou outro atributo)
    É preciso que o resultado possa ser ordenado pelo preço.(asc e desc)
    Disponibilizar um método que calcule o valor do frete em 20% o valor do livro.

Será avaliado no desafio:

    Organização de código;
    Manutenibilidade;
    Princípios de orientação à objetos;
    Padrões de projeto;
    Teste unitário

Para nos enviar o código, crie um fork desse repositório e quando finalizar, mande um pull-request para nós.

O projeto deve ser desenvolvido em C#, utilizando o .NET Core 3.1 ou superior.

Gostaríamos que fosse evitado a utilização de frameworks, e que tivesse uma explicação do que é necessário para funcionar o projeto e os testes.
# IAGRO API

Este projeto é uma API para o sistema IAGRO.

## Pré-requisitos

- Docker
- Docker Compose

## Instruções para executar o projeto

1. Certifique-se de que o Docker e o Docker Compose estão instalados em sua máquina.

2. Abra um terminal e navegue até a pasta raiz do projeto (onde está localizado o arquivo `docker-compose.yml`).

3. Execute o seguinte comando para construir e iniciar o container: docker-compose up --build


Este comando irá construir a imagem Docker (se necessário) e iniciar o container.

4. A API estará disponível em `http://localhost:5193`.

5. Para acessar a documentação Swagger, abra um navegador e acesse: http://localhost:5193/swagger

6. Para parar a execução, pressione `Ctrl+C` no terminal onde o docker-compose está rodando.

7. Se quiser parar e remover os containers, execute: docker-compose down

## Notas adicionais

- O arquivo `books.json` deve estar localizado na pasta `IAGRO/IAGRO.Api/DBO/` para que o volume do Docker funcione corretamente.
- Qualquer alteração no código fonte requer uma reconstrução da imagem Docker. Use o comando `docker-compose up --build` para reconstruir e reiniciar o container.