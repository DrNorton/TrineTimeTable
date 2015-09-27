using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLiteNetExtensionsAsync.Extensions;


namespace TrainTimeTable.LocalEntities.Repositories
{
    public class FavoriteTrainRepository : IFavoriteTrainRepository
    {
        private readonly SqliteContext _context;

        public FavoriteTrainRepository(SqliteContext context)
        {
            _context = context;
        }

        public Task<List<FavoriteTrainPath>> GetAllFavorites()
        {
            return _context.CreateConnection().GetAllWithChildrenAsync<FavoriteTrainPath>(recursive:true);
        }

        public async Task AddToFavorites(long fromEcr, long toEcr)
        {
            var connection = _context.CreateConnection();
            var stationTable= connection.Table<Station>();
            var newFavorite = new FavoriteTrainPath()
            {
                FromId = (await stationTable.Where(x => x.Ecr == fromEcr).FirstOrDefaultAsync()).Id,
                ToId = (await stationTable.Where(x => x.Ecr == toEcr).FirstOrDefaultAsync()).Id
            };
            await connection.InsertAsync(newFavorite);
        }

        public async Task<bool> CheckFavorites(long ecrfrom,long ecrto)
        {
            var connection = _context.CreateConnection();
            var favoriteTable = connection.Table<FavoriteTrainPath>();
            var stationTable = connection.Table<Station>();
            var allFavorites = await connection.GetAllWithChildrenAsync<FavoriteTrainPath>();
            var stations = await stationTable.ToListAsync();
            if (!allFavorites.Any())
            {
                return false;
            }
            else
            {
                var favoriteTrainPaths = allFavorites.FirstOrDefault(
                    x =>
                        (x.From.Ecr == ecrfrom && x.To.Ecr == ecrto) || (x.To.Ecr == ecrfrom && x.From.Ecr == ecrto));
      
                return favoriteTrainPaths!=null;
            }
        
        }

        public async Task DeleteFromFavorites(long ecrfrom, long ecrto)
        {
            var connection = _context.CreateConnection();
            var allFavorites = await connection.GetAllWithChildrenAsync<FavoriteTrainPath>();
            var favoriteTrainPaths = allFavorites.FirstOrDefault(
                    x =>
                        (x.From.Ecr == ecrfrom && x.To.Ecr == ecrto) || (x.To.Ecr == ecrfrom && x.From.Ecr == ecrto));
            await connection.DeleteAsync<FavoriteTrainPath>(favoriteTrainPaths.Id);
        }
    }
}
