namespace HttpClientApi.Models.Entity
{
	public class Comment
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? Body { get; set; }

		public override string ToString()
		{
			string res = $"Id do comentário: {Id}";
			res += $"\nId da publicação: {PostId}";
			res += $"\nNome do usuário: {Name}";
			res += $"\nEmail do usuário: {Email}";
			res += $"\nConteúdo: {Body}";
			return res;
		}
	}
}
