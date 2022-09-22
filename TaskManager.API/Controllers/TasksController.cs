using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using TaskManager.Core.DTOs;
using TaskManager.Core.Services;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
      private readonly ITaskServices _taskServices;
      private readonly IMapper _mapper;
      private readonly ILogger<TasksController> _logger;
        public TasksController(ITaskServices taskServices, IMapper mapper, ILogger<TasksController> logger)
        {
            _taskServices = taskServices;
            _mapper = mapper;
            _logger = logger;    
        }
        [HttpGet]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
      public async Task<IActionResult> GetAllTasks()
      {
          Log.Information($"getting all task");
          var result = await _taskServices.GetAllTasksAsync();         
          return Ok(result);
      }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTasks([FromBody] CreateTaskDTO model)
        {
            if (!ModelState.IsValid)
            {
              return BadRequest(ModelState);
                Log.Error($"Error occuried posting to database");
            }

            var res = await _taskServices.AddTaskAsync(model);
            return Ok(res);
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> GetTaskById(string id)
        {
            var results = await _taskServices.GetTaskByIdAsync(id);
            return Ok(results);
        }
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> DeleteTask(string id)
        {
            var results = await _taskServices.DeleteAsync(id);
            return Ok(results);
        }
    }
}
