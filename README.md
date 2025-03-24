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
