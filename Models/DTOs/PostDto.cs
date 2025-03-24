using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;

namespace HttpClientApi.Models.DTOs
{
	public class PostDto
	{
		[Required(ErrorMessage = "Insira o id do usuário")]
		public required int UserId { get; set; }

		[Required(ErrorMessage = "Insira o título da publicação")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[StringLength(50, MinimumLength = 6, 
			ErrorMessage ="O título deve conter pelo menos 6 caracteres e só pode ter 50 caracteres no máximo")]
		public required string Title { get; set; }

		[Required(ErrorMessage = "Insira o conteúdo da publicação")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[StringLength(1500, MinimumLength=20,
			ErrorMessage="O conteúdo publicação deve ter pelo menos 20 caracteres e não pode exceder 1500 caracteres")]
		public required string Body { get; set; }

		public override string ToString()
		{
			return $"Id do usuário: {UserId}\nTítulo: {Title}\nBody: {Body}";
		}
	}
}
