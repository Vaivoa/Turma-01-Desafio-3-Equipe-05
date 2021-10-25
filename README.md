[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
![Contribuidores](https://badgen.net/badge/Contribuidores/7?icon=github)

<hr>

![Linkedin Badge](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Linkedin Badge](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white)
![Linkedin Badge](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=Swagger&logoColor=white)
![Linkedin Badge](https://img.shields.io/badge/Docker-2CA5E0?style=for-the-badge&logo=docker&logoColor=white)
![Linkedin Badge](https://img.shields.io/badge/Apache_Kafka-231F20?style=for-the-badge&logo=apache-kafka&logoColor=white)
![Linkedin Badge](https://img.shields.io/badge/redis-%23DD0031.svg?&style=for-the-badge&logo=redis&logoColor=white)
![Linkedin Badge](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Linkedin Badge](https://img.shields.io/badge/MongoDB-4EA94B?style=for-the-badge&logo=mongodb&logoColor=white)
![Linkedin Badge](https://img.shields.io/badge/xUnity-100000?style=for-the-badge&logo=unity&logoColor=white)
![Linkedin Badge](https://img.shields.io/badge/GitHub_Actions-2088FF?style=for-the-badge&logo=github-actions&logoColor=white)
![Linkedin Badge](https://img.shields.io/badge/Insomnia-5849be?style=for-the-badge&logo=Insomnia&logoColor=white)
![Linkedin Badge](https://img.shields.io/badge/Git-F05032?style=for-the-badge&logo=git&logoColor=white)
<hr>

  <h1 align="center">Projeto: Desafio 03 - API Modalmais</h1>
  
<div align="center">
  <img style="border-radius:50px" src="https://blogsupereconomica.com/wp-content/uploads/2018/12/modalmais-%C3%A9-boa.jpg" min-width="400px" max-width="400px" width="400px" alt="Development">
  <br>
  <img style="border-radius:50px" src="https://uploads-ssl.webflow.com/6091713b6a00f546c5f59141/60917281100ec9f4a29cf107_Logo%20Vaivoa.png" min-width="400px" max-width="500px" width="400px" alt="Development">
</div>

## Sobre o projeto

Este projeto foi desenvolvido com o intuito de utilizar e fixar os conhecimentos que foram vistos durante os cursos realizados neste período de 3 semanas, com foco em construir uma API de escopo bancário, onde é possível: realizar cadastros de contas, clientes, chave pix, validar os seus documentos, realizar transferências bancárias utilizando pix e gerar extratos financeiros.

O desafio foi idealizado pelo instrutor Eduardo Bueno, e construído inteiramente pelos integrantes do time, listados na última sessão deste documento, utilizando as ferramentas da próxima sessão em conjunto com as boas praticas de programação e estruturação aprendidas nos cursos e pesquisas.

### Desenvolvido com

- .Net Core 5
- Swagger
- Docker
- Docker Compose
- Apache Kafka
- Redis
- PostgreSQL
- MongoDb
- xUnity
- Github Actions
- Visual Studio 2019

## Como começar

Para começar com o projeto basta você possuir em sua máquina o Docker instalado e um navegador web ou ferramenta de chamadas Rest externa, como o Insomnia.

Talvez seja necessário a instalação do certificado de desenvolvimento do .NET Core, para isso utilize os seguintes comandos:
```bash
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\aspnetapp.pfx -p suasenha
dotnet dev-certs https --trust
```
[Para mais informações do certificado](https://docs.microsoft.com/en-us/dotnet/core/additional-tools/self-signed-certificates-guide)

### Pré-requisitos

Clonar o repositório:

```bash
git clone https://github.com/Vaivoa/Turma-01-Desafio-3-Equipe-05.git
```

Para executar todo o ecossistema do projeto é necessário utilizar o Docker Compose usando o seguindo comando na pasta raiz do projeto (Modalmais)::

```bash
docker-compose up
```

## Como usar

Para utilizar e testar o projeto acesse os endereços abaixo: 

```bash
http://localhost:5001         //colocar redis no primeiro campo.
http://localhost:5101        // API de Transação
http://localhost:5201       // API de Cadastro
```
Agora você será redirecionado para a página de OpenApi do Swagger onde poderá testar os endpoint, ou também pode utilizar outras ferramentas para isso.

## Imagens

<p align="center" width="100%">
    <img width="33%" src="https://i.ibb.co/4t6xjdP/estrutura-bds.png"> 
    <br>
    <em>Estrutura dos SGBD's: MongoDB e PostgreSQL. </em>
</p>
<hr>
<p align="center" width="100%">
    <img width="33%" src="https://i.ibb.co/wBDVB2B/exemplo-resposta-extrato.png"> 
    <br>
    <em>Exemplo de uma resposta fornecida pela API de cadastro, usando o endpoint para gerar um extrato e a ferramenta Rest Insomnia. </em>
</p>
<hr>
<p align="center" width="100%">
    <img width="33%" src="https://i.ibb.co/F051nns/swagger-api-cadastro.png"> 
    <br>
    <em>Apresentação da página do Swagger (OpenAPI) da API de cadastro. </em>
</p>
<hr>
<p align="center" width="100%">
    <img width="33%" src="https://i.ibb.co/TbCHSpC/swagger-api-transacoes.png"> 
    <br>
    <em>Apresentação da página do Swagger (OpenAPI) da API de transação. </em>
</p>
<hr>
<p align="center" width="100%">
    <img width="33%" src="https://i.ibb.co/HhVvxP3/visualizar-kafka.png"> 
    <br>
    <em>Visualização dos topicos e mensagens do Apache Kafka utilizando a ferramenta KaDeck. </em>
</p>
<hr>
<p align="center" width="100%">
    <img width="33%" src="https://i.ibb.co/cNkzthm/visualizar-redis.png"> 
    <br>
    <em>Apresentação da página de acesso ao Redis, utilizando a ferramenta marian/rebrow que está no Docker-Compose. </em>
</p>
<hr>
<p align="center" width="100%">
    <img width="33%" src="https://i.ibb.co/wYvxmz7/estrutura-projeto.png"> 
    <br>
    <em>Estrutura do projeto, dividido em camadas e Microsserviços. </em>
</p>
<br>
<br>

## Contribuidores

[![Linkedin Badge](	https://img.shields.io/badge/Lucas%20Paes-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/lucastmp/)
[![Linkedin Badge](	https://img.shields.io/badge/Matheus%20Pimentel-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/matheus-vinicius-pimentel/)
[![Linkedin Badge](	https://img.shields.io/badge/Victor%20Magdesian-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/victor-felippe-magdesian-7a45051a7/)
[![Linkedin Badge](	https://img.shields.io/badge/Matheus%20Paixao-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/matheuspaixao/)
[![Linkedin Badge](	https://img.shields.io/badge/Vitor%20Jansen-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/vitorjansen/)
[![Linkedin Badge](	https://img.shields.io/badge/Railton%20Rames-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/railton-rames/)
[![Linkedin Badge](	https://img.shields.io/badge/Eduardo%20Bueno-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/eduardobueno/)
