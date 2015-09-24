using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrainTimeTable.LocalEntities.Repositories
{
    public interface IFavoriteTrainRepository
    {
        Task AddToFavorites(long fromEcr, long toEcr);
        Task<List<FavoriteTrainPath>> GetAllFavorites();
        Task<bool> CheckFavorites(long ecrfrom, long ecrto);
        Task DeleteFromFavorites(long ecr, long l);
    }
}