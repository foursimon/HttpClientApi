namespace HttpClientApi.Models.Entity
{
	public class Post
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string? Title { get; set; }
		public string? Body { get; set; }

		public override string ToString()
		{
			return $"id do Post: {Id}\nid do usuário: {UserId}\ntítulo: {Title}\nbody: {Body}";
		}
	}
}
