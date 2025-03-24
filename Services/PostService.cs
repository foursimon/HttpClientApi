using HttpClientApi.Models.DTOs;
using HttpClientApi.Models.Entity;
using HttpClientApi.Services.Interfaces;
using AutoMapper;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using HttpClientApi.Models.DTOs.PatchDto;
namespace HttpClientApi.Services
{
	public class PostService : IPostService
	{
		private readonly HttpClient _httpClient;
		private readonly IMapper _mapper;
		public PostService(HttpClient httpClient, IMapper mapper)
		{
			_httpClient = httpClient;
			_mapper = mapper;
		}
		//Utilize Post ao invés do DTO apenas para ver o retorno completo
		public async Task<List<Post>> BuscarTodosPosts()
		{
			//Utilizo HttpResponseMessage para receber o retorno e o código HTTP.
			using HttpResponseMessage resposta = await _httpClient.GetAsync("/posts");
			//Valida o código Http, jogando uma exceção HTTP dependendo do código HTTP recebido.
			//Por exemplo: Não encontrado: 404
			resposta.EnsureSuccessStatusCode();
			//Transformo o conteúdo em Json em instâncias da classe Post.
			List<Post> posts = await resposta.Content.ReadFromJsonAsync<List<Post>>() ?? [];
			return posts;
		}

		public async Task<PostDto> BuscarPostPorId(int id)
		{
			using HttpResponseMessage resposta = await _httpClient.GetAsync($"/posts/{id}");
			resposta.EnsureSuccessStatusCode();
			PostDto post = await resposta.Content.ReadFromJsonAsync<PostDto>();
			return post;
		}

		public async Task<Post> FazerUmPost(PostDto post)
		{
			//Transformo o objeto em json para enviar na api.
			var postJson = JsonConvert.SerializeObject(post);
			//transforma em um conteúdo HTTP com o json, o tipo de caracteres (UTF8)
			//e o tipo do conteúdo no cabeçalho (header).
			var conteudo = new StringContent(postJson, Encoding.UTF8, "application/json");
			//Realizando a requisição HTTP Post com o conteudo http.
			using HttpResponseMessage resposta = await _httpClient.PostAsync("/posts", conteudo);
			resposta.EnsureSuccessStatusCode();
			Post novoPost = await resposta.Content.ReadFromJsonAsync<Post>();
			return novoPost;
		}

		public async Task<PostDto> AtualizarTodoPost(int id, PostDto postAtualizado)
		{
			//Verificando se o post a ser atualizado existe.
			await BuscarPostPorId(id);
			//a Api espera um objeto post completo, por isso criei uma instância da classe post.
			Post post = new Post() { Id = id };
			//Usando AutoMapper, mapeei os dados recebidos do objeto PostDto para Post
			_mapper.Map(postAtualizado, post);
			var postJson = JsonConvert.SerializeObject(post);
			var conteudo = new StringContent(postJson, Encoding.UTF8, "application/json");
			using HttpResponseMessage resposta = await _httpClient.PutAsync($"posts/{id}", conteudo);
			resposta.EnsureSuccessStatusCode();
			PostDto resultado = await resposta.Content.ReadFromJsonAsync<PostDto>();
			return resultado;
		}
		public async Task<PostDto> AtualizarPostParcialmente(int id, PostPatchDto dados)
		{
			await BuscarPostPorId(id);
			var postJson = JsonConvert.SerializeObject(dados);
			var conteudo = new StringContent(postJson, Encoding.UTF8, "application/json");
			using HttpResponseMessage resposta = await _httpClient.PatchAsync($"posts/{id}", conteudo);
			resposta.EnsureSuccessStatusCode();
			PostDto resultado = await resposta.Content.ReadFromJsonAsync<PostDto>();
			return resultado;
		}

		public async Task RemoverPost(int id)
		{
			await BuscarPostPorId(id);
			using HttpResponseMessage resposta = await _httpClient.DeleteAsync($"posts/{id}");
			resposta.EnsureSuccessStatusCode();
			return;
		}
	}
}
