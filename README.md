<h1>📂 LojaOnlineApi</h1>
<p>Uma API RESTful para gerenciamento de produtos e categorias de uma loja virtual. Desenvolvida em <b>.NET 9</b> com autenticação JWT e documentação via Swagger.</p>
<hr>

<h2>🚀 Funcionalidades</h2>
<ul>
  <li>✅ CRUD de <b>categorias</b> (<code>/api/categoria</code>)</li>
  <li>✅ CRUD de <b>produtos</b> (<code>/api/produto</code>)</li>
  <li>🔒 Controle de permissões por tipo de usuário
    <ul>
      <li>🖓 Endpoints de <b>listagem são públicos</b></li>
      <li>🔐 Endpoints de <b>criação, edição e remoção são restritos a administradores</b></li>
    </ul>
  </li>
</ul>
<hr>

<h2>🛠️ Tecnologias Utilizadas</h2>
<ul>
  <li><a href="https://dotnet.microsoft.com/">.NET 9</a></li>
  <li>PostgreSQL</li>
  <li>Entity Framework Core</li>
  <li>JWT (JSON Web Tokens)</li>
  <li>Swagger (Swashbuckle)</li>
</ul>
<hr>

<h2>📂 Estrutura de Diretórios</h2>
<pre>
📁 Controllers      # Endpoints da API
📁 Services         # Lógica de autenticação e negócio
📁 Data             # Configuração do DbContext e migrations
📁 DTOs             # Objetos de transferência de dados
📄 Program.cs       # Inicialização da aplicação
📄 appsettings.json # Configurações gerais
</pre>
<hr>

<h2>🔐 Autenticação</h2>
<p>Esta API utiliza autenticação via <b>JWT</b>.<br>
Ao fazer login com credenciais válidas, será gerado um token que deve ser incluído no header <code>Authorization</code> das requisições protegidas:</p>
<pre>
Authorization: Bearer &lt;seu-token&gt;
</pre>
<hr>

<h2>🔗 Endpoint Público de Exemplo</h2>
<p>Listar todas as categorias cadastradas:</p>
<pre>
GET http://localhost:5063/api/categoria
</pre>
<hr>

<h2>🧪 Como Rodar Localmente</h2>
<ol>
  <li>✅ <b>Pré-requisitos</b>:
    <ul>
      <li><a href="https://dotnet.microsoft.com/en-us/download">.NET 9 SDK</a></li>
      <li>PostgreSQL rodando localmente</li>
    </ul>
  </li>
  <li>⚙️ <b>Configurações obrigatórias</b>:
    <ul>
      <li>Configure o arquivo <code>launchSettings.json</code> com as variáveis de ambiente:</li>
    </ul>
    <pre>
"environmentVariables": {
  "ASPNETCORE_ENVIRONMENT": "Development",
  "DbPassword": "suaSenhaPostgres",
  "jwtKey": "suaChaveJWTsecreta"
}
    </pre>
    <ul>
      <li>Ajuste o <code>appsettings.json</code> para apontar corretamente para o banco (sem expor senhas)</li>
    </ul>
  </li>
  <li>🏃‍♂️ <b>Executar o projeto</b>:
    <pre>
dotnet run
    </pre>
  </li>
  <li>📄 <b>Acessar a documentação Swagger</b>:
    <pre>
http://localhost:5063/swagger
    </pre>
  </li>
</ol>
<hr>

<h2>📌 Notas Importantes</h2>
<ul>
  <li><b>Nunca</b> suba sua chave JWT ou senha do banco em repositórios públicos.</li>
  <li>O arquivo <code>appsettings.json</code> <b>não deve conter informações sensíveis</b> e pode ser ignorado com <code>.gitignore</code>.</li>
  <li>A autenticação é baseada em roles. Por padrão:
    <ul>
      <li>Usuários comuns: apenas leitura</li>
      <li>Administradores: CRUD completo</li>
    </ul>
  </li>
</ul>
<hr>

<h2>📃 Licença</h2>
<p>Este projeto é open-source e livre para uso pessoal ou educacional.<br>
Sinta-se à vontade para contribuir com melhorias!</p>
<hr>

<h2>🧑‍💻 Desenvolvido por</h2>
<div style="display: flex; align-items: center; gap: 1rem;">
    <a href="https://github.com/DavidSouzaxz">
        <img src="https://avatars.githubusercontent.com/u/187334857?v=4" alt="David Souza" width="100" style="border-radius: 50%; border: 2px solid #007fff"/>
    </a>
    <p>David Souza</p>
</div>
