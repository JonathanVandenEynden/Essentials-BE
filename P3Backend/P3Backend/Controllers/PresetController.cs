using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PresetController : ControllerBase
    {
        private readonly IPresetRepository _presetRepo;

        public PresetController(IPresetRepository presetRepo)
        {
            _presetRepo = presetRepo;
        }

        /// <summary>
        /// Return all possible preset surveys
        /// </summary>
        /// <returns>IEnumerable of PresetSurvey</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "ChangeManagerAccess")]
        public IEnumerable<PresetSurvey> GetAll()
        {
            return _presetRepo.GetAll();
        }

        /// <summary>
        /// Returns a certain PresetSurvey by Id
        /// </summary>
        /// <param name="id">Id of the wanted PresetSurvey</param>
        /// <returns>PresetSurvey</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "ChangeManagerAccess")]
        public ActionResult<PresetSurvey> GetPresetSurvey(int id)
        {
            PresetSurvey ps = _presetRepo.GetBy(id);

            if (ps == null)
            {
                return NotFound("PresetSurvey not found");
            }

            return ps;
        }

        /// <summary>
        /// Returns all PresetSurveys with given theme
        /// </summary>
        /// <param name="theme">Theme of PresetSurvey</param>
        /// <returns>IEnumerable of PresetSurvey</returns>
        [HttpGet("GetPresetSurveyBy/{theme}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "ChangeManagerAccess")]
        public IEnumerable<PresetSurvey> GetPresetSurveyBy(string theme)
        {
            return _presetRepo.GetBy(theme);
        }

        /// <summary>
        /// Deletes PresetSurvey with given Id
        /// </summary>
        /// <param name="id">Id of PresetSurvey</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Policy = "AdminAccess")]
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

        /// <summary>
        /// Posts a new PresetSurvey
        /// </summary>
        /// <param name="dto">DTO for making the PresetSurvey</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Policy = "AdminAccess")]
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

            PresetSurvey ps = new PresetSurvey(dto.Theme);
            ps.PresetQuestions.Add(question);

            _presetRepo.Add(ps);
            _presetRepo.SaveChanges();

            return CreatedAtAction(nameof(GetPresetSurvey), new {id = ps.Id}, ps);
        }

        /// <summary>
        /// Posts a list of possible answers to a multiple choice question
        /// </summary>
        /// <param name="questionId">Question where answers should be added</param>
        /// <param name="possibleAnswers">The list of possible answers</param>
        /// <returns></returns>
        [HttpPost("PostAnswerToPresetQuestion/{questionId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Policy = "AdminAccess")]
        public IActionResult PostAnswerToPresetQuestion(int questionId, List<string> possibleAnswers)
        {
            Question question = _presetRepo.GetQuestion(questionId);

            if (question == null)
            {
                return NotFound("There was no question for the given questionId");
            }

            if (question.Type == QuestionType.MULTIPLECHOICE)
            {
                ((MultipleChoiceQuestion)question).AddPossibleAnswers(possibleAnswers, true);
            }

            _presetRepo.UpdateQuestions(question);
            _presetRepo.SaveChanges();
            return NoContent();
        }
    }
}