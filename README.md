<h1> <img src="./Assets/api.png" width="50px"> API HttpClient</h1>
<p>Este projeto foi construíada para entender melhor sobre a classe HttpClient.</p>
<p>Utilizando a classe HttpClient, é realizado o consumo de uma api dedicada ao apredizado, a qual se chama
jsonplaceholder</p>
<h2><img src="./Assets/goal.png" width="30px">Objetivo</h2>
<p>O principal objetivo deste projeto é familiarizar com consumo de api em asp.net utilizando HttpClient</p>
<h2><img src="./Assets/generator.png" width="30px">O que foi utilizado</h2>
<ul>
	<li>AutoMapper: para mapear dados entre classes;</li>
	<li>Newtonsoft.Json: para realizar serializar e deserializar objetos json;</li>
	<li>Scalar.AspNetCore: nova documentação de api utilizada para substituir Swagger;</li>
</ul>
<h2><img src="./Assets/question.png" width="30px" />Como funciona</h2>
<p>A minha API estabelece um conexão com outra API que se conecta a uma base de dados.</p>
<p>Para realizar requisições, é necessário enviar uma chave API para verificar se está autorizado ou não.</p>
<p>A classe "ApiKeyAuthentication" é responsável por validar a chave enviada no cabeçalho da requisição.</p>
<p>Essa classe foi herda a interface "IAuthorizationFilter" para validar a chave recebida do cabeçalho com a 
chave armazenada nesta API, e também herda da classe "Attribute" para ser utilizada como um atributo nos 
controladores da API.</p>
<p>Implementei uma política de CORS para permitir requisições de um ponto de origem diferente.</p>
<p>Átravés dessa política, defino quais métodos de requisição HTTP e quais valores
de cabeçalho são permitidos dessa origem.</p>

<h2><img src="./Assets/api.png" width="30px">Documentação Api</h2>
<h3>Requisição não autorizada</h3>
<img src="./Assets/fracasso.png" width="500px">
<br/>
<h3>Requisição realizada com sucesso</h3>
<img src="./Assets/sucesso.png" width="500px">
<br/>
<h3>Política de Cors</h3>
<img src="./Assets/politicaCors.png" width="500px">
<br/>
<h3>Validação de chave</h3>
<img src="./Assets/chave.png" width="500px">