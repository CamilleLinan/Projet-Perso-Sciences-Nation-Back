namespace sciences_nation_back.Models.Dto
{
	public class UserDto
	{
		public required string Id { get; set; }
		public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public List<string> Favorites { get; set; } = new List<string>();
    }
}