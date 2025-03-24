
using System.ComponentModel.DataAnnotations;

namespace HttpClientApi.Models.DTOs.PatchDto
{
	public class PostPatchDto
	{
		[StringLength(50, ErrorMessage = "O título da publicação não pode exceder 50 caracteres")]
		public string? Body { get; set; }
		[StringLength(1500, ErrorMessage = "O corpo da publicação não pode exceder 1500 caracteres")]
		public string? Title { get; set; }
	}
}
