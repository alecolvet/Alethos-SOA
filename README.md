# Alethos — SOA e Web Services


## Grupo
Alexandre Colvet  - RM560059
Enzo Luciano      - RM559557
Luigi Thiengo     - RM560755
Pedro Claudino    - RM561023
Witalon Rodrigues - RM559023


## Link do repositorio:
https://github.com/alecolvet/ProtegePlus-SOA

Backend REST da CitizenTech Antigolpe para Idosos. Esta entrega cobre somente a disciplina **Arquitetura Orientada a Serviços (SOA) e Web Services**: estruturação do domínio, persistência, migrations e testes manuais via Insomnia.

## Escopo

A API registra cidadãos atendidos, contatos de confiança, ocorrências/mensagens suspeitas e solicitações de ajuda. A classificação recebida em `POST /api/ocorrencias` é um dado informado pelo serviço/etapa de IA da equipe; esta API **não treina nem executa um modelo de IA**.

## Tecnologias

- C# / ASP.NET Core Web API (.NET 10)
- Entity Framework Core 10
- SQLite (arquivo local `protegeplus.db`)
- Swagger / OpenAPI
- Insomnia

> Escolha de persistência: o material C# fornecido contém um arquivo de banco SQLite (`antigolpe.db`). Para evitar alterar acidentalmente o banco da aplicação desktop e sem conhecer seu schema completo, esta API usa um arquivo SQLite próprio (`protegeplus.db`). A integração com o Windows Forms deve ocorrer por HTTP/JSON, consumindo esta API; não por acesso direto ao arquivo do banco.

## Pré-requisitos

1. Windows 10/11.
2. .NET 10 SDK instalado (`dotnet --version` deve retornar `10.x`).
3. Insomnia (para executar a collection em `insomnia/`).

Não é necessário instalar SQL Server ou PostgreSQL. O SQLite é criado localmente após aplicar a migration.

## Como executar no Visual Studio

1. Extraia a pasta `ProtegePlus-SOA`.
2. Abra `ProtegePlus.sln` no Visual Studio 2026 (ou versão compatível com .NET 10).
3. Aguarde a restauração dos pacotes NuGet.
4. No menu **Tools > NuGet Package Manager > Package Manager Console**, execute:

   ```powershell
   dotnet tool install --global dotnet-ef --version 10.0.0
   dotnet ef database update --project src/ProtegePlus.Api
   ```

5. Defina `ProtegePlus.Api` como projeto de inicialização e execute com HTTPS.
6. Abra a URL `/swagger` exibida no terminal/navegador.

## Como executar pelo terminal

Abra o PowerShell na pasta raiz do projeto:

```powershell
dotnet --version
dotnet tool install --global dotnet-ef --version 10.0.0
dotnet restore ProtegePlus.sln
dotnet ef database update --project src/ProtegePlus.Api
dotnet run --project src/ProtegePlus.Api --urls http://localhost:5080
```

Acesse:

- API: http://localhost:5080
- Swagger: http://localhost:5080/swagger

O arquivo `protegeplus.db` será criado na pasta de execução quando a migration for aplicada. Para recriar o banco durante os testes, pare a API, apague o arquivo e execute novamente `dotnet ef database update`.

## Migrations

A migration inicial está em `src/ProtegePlus.Api/Migrations`.

```powershell
# Aplicar migrations pendentes
dotnet ef database update --project src/ProtegePlus.Api

# Criar uma nova migration após alterar as entidades ou o DbContext
dotnet ef migrations add NomeDaAlteracao --project src/ProtegePlus.Api

# Atualizar o banco
dotnet ef database update --project src/ProtegePlus.Api

# Listar migrations
dotnet ef migrations list --project src/ProtegePlus.Api
```

Não use `EnsureCreated()`: o projeto usa migrations do EF Core para versionar o schema.

## Modelo de domínio

- **Usuario 1:N ContatoConfianca** — uma pessoa pode ter vários familiares/contatos de confiança.
- **Usuario 1:N Ocorrencia** — cada ocorrência pertence a um usuário.
- **Usuario 1:N SolicitacaoAjuda** — um usuário pode solicitar ajuda várias vezes.
- **ContatoConfianca 1:N SolicitacaoAjuda (opcional)** — uma solicitação pode ser encaminhada a um contato cadastrado.

Regras implementadas: e-mail único; telefone único por usuário; ocorrência exige usuário existente, classificação permitida e risco entre 0 e 100; contato de ajuda precisa pertencer ao usuário; status de solicitação é controlado e itens finalizados não podem ser reabertos.

## Endpoints

| Método | Rota | Finalidade |
|---|---|---|
| POST | `/api/usuarios` | Cadastrar usuário |
| GET | `/api/usuarios` | Listar usuários |
| GET | `/api/usuarios/{id}` | Consultar usuário |
| POST | `/api/contatos` | Cadastrar contato de confiança |
| GET | `/api/contatos/usuario/{usuarioId}` | Listar contatos de um usuário |
| POST | `/api/ocorrencias` | Registrar mensagem e resultado de análise |
| GET | `/api/ocorrencias/{id}` | Consultar ocorrência |
| GET | `/api/ocorrencias/usuario/{usuarioId}` | Histórico de ocorrências |
| POST | `/api/solicitacoes-ajuda` | Abrir pedido de confirmação/ajuda |
| GET | `/api/solicitacoes-ajuda` | Listar solicitações (filtros `usuarioId` e `status`) |
| GET | `/api/solicitacoes-ajuda/{id}` | Consultar solicitação |
| PATCH | `/api/solicitacoes-ajuda/{id}/status` | Atualizar status |

Classificações aceitas: `Suspeito`, `AparentementeSeguro`, `Inconclusivo`.

Status aceitos: `Pendente`, `EmAtendimento`, `Concluida`, `Cancelada`.

## Testes manuais no Insomnia

Importe `insomnia/ProtegePlus-Insomnia.json`. A collection usa `http://localhost:5080`.

Ordem sugerida:
1. `01 - Criar usuário` (guarde o `id` retornado).
2. `02 - Criar contato` (substitua `usuarioId` pelo ID real).
3. `03 - Criar ocorrência` (substitua `usuarioId`).
4. `04 - Criar solicitação` (substitua os IDs retornados).
5. `05 - Listar solicitações`.
6. `06 - Concluir solicitação` (substitua `id` da URL).
7. Execute também os testes negativos listados em `docs/ROTEIRO-TESTES.md`.

Para evidenciar os 1,5 ponto de testes manuais, capture no Insomnia a requisição, o status HTTP e o corpo de resposta dos cenários positivos e negativos.

## Segurança e dados de demonstração

Use somente nomes, telefones, e-mails e mensagens fictícios nos testes. Esta versão é um protótipo acadêmico: antes de uso real, deve receber autenticação/autorização, proteção de dados pessoais, política de retenção, auditoria e comunicação segura entre os serviços.

## Integrantes

Preencher com nome completo e RM de cada integrante da equipe antes da entrega.
