﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc;
using Quiz.Data.QA;
using Quiz.Web.Models;
using Quiz.Data.Repository.IRepository;
using Quiz.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.SharePoint.Client;
using System.Security.Claims;
using System.Formats.Asn1;

namespace Quiz.Web.Controllers
{
    public class QuizController : Controller
    {
        private readonly IQAUnitOfWork _qaRepo;
        private readonly IMapper _mapper;

        public QuizController(IQAUnitOfWork qaRepo, IMapper mapper)
        {
            _qaRepo = qaRepo;
            _mapper = mapper;
        }

        // GET: HomeController
        public async Task<ActionResult> QuizTable()
        {

            //if (toDate.Value.DayOfWeek == DayOfWeek.Thursday) toDate.Value.AddDays(4);
            //if(fromDate.Value.DayOfWeek == DayOfWeek.Thursday) fromDate.Value.AddDays(-4);
            try
            {
                // Get the prices back from the database

                IEnumerable<Data.QA.Quiz> Quizzes = await _qaRepo.Quiz.GetAllAsync();

                // If prices are found return view
                QuizTableVM quizTableVM = new();

                if (Quizzes.Any())
                {
                    quizTableVM.Quizzes = new List<QuizVM>();
                    foreach (var quiz in Quizzes)
                    {
                        QuizVM QuizTable = _mapper.Map<Data.QA.Quiz, QuizVM>(quiz);
                        quizTableVM.Quizzes.Add(QuizTable);
                    }

                    return View(quizTableVM);
                }
                // No prices found set the message to display
                ViewData["NoPricesFoundMessage"] = "No values found, please select a new range";

                //Empty model to make the resulting view clean and stop
                // null ref exception.
                return View(quizTableVM);
            }

            catch (DbException ex)
            {
                ModelState.AddModelError("", "Error connecting to the database,"
                                             + "please try again and if the problem persists contact Tom.");
                return View();
            }
        }
        public async Task<IEnumerable<SelectListItem>> GetQuestionList(int id)
        {
            IEnumerable<SelectListItem> Questions = (await _qaRepo.Question.GetAllAsync()).Select(n =>
            new SelectListItem()
            {
                Text = n.Question1,
                Value = n.Id.ToString(),
                Selected = (n.Id == id)
            });
            return Questions;
        }
        public async Task<IActionResult> Questions(int id, QuestionVM source)
        {

            IEnumerable<Question> Questions = await _qaRepo.Question.GetAllAsync(e => e.QuizId == id);

            // If prices are found return view
            QuestionVM questionTableVM = new();

            if (Questions.Any())
            {
                questionTableVM.Qustions = new List<QuestionVM>();
                foreach (var question in Questions)
                {
                    QuestionVM questionTable = _mapper.Map<Question, QuestionVM>(question);
                    questionTableVM.Qustions.Add(questionTable);
                }

                return View(questionTableVM);
            }
            // No prices found set the message to display
            ViewData["NoPricesFoundMessage"] = "No values found, please select a new range";

            //Empty model to make the resulting view clean and stop
            // null ref exception.
            return View(questionTableVM);

        }
        public IActionResult AddQuestion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitQuestion(QuestionVM source, int id)
        {
            source.Id = id;
            Question entity = _mapper.Map<QuestionVM, Question>(source);
            
                _qaRepo.Question.Add(entity);
                await _qaRepo.SaveAsync();
                return RedirectToAction("EditSuccess");
        }

        public async Task<IActionResult> ViewQuestion(int id, QuestionVM source)
        {
            var Question = _qaRepo.Question.GetFirstOrDefault(e => e.Id == id);
            QuestionVM entity = _mapper.Map<Question, QuestionVM>(Question);
            return View(entity);
        }
        public async Task<IActionResult> ViewAnswers(int id, AnswerVM source)
        {
            IEnumerable<Answer> answers = await _qaRepo.Answer.GetAllAsync(e => e.QuestionId == id);

            AnswerVM answerTableVM = new();

            if (answers.Any())
            {
                answerTableVM.Answers = new List<AnswerVM>();
                foreach (var answer in answers)
                {
                    AnswerVM answerTable = _mapper.Map<Answer, AnswerVM>(answer);
                    answerTableVM.Answers.Add(answerTable);
                }
                return View(answerTableVM);
            }
            ViewData["NoPricesFoundMessage"] = "No values found, please select a new range";
            return View(answerTableVM);
        }
        public async Task<IActionResult> ViewAnswer(int id, AnswerVM source)
        {
            var Answer = _qaRepo.Answer.GetFirstOrDefault(e => e.Id == id);
            AnswerVM entity = _mapper.Map<Answer, AnswerVM>(Answer);
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> EditQuestion(QuestionVM question)
        {
            Question entity = _mapper.Map<QuestionVM, Question>(question);
            await _qaRepo.Question.UpdateAsync(entity);
            await _qaRepo.SaveAsync();
            return RedirectToAction(nameof(EditSuccess));
        }

        [HttpPost]
        public async Task<IActionResult> EditAnswer(AnswerVM answer)
        {
            Answer entity = _mapper.Map<AnswerVM, Answer>(answer);
            await _qaRepo.Answer.UpdateAsync(entity);
            await _qaRepo.SaveAsync();
            return RedirectToAction(nameof(EditSuccess));
        }
        public IActionResult EditSuccess()
        {
            return View();
        }

    }

}