using HttpClientApi.Models.Entity;
using HttpClientApi.Models.DTOs;
using HttpClientApi.Models.DTOs.PatchDto;

namespace HttpClientApi.Services.Interfaces
{
	public interface ICommentService
	{
		public Task<List<Comment>> BuscarTodosComentarios();
		public Task<CommentDto> BuscarComentarioPorId(int id);
		public Task<Comment> FazerUmComentario(int postId, CommentDto comentario);
		public Task<CommentDto> AtualizarUmComentario(int id, CommentPatchDto novoComentario);
		public Task RemoverUmComentario(int id);
	}
}
