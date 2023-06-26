# ProductManagement

*SOLUÇÕES, PROJETOS E TECNOLOGIAS

- A solução foi divida em 3 projetos, cada um com uma função especifica: ProductManagement.Entity(Apenas paras classes), ProductManagement.DAL (Conexões com BD e arquivos Util, para criar algumas logicas) e ProductManagement(FrontEnd).

- ProductManagement.DAL e ProductManagement.Entity são bibliotecas de classes, com .net 0.6

- ProductManagement Feito em ASP.NET CORE MVC.

- Usado o BD SQL SERVER, Linq to Entity, Entity Framework, alem de ASP.NET CORE MVC



*DEPENDENCIAS DE CADA PROJETO:
- ProductManagement.DAL 

1 EntityFramework
2 Microsoft.EntityFrameworkCore
3 Microsoft.EntityFrameworkCore.Relational
4 Microsoft.EntityFrameworkCore.SqlServer
5 Microsoft.Extensions.Configuration
6 Microsoft.Extensions.Configuration.FileExtensions
7 Microsoft.Extensions.Configuration.Json
8 Newtonsoft.Json

- ProductManagement.Entity 
N/A

-ProductManagement
1 Microsoft.EntityFrameworkCore
2 Newtonsoft.Json



* ENTIDADES

- Foi criado 3 classes de Entidades: Order: Pedido, Product: Produto, OrderItems: itens no pedido, todos com colunas com nomes e tipos que foi pedido

- Alem de uma classe base (BaseEntity), seguindo padrões, que salva dados para informações de cadastro, como data de criação.

- Todas as classes Entidades estão no projeto: ProductManagement.Entity.



* FUNCIONALIDADE DO CODIGO
 - ProductManagement.DAL:
	DataContext: Captura as connectionString e Cria a conexão com o SQLSERVER.

	Repositories: Usa a conexão de DataContext, cada repository chama todas as procedures e organiza os parametros para executa-las, tem todas as funcões de CRUD 	para cada Entity.

	TransactionTRA: Feito para criar Funções de utilidade do projeto, para poder inseri, Update e romove, entre outros metodos uteis para o funcionamento.

 - ProductManagement.Entity:
	Contem apenas as classes de Entities que serão usadas.

 - ProductManagement (Front)
	Organizado em MVC, cada Entity tem seu Controller e as views são separadas por pastas, para cada um.

	STYLES: Não foi muito focado em styles, apenas o basico para funcionar. Usado BOOTSTRAP, boxicons e reutilizado um CSS proprio, por isso terá class com nomes diferentes.

	Controllers: HomeController: Controla as paginas principais, que seria criação de um novo pedidos e todos os produtos relacionados a eles, as demais controlam 	as paginas de produtos, lista e atualização de pedidos.


*****  PARA RODAR O CODIGO *****
1 - Executar os 2 arquivos de script, para criar todas as procedures e tables.

2 - Sugiro criar um nova base de dados chamada: "ProductControl", Pois todos as procedures seguem um padrão de busca de tabela, exemplo: 
SELECT [ProductControl].[dbo].[Order]
Ou apenas alteras o nome da base de dados ("[ProductControl]..") em todos os scripts

3 - No projeto ProductManagement Alterar as connectionStrings em appsettings.json 

  "ConnectionStrings": {
    "DataConnection": "Data Source=;Initial Catalog=;Integrated Security=true;TrustServerCertificate=True;"
  }

"TrustServerCertificate=True": Essa configuração indica que o certificado do servidor de banco de dados é confiável e pode ser aceito sem validação adicional...


4 - Defina ProductManagement como Projeto de Inicialização.

5 - Pronto para rodar perfeitamente, sem ERROS.

6 - Depois de iniciar o projeto, Em HOMEPAGE: Novo Pedido (Principal), Lista de Pedidos (Tela de Pedidos), Lista de Produtos(Tela de Produtos).








