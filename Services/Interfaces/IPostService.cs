using HttpClientApi.Models.DTOs;
using HttpClientApi.Models.DTOs.PatchDto;
using HttpClientApi.Models.Entity;

namespace HttpClientApi.Services.Interfaces
{
	public interface IPostService
	{
		public Task<List<Post>> BuscarTodosPosts();
		public Task<PostDto> BuscarPostPorId(int id);
		public Task<Post> FazerUmPost(PostDto post);
		public Task<PostDto> AtualizarTodoPost(int id, PostDto postAtualizado);
		public Task<PostDto> AtualizarPostParcialmente(int id, PostPatchDto post);
		public Task RemoverPost(int id);
	}
}
