namespace TourOfHeroes.Contracts.Heroes
{
    public sealed record HeroResponse(
        string Id, 
        string Name, 
        List<SuperPowerResponse>? SuperPowers);

    public sealed record SuperPowerResponse(
        string Id, 
        string Name, 
        string Description);
}
