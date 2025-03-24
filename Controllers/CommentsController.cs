using HttpClientApi.Models.Entity;
using HttpClientApi.Models.DTOs;
using HttpClientApi.Models.DTOs.PatchDto;
using HttpClientApi.Services.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentsController : ControllerBase
	{
		private readonly ICommentService _commentService;
		public CommentsController(ICommentService commentService)
		{
			_commentService = commentService;
		}

		[HttpGet]
		[ProducesResponseType(200)]
		public async Task<ActionResult<List<Comment>>> BuscarTodosComentarios()
		{
			try
			{
				List<Comment> resposta = await _commentService.BuscarTodosComentarios();
				return Ok(resposta);
			}
			catch(HttpRequestException ex)
			{
				if (ex is { StatusCode: HttpStatusCode.NotFound })
					return NotFound("Nenhum comentário foi encontrado");
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("{id}")]
		[ProducesResponseType(200)]
		public async Task<ActionResult<CommentDto>> BuscarComentarioPorId(int id)
		{
			try
			{
				CommentDto resposta = await _commentService.BuscarComentarioPorId(id);
				return Ok(resposta);
			}
			catch (HttpRequestException ex)
			{
				if (ex is { StatusCode: HttpStatusCode.NotFound })
					return NotFound($"Comentário com id {id} não foi encontrado");
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("{postId}")]
		[ProducesResponseType(201)]
		public async Task<ActionResult<Comment>> FazerUmComentario(int postId, CommentDto comentario)
		{
			try
			{
				Comment resposta = await _commentService.FazerUmComentario(postId, comentario);
				return CreatedAtAction(nameof(BuscarComentarioPorId), new { id = resposta.Id },
					resposta + "Comentário realizado com sucesso");

			}
			catch(HttpRequestException ex)
			{
				if (ex is { StatusCode: HttpStatusCode.NotFound })
					return NotFound($"A publicação com id {postId} não foi encontrado.");
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch("{id}")]
		[ProducesResponseType(200)]
		public async Task<ActionResult<CommentDto>> AtualizarUmComentario(int id, CommentPatchDto comentario)
		{
			try
			{
				CommentDto resposta = await _commentService.AtualizarUmComentario(id, comentario);
				return Ok(resposta);
			}
			catch(HttpRequestException ex)
			{
				if (ex is { StatusCode: HttpStatusCode.NotFound })
					return NotFound($"O comentário com id {id} não foi encontrado");
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		public async Task<ActionResult> RemoverUmComentario(int id)
		{
			try
			{
				await _commentService.RemoverUmComentario(id);
				return NoContent();
			}
			catch (HttpRequestException ex)
			{
				if (ex is { StatusCode: HttpStatusCode.NotFound })
					return NotFound($"O comentário com id {id} não foi encontrado.");
				return BadRequest(ex.Message);
			}
		}

	}
}
