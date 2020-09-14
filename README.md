# Back End varejo
Repositório contendo uma API .NET Core e um Modelo de Banco de Dados MySQL, entre outros, para executar funcionalidades back-end de controle de operações genéricas de varejo.

COMO PREPARAR A APLICAÇÃO:

1) ***Banco de dados em MySQL (mydatabase)***
Para configurar o banco de dados da aplicação é necessário que na máquina onde se irá rodar a API REST já tenha sido criada uma database MySQL com o nome "mydatabase". Então, use o MySqlWorkbench para iniciar o servidor MySQL no port 3306. A API REST tentará se conectar a essa database com usuário "root", senha "root". Caso na máquina em questão o usuário root não tenha essa senha é necessário abrir o Visual Studio, abrir com ele a solution da REST API e procurar em todos os locais da solution o texto "server=localhost;port=3306;user=root;password=root;database=mydatabase". Quando forem exibidos, execute um replace all na solution mudando o valor do parâmetro "password" para que fique igual à senha do usuário root do banco de dados mydatabase na sua máquina. Após criar a database no MySQLWorkbench, use um terminal cmd para navegar até o diretório da REST API e executar os seguintes comando na ordem:
dotnet tool install dotnet-ef --global
dotnet ef migrations add MyBaseMigration --context SqlContext
dotnet ef database update
Assim, a aplicação em .NET core criará um modelo para a base de dados totalmente embasado no domínio (entidades) da REST API, usando o Entity Framework.

2) ***REST API em .Net Core (RESTAPI)***
Para rodar esta API, abra a Solution da API com o Visual Studio e execute a API em modo debug com o IIS Express. A aplicação estará rodando no servidor local, port 44336. Ao acessar o endereço "https://localhost:44336/index.html" você poderá realizar testes de integração na API, mas certifique-se de ter configurado o banco de dados MySQL com as informações acima para fazê-los. Caso deseje gerar o build para produção/release, clique com o botão direito no projeto Presentation - que é o projeto de inicialização, e no menu exibido clique em "Publish", selecione a opção de publicar em um diretório, e escolha o diretório desejado para publicar na configuração release.
OBSERVAÇÃO: Adicionei suporte ao Docker no diretório da Solution, usando uma Dockerfile. Se a máquina de testes possuir o Docker instalado pode-se executar a API REST em um container do docker. Para isso, navegue com o terminal cmd até o diretório da solution e execute os seguintes comandos:
"docker build -t aspnetapp ."
"docker run -d -p 8080:80 --name netcoreapi aspnetapp"
Assim, o container com a REST API estará rodando no servidor local, na porta 8080.