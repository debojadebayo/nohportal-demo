namespace ComposedHealthBase.Server.Entities
{
	public interface IApplicationUser
    {
        string FirstName { get; set; }
		string LastName { get; set; }
        string? UserName { get; set; }
		string Telephone { get; set; }
		string Email { get; set; }
        string? AvatarImage { get; set; }
		string? AvatarTitle { get; set; }
		string? AvatarDescription { get; set; }
        Guid KeycloakId { get; set; }
    }
}