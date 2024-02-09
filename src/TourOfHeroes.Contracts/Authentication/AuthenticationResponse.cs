namespace TourOfHeroes.Contracts.Authentication
{
    public sealed record AuthenticationResponse(
        string Id,
        string FirstName,
        string LastName,
        string Email,
        string Token);
}
