using Microsoft.EntityFrameworkCore;
using stream_api_challenge.DataContext;
using stream_api_challenge.Enums;
using stream_api_challenge.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace stream_api_challenge.Services.MovieService
{
    public class MovieService : IMovieInterface
    {
        private readonly ApplicationDbContext _context;
        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<ServiceResponse<List<MovieModel>>> GetMovies(int pageNumber, int pageSize)
        {
            ServiceResponse<List<MovieModel>> serviceResponse = new ServiceResponse<List<MovieModel>>();

            try
            {

                var query = _context.Movies
                    .Include(m => m.Streamings)
                    .Include(m => m.Ratings)
                    .AsQueryable();

                int totalRecords = await query.CountAsync();

                var movies = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                serviceResponse.Data = movies;

                if (serviceResponse.Data.Count == 0)
                {
                    serviceResponse.Message = "No data have found!";
                }
                else
                {
                    serviceResponse.Message = $"Page {pageNumber} of {Math.Ceiling((double)totalRecords / pageSize)}";

                }

                serviceResponse.Success = true;

            }
            catch (Exception ex)
            {

                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<MovieModel>> RegisterMovie(MovieModel newMovie)
        {
            ServiceResponse<MovieModel> serviceResponse = new ServiceResponse<MovieModel>();

            try
            {
                if (newMovie == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Inform data!";
                    serviceResponse.Success = false;

                    return serviceResponse;
                }

                var existingMovie = await _context.Movies.FirstOrDefaultAsync(m => m.Title == newMovie.Title);
                if (existingMovie != null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Title already exists!";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                // Define movie reference for each streaming
                if (newMovie.Streamings != null)
                {
                    foreach (var streaming in newMovie.Streamings)
                    {
                        streaming.MovieId = newMovie.Id;
                    }
                }

                // Define movie reference for each rating
                if (newMovie.Ratings != null)
                {
                    foreach (var rating in newMovie.Ratings)
                    {
                        rating.MovieId = newMovie.Id;
                    }   
                }

                newMovie.DateCreated = DateTime.Now.ToLocalTime();
                newMovie.DateUpdated = DateTime.Now.ToLocalTime();

                _context.Add(newMovie);
                await _context.SaveChangesAsync();

                var createdMovie = await _context.Movies
                    .Include(m => m.Streamings)
                    .Include(m => m.Ratings)
                    .FirstOrDefaultAsync(m => m.Id == newMovie.Id); 

                if (createdMovie != null)
                {
                    serviceResponse.Data = createdMovie;
                    serviceResponse.Success = true;
                }
                else
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Movie creation failed.";
                    serviceResponse.Success = false;
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
           }

            return serviceResponse;
        }

        public async Task<ServiceResponse<MovieModel>> GetMovieById(int id)
        {
            ServiceResponse<MovieModel> serviceResponse = new ServiceResponse<MovieModel>();

            try
            {
                MovieModel? movie = await _context.Movies
                    .Include(m => m.Streamings)
                    .Include(m => m.Ratings)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (movie != null)
                {
                    movie.Streamings = movie.Streamings ?? new List<StreamingModel>();
                    movie.Ratings = movie.Ratings ?? new List<RatingModel>();

                    serviceResponse.Data = movie;
                    serviceResponse.Success = true;
                }
                else
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Movie not found";
                    serviceResponse.Success = false;
                }

                serviceResponse.Data = movie;

            }
            catch (Exception ex)
            {

                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<MovieModel>> UpdateMovie(MovieModel updateMovie)
        {
            ServiceResponse<MovieModel> serviceResponse = new ServiceResponse<MovieModel>();

            try
            {
                MovieModel? movie = await _context.Movies
                    .Include(m => m.Streamings)
                    .Include(m => m.Ratings)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == updateMovie.Id);

                if (movie == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Movie not found!";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }


                movie.DateUpdated = DateTime.Now.ToLocalTime();

                _context.Movies.Update(updateMovie);
                await _context.SaveChangesAsync();

                // Inclui as coleções relacionadas no filme atualizado
                MovieModel? updatedMovie = await _context.Movies
                    .Include(m => m.Streamings)
                    .Include(m => m.Ratings)
                    .FirstOrDefaultAsync(m => m.Id == updateMovie.Id);

                if (updatedMovie != null)
                {
                    serviceResponse.Data = updatedMovie;
                    serviceResponse.Success = true;
                }
                else
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Movie update failed.";
                    serviceResponse.Success = false;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        
        public async Task<ServiceResponse<MovieModel>> DeleteMovie(int id)
        {
            ServiceResponse<MovieModel> serviceResponse = new ServiceResponse<MovieModel>();

            try
            {
                MovieModel? movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);

                if (movie == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Movie not found!";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();

                serviceResponse.Data = null;
                serviceResponse.Message = "Movie deleted successfully.";
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<MovieModel>>> GetMoviesByAverageRating([Range(0, 5)] double minAverageRating)
        {
            ServiceResponse<List<MovieModel>> serviceResponse = new ServiceResponse<List<MovieModel>>();

            try
            {
                serviceResponse.Data = await _context.Movies
                    .Include(m => m.Streamings)   
                    .Include(m => m.Ratings)      
                    .Where(m => m.Ratings.Any() && m.Ratings.Average(r => r.Rating) >= minAverageRating)  
                    .ToListAsync();

                if (serviceResponse.Data.Count == 0)
                {
                    serviceResponse.Message = "No movies found with the specified average rating.";
                }
                else
                {
                    serviceResponse.Message = $"Found {serviceResponse.Data.Count} movies with an average rating of {minAverageRating} or higher.";
                }

                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<MovieModel>>> GetMoviesByComment(string commentText)
        {
            ServiceResponse<List<MovieModel>> serviceResponse = new ServiceResponse<List<MovieModel>>();

            try
            {
                serviceResponse.Data = await _context.Movies
                    .Include(m => m.Streamings)   
                    .Include(m => m.Ratings)      
                    .Where(m => m.Ratings.Any(r => r.Comments != null && r.Comments.ToUpper().Contains(commentText.ToUpper())))  
                    .ToListAsync();

                if (serviceResponse.Data.Count == 0)
                {
                    serviceResponse.Message = "No movies found with the specified comment.";
                }
                else
                {
                    serviceResponse.Message = $"Found {serviceResponse.Data.Count} movies with comments containing '{commentText}'.";
                }

                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<MovieModel>>> GetMoviesByYear(int year, int pageNumber, int pageSize)
        {
            ServiceResponse<List<MovieModel>> serviceResponse = new ServiceResponse<List<MovieModel>>();

            try
            {
                var query = _context.Movies
                    .Include(m => m.Streamings)
                    .Include(m => m.Ratings)
                    .Where(m => m.ReleaseDate.Year == year);

                var totalRecords = await query.CountAsync();
                var movies = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                serviceResponse.Data = movies;

                if (serviceResponse.Data.Count == 0)
                {
                    serviceResponse.Message = $"No movies found for the year {year}.";
                }
                else
                {
                    serviceResponse.Message = $"Found {serviceResponse.Data.Count} movies for the year {year}.";
                }

                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

    }


}
