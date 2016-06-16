using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExam.Models;
using TestExam.Repositories;
using TestExam.Models.Enums;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace TestExam
{
    class Program
    {
        private static readonly ITestRepositoryQuestions questionsRepository = new JsonTestQuestionsRepository();
        private static readonly ITestAnswerRepository ITestAnswerRepository = new TestAnswerRepository();
        private static IAnswerProccessor answerProccessor = new AnswerProccessor(ITestAnswerRepository);

        private static TestReporter testReporter = new TestReporter(ITestAnswerRepository);

        static void Main()
        {
            var questions = questionsRepository.GetAll().ToList();
            var testGenerator = new TestGenerator();
            Console.WriteLine("Please enter you name: ");
            var userName = Console.ReadLine().ToLower();

            var testQuestions = testGenerator.GenerateTest(questions);
            //var testQuestionsQueue = new Queue<TestQuestion>(testQuestions);

            foreach (var question in testQuestions)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                Console.WriteLine(question.Question);
                PrintAnswers(question);
                char[] delimiters = new[] { ',', ';', ' ' };  // List of your delimiters
                var clientAnswers = Console.ReadLine().Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
                stopwatch.Stop();
                answerProccessor.ProccessAnswers(clientAnswers, question, stopwatch.Elapsed);

            }

            var successrate = testReporter.CalculateAvgSucessRate();

            foreach (var error in testReporter.GetErrorsReport())
            {
                Console.WriteLine(error + Environment.NewLine);
            }
            Console.WriteLine();
            Console.WriteLine("Success rate: {0:P2}.", successrate);
            Console.WriteLine(testReporter.GetUserSkillLevel(successrate, userName, true));
            Console.ReadKey();
        }

        private static void PrintAnswers(TestQuestion question)
        {
            for (int i = 0; i < question.Answers.Count; i++)
            {
                Console.WriteLine("{0}. {1}", (AnswerLetters)(i + 1), question.Answers[i].AnswerProp);
            }
        }
    }
}
