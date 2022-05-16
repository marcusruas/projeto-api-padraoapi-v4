# Scaffold de API

_Scaffold criado para definir o padrão de desenvolvimento de minhas APIS_

### Sobre o projeto

Api criada com o framework .NET Core na versão 6, utilizando pacotes criados com arquitetura netstandard2.1

### Configurações da API

Com o intuito de facilitar o fork do repositório de Scaffold, as configurações da API (middlewares básicos, serviços e implementação de pacotes etc.) foram colocadas em pacotes Nuget localizados no repositório abaixo:

*https://github.com/marcusruas/MandradeFrameworks-v3*

Até a data da implementação desta documentação (15/02/2022), os pacotes ainda não foram disponibilizados em domínio público, sendo necessário a instalação dos pacotes localmente.

## Configurações iniciais necessárias

Para começar a desenvolver neste Scaffold, é necessário somente algumas coisas:

- Alterar o nome da solution principal (ScaffoldApi.sln) para o nome da futura api;
- Alterar a variável NOME_API na classe Api.Configuration.ApiConfiguration da camada de api para o nome da api desejada, bem como o nome do documento na invocação do serviço;
- Alterar a variável TABELA_LOGS na classe Api.Configuration.ApiConfiguration da camada de api para o nome da tabela desejada.

## Um pouco sobre a arquitetura da aplicação

## Api

Camada com os endpoints da aplicação. Através do pacote [MediaTR](https://github.com/jbogard/MediatR) cada endpoint chama o handler indicado para a operação.

## UseCases

Camada com os handlers da aplicação. Ela utiliza os objetos e serviços das outras camadas para atender as solicitações do cliente.

## Domain

Camada com as regras de negócio da aplicação.

## Infrastructure

Camada responsável pela interação com aplicações externas a APIS

### Communication

Camada responsável pela comunicação da API de vendas com outras APIs, como a da SalesForce.

### Repositories

Camada com os repositórios da aplicação, queries SQL e definições de banco de dados para interação da aplicação com SQL.

## Tests

Camada com os testes da aplicação utilizando XUnit.

# Retorno da aplicação

Todos os endpoints da aplicação devem retornar seus dados encapsulados no objeto _MandradeFrameworks.Retornos.Models.RetornoApi_ conforme abaixo no formato _application/json_:

```
{
    "sucesso": bool,
    "retorno": T,
    "mensagens": [
        {
            "tipo": int,
            "valor": string
        }
    ]
}
```

Para gerar o retorno acima, os dados de retorno devem ser encapsulados no método _MandradeFrameworks.Retornos.Controllers.StandardController.RespostaPadrao<T>(T dados)_ ou _MandradeFrameworks.Retornos.Controllers.StandardController.ProcessarSolicitacao<T>(IRequest<T> solicitacao)_ caso seu endpoint execute apenas uma chamada via [MediaTR](https://github.com/jbogard/MediatR).

## Explicação do modelo de retorno

_sucesso_: define se a operação ocorreu com sucesso ou não. Essa propriedade é definida dependendo se ocorreu uma exception não tratada na aplicação durante a execução do endpoint.
_dados_: dados de retorno do endpoint. Aqui ficará todos os dados de retorno do endpoint, sejam eles de quaisquer tipos. Caso ocorra uma exception não tratada na aplicação durante a execução do endpoint, será retornado apenas uma mensagem de erro padrão nesta propriedade.
_mensagens_: aqui ficará as mensagens que foram adicionadas durante a execução do endpoint pelo serviço _MandradeFrameworks.Mensagens.IMensageria_

## Erros na aplicação

Caso ocorra algum erro na aplicação, há um middleware para tratamento de exceptions que garantirá que a aplicação retorne os dados no formato padrão da API, sem quaisquer preocupações por parte do desenvolvedor.
