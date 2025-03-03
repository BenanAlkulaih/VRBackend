using Microsoft.AspNetCore.Mvc;
using VRBackend.Dtos;
using VRBackend.Models;
using VRBackend.Services;

namespace VRBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly QuestionService _questionService;

    public QuestionsController(QuestionService questionService)
    {
        _questionService = questionService;
    }

    // GET: api/Questions
    [HttpGet]
    public ActionResult<List<Question>> Get() =>
        _questionService.Get();

    // GET: api/Questions/{id}
    [HttpGet("{id:length(24)}", Name = "GetQuestion")]
    public ActionResult<Question> Get(string id)
    {
        var question = _questionService.Get(id);
        if (question == null)
        {
            return NotFound();
        }
        return question;
    }

    // POST: api/Questions
    [HttpPost]
    [ProducesResponseType(typeof(Question), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Question> Create(CreateQuestionDto dto)
    {
        var question = new Question
        {
            Text = dto.Text,
            Subject = dto.Subject,
            Options = dto.Options.Select(o => new Option
            {
                Text = o.Text,
                IsCorrect = o.IsCorrect
            }).ToList()
        };

        _questionService.Create(question);
        return CreatedAtRoute("GetQuestion", new { id = question.Id.ToString() }, question);
    }

    // PUT: api/Questions/{id}
    [HttpPut("{id:length(24)}")]
    public IActionResult Update(string id, EditQuestionDto dto)
    {
        var question = _questionService.Get(id);
        if (question == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrWhiteSpace(dto.Text))
        {
            question.Text = dto.Text;
        }

        if (!string.IsNullOrWhiteSpace(dto.Subject))
        {
            question.Subject = dto.Subject;
        }


        if (dto.Options != null && dto.Options.Any())
        {
            question.Options = dto.Options.Select(o => new Option
            {
                Text = o.Text,
                IsCorrect = o.IsCorrect
            }).ToList();
        }

        _questionService.Update(id, question);
        return NoContent();
    }


    // DELETE: api/Questions/{id}
    [HttpDelete("{id:length(24)}")]
    public IActionResult Delete(string id)
    {
        var question = _questionService.Get(id);
        if (question == null)
        {
            return NotFound();
        }
        _questionService.Remove(id);
        return NoContent();
    }
}
