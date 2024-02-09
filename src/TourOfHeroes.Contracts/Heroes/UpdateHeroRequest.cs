namespace TourOfHeroes.Contracts.Heroes
{
    public sealed record UpdateHeroRequest(
        string Name, 
        List<SuperPowerRequest>? SuperPowers);
}
