using Microsoft.AspNetCore.Mvc;

namespace todo_list_dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private static List<Models.Task> tasks = new List<Models.Task>
    {
        new Models.Task { Id = 1, Title = "Task 1", Description = "Task 1" },
        new Models.Task { Id = 2, Title = "Task 2", Description = "Task 2" }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Models.Task>> Get()
    {
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public ActionResult<Models.Task> Get(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public ActionResult<Models.Task> Post(Models.Task task)
    {
        task.Id = tasks.Max(t => t.Id) + 1;
        tasks.Add(task);
        return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Models.Task updatedTask)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return NotFound();

        task.Title = updatedTask.Title;
        task.Description = updatedTask.Description;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return NotFound();
        tasks.Remove(task);
        return NoContent();
    }
}