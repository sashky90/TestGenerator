using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExam.Models;
using TestExam.Models.Enums;
using TestExam.Repositories;

namespace TestExam
{
    public class AnswerProccessor : IAnswerProccessor
    {
        ITestAnswerRepository answerRepository;
        public AnswerProccessor(ITestAnswerRepository answerRepository)
        {
            this.answerRepository = answerRepository;
        }

        public void ProccessAnswers(List<string> clientAnswers, TestQuestion question, TimeSpan time)
        {
            //Parse answers
            var parsedAnswers = ParseClientAnswers(clientAnswers, question);
            //check if they are correct
            var rightAnswers = GetRightAnswers(question);
            var isAnswerCorrect = IsAnswerCorrect(rightAnswers, parsedAnswers);
            //Save the answers
            SaveUserAnswer(question, parsedAnswers, rightAnswers, isAnswerCorrect, time);
        }

        private void SaveUserAnswer(TestQuestion question, List<Answer> clientParsedAnswers, List<Answer> rightAnswers, bool isAnswerCorrect, TimeSpan time)
        {
            var userAnswer = new UserAnswer
            {
                TestQuestion = question,
                ClientAnswers = clientParsedAnswers,
                RightAnswers = rightAnswers,
                IsCorrect = isAnswerCorrect,
                Time = time.ToString(@"m\:ss")
        };
            answerRepository.Add(userAnswer);
        }

        private static bool IsAnswerCorrect(List<Answer> rightAnswer, List<Answer> parsedAnswers)
        {
            return rightAnswer.All(parsedAnswers.Contains);
        }

        private List<Answer> ParseClientAnswers(List<string> clientAnswers, TestQuestion question)
        {
            var parsedAnswers = new List<Answer>();
            foreach (var answer in clientAnswers)
            {
                AnswerLetters parsedEnum;
                Enum.TryParse(answer, out parsedEnum);
                if (parsedEnum != 0)
                {
                    parsedAnswers.Add(question.Answers[(int)parsedEnum - 1]);
                }
            }

            return parsedAnswers;
        }

        private List<Answer> GetRightAnswers(TestQuestion question)
        {
            return question.Answers.Select(x => x).Where(x => x.IsCorrectAnswer == true).ToList();
        }
    }
}
