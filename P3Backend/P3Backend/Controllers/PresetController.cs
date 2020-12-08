
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3Backend.Model;
using P3Backend.Model.DTO_s;
using P3Backend.Model.Questions;
using P3Backend.Model.RepoInterfaces;

namespace P3Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PresetController : ControllerBase
    {
        private readonly IPresetRepository _presetRepo;

        public PresetController(IPresetRepository presetRepo)
        {
            _presetRepo = presetRepo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<PresetSurvey> GetAll()
        {
            return _presetRepo.GetAll();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PresetSurvey> GetPresetSurvey(int id)
        {
            PresetSurvey ps = _presetRepo.GetBy(id);

            if (ps == null)
            {
                return NotFound("PresetSurvey not found");
            }

            return ps;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeletePresetSurvey(int id)
        {
            PresetSurvey ps = _presetRepo.GetBy(id);

            if (ps == null)
            {
                return NotFound("PresetSurvey not found");
            }
            
            _presetRepo.Delete(ps);
            _presetRepo.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult PostPresetSurvey(PresetSurveyDTO dto)
        {
            Question question = null;
            QuestionDTO questionDTO = dto.PresetQuestion;
            switch (dto.PresetQuestion.Type)
                {
                    case QuestionType.MULTIPLECHOICE:
                        question = new MultipleChoiceQuestion(questionDTO.QuestionString);
                        break;
                    case QuestionType.RANGED:
                        question = new RangedQuestion(questionDTO.QuestionString);
                        break;
                    case QuestionType.YESNO:
                        question = new YesNoQuestion(questionDTO.QuestionString);
                        break;
                    case QuestionType.OPEN:
                        question = new OpenQuestion(questionDTO.QuestionString);
                        break;
                }
                if (question == null)
                {
                    throw new Exception();
                }

            PresetSurvey ps = new PresetSurvey(dto.Theme, question);
            
            _presetRepo.Add(ps);
            _presetRepo.SaveChanges();

            return CreatedAtAction(nameof(GetPresetSurvey), new { id = ps.Id}, ps);
        }
    }
}