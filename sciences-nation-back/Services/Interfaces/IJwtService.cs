namespace sciences_nation_back.Services.Interfaces
{
	public interface IJwtService
	{
		string GenerateToken(string userId, string email);
	}
}