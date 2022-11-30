using BelajarWebApi.External.Contracts.Ghibli;

namespace BelajarWebApi.Services
{
    public interface IGhibliApi
    {
        Task<IEnumerable<Film>> GetFilms();

        Task<Film> GetFilm(Guid id);
    }
}
