using HttpClientApi.Models.DTOs;
using HttpClientApi.Models.DTOs.PatchDto;
using HttpClientApi.Models.Entity;
using HttpClientApi.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace HttpClientApi.Services
{
	public class CommentService : ICommentService
	{
		private readonly HttpClient _httpClient;
		public CommentService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<List<Comment>> BuscarTodosComentarios()
		{
			using HttpResponseMessage resposta = await _httpClient.GetAsync("/comments");
			resposta.EnsureSuccessStatusCode();
			List<Comment> comentarios = await resposta.Content.ReadFromJsonAsync<List<Comment>>() ?? [];
			return comentarios;

		}

		public async Task<CommentDto> BuscarComentarioPorId(int id)
		{
			using HttpResponseMessage resposta = await _httpClient.GetAsync($"/comments/{id}");
			resposta.EnsureSuccessStatusCode();
			CommentDto comentario = await resposta.Content.ReadFromJsonAsync<CommentDto>();
			return comentario;
		}

		public async Task<Comment> FazerUmComentario(int postId, CommentDto comentario)
		{
			await BuscarPostPorId(postId);
			var comentarioJson = JsonConvert.SerializeObject(comentario);
			var conteudo = new StringContent(comentarioJson, Encoding.UTF8, "application/json");
			using HttpResponseMessage resposta = await _httpClient.PostAsync($"/comments", conteudo);
			resposta.EnsureSuccessStatusCode();
			Comment resultado = await resposta.Content.ReadFromJsonAsync<Comment>();
			return resultado;
		}

		public async Task<CommentDto> AtualizarUmComentario(int id, CommentPatchDto novoComentario)
		{
			await BuscarComentarioPorId(id);
			var json = JsonConvert.SerializeObject(novoComentario);
			var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
			using HttpResponseMessage resposta = await _httpClient.PatchAsync($"/comments/{id}", conteudo);
			resposta.EnsureSuccessStatusCode();
			CommentDto resultado = await resposta.Content.ReadFromJsonAsync<CommentDto>();
			return resultado;
		}

		public async Task RemoverUmComentario(int id)
		{
			await BuscarComentarioPorId(id);
			using HttpResponseMessage resposta = await _httpClient.DeleteAsync($"/comments/{id}");
			resposta.EnsureSuccessStatusCode();
			return;
		}

		private async Task<PostDto> BuscarPostPorId(int id)
		{
			using HttpResponseMessage resposta = await _httpClient.GetAsync($"/posts/{id}");
			resposta.EnsureSuccessStatusCode();
			PostDto post = await resposta.Content.ReadFromJsonAsync<PostDto>();
			return post;
		}
	}
}
