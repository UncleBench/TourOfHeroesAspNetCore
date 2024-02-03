namespace TourOfHeroes.Contracts.Authentication
{
    public sealed record AuthenticationResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Token);
}
