using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExam.Models;

namespace TestExam
{
    public class TestGenerator
    {
        Random rnd = new Random();

        public TestGenerator()
        {

        }

        public List<TestQuestion> GenerateTest(List<TestQuestion> questions)
        {
            var testQuestions = new List<TestQuestion>();
            var availlableLevels = questions.GroupBy(x => x.Level).Select(x => x.First().Level).ToList();

            foreach (var level in availlableLevels)
            {
                testQuestions.AddRange(this.GetNRandomQuestionsByLevel(questions, level, 2));
            }

            return testQuestions;
        }

        private List<TestQuestion> GetNRandomQuestionsByLevel(List<TestQuestion> questions, int level, int count = 1)
        {
            if (count == 0)
            {
                count = 1;
            }

            var randomQuestions = new List<TestQuestion>();

            var questionsByLevel = questions.Select(x => x).Where(x => x.Level == level).ToList();

            if (questionsByLevel.Count < count)
            {
                count = questionsByLevel.Count;
            }

            var rndNumbers = Enumerable.Range(0, questionsByLevel.Count).OrderBy(t => rnd.Next()).Take(count).ToArray();
            for (int i = 0; i < count; i++)
            {
                randomQuestions.Add(questionsByLevel[rndNumbers[i]]);
            }        

            return randomQuestions;
        }
    }
}
