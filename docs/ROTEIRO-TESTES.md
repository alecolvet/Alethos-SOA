# Roteiro de testes manuais — Insomnia

Base URL: `http://localhost:5080`

## Cenários positivos

1. **Cadastrar usuário** — `POST /api/usuarios`
   ```json
   {"nome":"Maria de Teste","email":"maria.teste@example.com"}
   ```
   Esperado: `201 Created`, objeto com `id`.
2. **Cadastrar contato** — `POST /api/contatos`
   ```json
   {"usuarioId":1,"nome":"João de Teste","telefone":"11999990000","parentesco":"Filho"}
   ```
   Esperado: `201 Created`.
3. **Registrar ocorrência** — `POST /api/ocorrencias`
   ```json
   {"usuarioId":1,"mensagemRecebida":"Troquei de número. Faça um PIX urgente.","canal":"WhatsApp","classificacao":"Suspeito","nivelRisco":90,"justificativa":"Pede transferência urgente e informa troca de número."}
   ```
   Esperado: `201 Created`.
4. **Criar solicitação** — `POST /api/solicitacoes-ajuda`
   ```json
   {"usuarioId":1,"contatoConfiancaId":1,"motivo":"Quero confirmar a identidade de um familiar antes de transferir dinheiro."}
   ```
   Esperado: `201 Created`, status `Pendente`.
5. **Concluir solicitação** — `PATCH /api/solicitacoes-ajuda/1/status`
   ```json
   {"status":"Concluida"}
   ```
   Esperado: `200 OK`, status `Concluida` e `finalizadaEm` preenchido.

## Cenários negativos

1. E-mail inválido ou nome vazio em `POST /api/usuarios` — esperado `400 Bad Request` (validação automática de `[ApiController]`).
2. Repetir o mesmo e-mail — esperado `409 Conflict`.
3. Cadastrar contato com `usuarioId` inexistente — esperado `404 Not Found`.
4. Criar ocorrência para usuário inexistente — esperado `400 Bad Request` com explicação.
5. Criar ocorrência com classificação diferente das três permitidas — esperado `400 Bad Request`.
6. Criar solicitação com contato que pertence a outro usuário — esperado `400 Bad Request`.
7. Consultar ocorrência/solicitação inexistente — esperado `404 Not Found`.
8. Tentar alterar uma solicitação concluída — esperado `400 Bad Request`.

Registre prints dos casos positivos e de pelo menos quatro casos negativos para comprovar os testes manuais.
