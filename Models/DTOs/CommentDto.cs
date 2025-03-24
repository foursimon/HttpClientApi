using HttpClientApi.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace HttpClientApi.Models.DTOs
{
	public class CommentDto
	{
		[Required(ErrorMessage = "O nome do usuário é obrigatório.")]
		[StringLength(100, MinimumLength = 5,
			ErrorMessage = "O nome do usuário deve ter pelo menos 5 caracteres e não pode exceder 100 caracteres")]
		public required string Name { get; set; }
		[Required(ErrorMessage = "O e-mail do usuário é obrigatório.")]
		public required string Email { get; set; }

		[Required(ErrorMessage = "O comentário não pode estar vazio")]
		[StringLength(2000, MinimumLength = 10,
			ErrorMessage = "O comentário deve ter pelo menos 10 caracteres e não pode exceder 2000 caracteres")]
		public required string Body { get; set; }
		public override string ToString()
		{
			string res = "";
			res += $"\nNome do usuário: {Name}";
			res += $"\nEmail do usuário: {Email}";
			res += $"\nConteúdo: {Body}";
			return res;
		}
	}
}
