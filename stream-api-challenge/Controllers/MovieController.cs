using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stream_api_challenge.Models;
using stream_api_challenge.Services.MovieService;

namespace stream_api_challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieInterface _movieInterface;
        public MovieController(IMovieInterface movieInterface)
        {
            _movieInterface = movieInterface;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<MovieModel>>>> GetMovies([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(await _movieInterface.GetMovies(pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<MovieModel>>>> RegisterMovie(MovieModel newMovie)
        {
            return Ok(await _movieInterface.RegisterMovie(newMovie));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<MovieModel>>> GetMovieById(int id)
        {
            ServiceResponse<MovieModel> serviceResponse = await _movieInterface.GetMovieById(id);

            return Ok(serviceResponse);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<MovieModel>>> UpdateMovie(MovieModel updateMovie)
        {
            ServiceResponse<MovieModel> serviceResponse = await _movieInterface.UpdateMovie(updateMovie);

            return Ok(serviceResponse);
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<MovieModel>>> DeleteMovie(int id)
        {
            ServiceResponse<MovieModel> serviceResponse = await _movieInterface.DeleteMovie(id);

            return Ok(serviceResponse);

        }

        [HttpGet("/Rating/{minAverageRating}")]
        public async Task<ActionResult<ServiceResponse<List<MovieModel>>>> GetMoviesByAverageRating( double minAverageRating)
        {
            return Ok(await _movieInterface.GetMoviesByAverageRating(minAverageRating));
        }

        [HttpGet("/Rating/by-comment")]
        public async Task<ActionResult<ServiceResponse<List<MovieModel>>>> GetMoviesByComment([FromQuery] string commentText)
        {
            return Ok(await _movieInterface.GetMoviesByComment(commentText));
        }

        [HttpGet("year/{year}")]
        public async Task<ActionResult<ServiceResponse<List<MovieModel>>>> GetMoviesByYear(int year, int pageNumber, int pageSize)
        {
            return Ok(await _movieInterface.GetMoviesByYear(year, pageNumber, pageSize));
        }
    }


}
