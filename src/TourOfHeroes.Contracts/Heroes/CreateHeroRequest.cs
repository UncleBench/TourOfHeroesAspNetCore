namespace TourOfHeroes.Contracts.Heroes
{
    public sealed record CreateHeroRequest(
        string Name,
        List<SuperPowerRequest>? SuperPowers);

    public sealed record SuperPowerRequest(
        string Name,
        string Description);
}
