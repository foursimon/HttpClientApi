using HttpClientApi.Models.DTOs;
using HttpClientApi.Models.Entity;
using HttpClientApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using HttpClientApi.Models.DTOs.PatchDto;

namespace HttpClientApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostsController : ControllerBase
	{
		private readonly IPostService _postService;
		public PostsController(IPostService postService)
		{
			_postService = postService;
		}
		[HttpGet]
		[ProducesResponseType(200)]
		public async Task<ActionResult<List<Post>>> BuscarTodosPosts()
		{
			try
			{
				var resposta = await _postService.BuscarTodosPosts();
				return Ok(resposta);
			}
			catch (HttpRequestException ex)
			{
				//Verificando se o código da exceção HTTP é 404 para retornar NotFound, que retorna o código 404.
				if (ex is { StatusCode: HttpStatusCode.NotFound }) 
					return NotFound("Não foi encontrado nenhuma publicação");
				//Caso seja retornado alguma exceção não esperada, retorno um BadRequest com a mensagem da própria
				//exceção
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("{id}")]
		[ProducesResponseType(200)]
		public async Task<ActionResult<Post>> BuscarPostPorId(int id)
		{
			try
			{
				var resposta = await _postService.BuscarPostPorId(id);
				return Ok(resposta);
			}
			catch (HttpRequestException ex)
			{
				if (ex is { StatusCode: HttpStatusCode.NotFound }) 
					return NotFound($"Não foi encontrado uma publicação com o id: {id}");
				return BadRequest(ex.Message);
			}
		}
		[HttpPost]
		[ProducesResponseType(201)]
		public async Task<ActionResult<Post>> FazerUmPost(PostDto post)
		{
			try
			{
				Post resposta = await _postService.FazerUmPost(post);
				//Utilizo o id recebido para gerar a url apontando para o post criado.
				//CreatedAtAction retorna o código 201, informando que um novo dado foi criado.
				return CreatedAtAction(nameof(BuscarPostPorId), new {id = resposta.Id},resposta + "\nCriado com sucesso");
			}
			catch (HttpRequestException ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		public async Task<ActionResult<PostDto>> AtualizarTodoPost(int id, PostDto dados)
		{
			try
			{
				PostDto resposta = await _postService.AtualizarTodoPost(id, dados);
				return Ok(resposta);
			}
			catch(HttpRequestException ex)
			{
				if(ex is { StatusCode: HttpStatusCode.NotFound })
					return NotFound($"Não foi encontrado uma publicação com id: {id}");
				return BadRequest(ex);
			}
		}
		[HttpPatch("{id}")]
		[ProducesResponseType(200)]
		public async Task<ActionResult<PostDto>> AtualizarPostParcialmente(int id, PostPatchDto dados)
		{
			try
			{
				PostDto resposta = await _postService.AtualizarPostParcialmente(id, dados);
				return Ok(resposta);
			}
			catch(HttpRequestException ex)
			{
				if (ex is { StatusCode: HttpStatusCode.NotFound }) return NotFound(ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		public async Task<ActionResult> RemoverPost(int id)
		{
			try
			{ 
				await _postService.RemoverPost(id);
				//A api não retorna um corpo, por isso eu retorno NoContent.
				//NoContent informa o código 404, significando que a operação foi um sucesso
				//e não há conteúdo retornado.
				return NoContent();
			}
			catch(HttpRequestException ex)
			{
				if (ex is { StatusCode: HttpStatusCode.NotFound}) 
					return NotFound($"A publicação com o id {id} não existe.");
				return BadRequest(ex.Message);
			}
		}

	}
}
