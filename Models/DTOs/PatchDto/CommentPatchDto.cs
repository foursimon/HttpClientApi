using System.ComponentModel.DataAnnotations;

namespace HttpClientApi.Models.DTOs.PatchDto
{
	public class CommentPatchDto
	{
		[StringLength(2000, MinimumLength = 10,
			ErrorMessage = "O comentário deve ter pelo menos 10 caracteres e não pode exceder 2000 caracteres")]
		public string? Body { get; set; }
	}
}
