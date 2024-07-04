using stream_api_challenge.Models;


namespace stream_api_challenge.Services.MovieService
{
    public interface IMovieInterface
    {
        Task<ServiceResponse<List<MovieModel>>> GetMovies(int pageNumber, int pageSize);
        Task<ServiceResponse<MovieModel>> RegisterMovie(MovieModel newMovie);
        Task<ServiceResponse<MovieModel>> GetMovieById(int id);
        Task<ServiceResponse<MovieModel>> UpdateMovie(MovieModel updateMovie);
        Task<ServiceResponse<MovieModel>> DeleteMovie(int id);
        Task<ServiceResponse<List<MovieModel>>> GetMoviesByAverageRating(double minAverageRating);
        Task<ServiceResponse<List<MovieModel>>> GetMoviesByComment(string commentText);
        Task<ServiceResponse<List<MovieModel>>> GetMoviesByYear(int year, int pageNumber, int pageSize);

    }
}
