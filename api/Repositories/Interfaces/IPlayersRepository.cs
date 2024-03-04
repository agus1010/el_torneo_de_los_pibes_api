using api.Models.Entities;


namespace api.Repositories.Interfaces
{
    public interface IPlayersRepository : IBaseCRUDRepository<Player>
    {
        ISet<Player> ReadMany(ISet<int> playerIds, bool track = false);
    }
}