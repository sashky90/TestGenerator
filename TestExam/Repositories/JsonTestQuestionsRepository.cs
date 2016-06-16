using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using TestExam.Models;

namespace TestExam.Repositories
{
    public class JsonTestQuestionsRepository : ITestRepositoryQuestions
    {
        private IList<TestQuestion> questions = new List<TestQuestion>();
        public JsonTestQuestionsRepository()
        {
            var jsonPath = GetJsonDirectoryPath();
            this.questions = ParseJsonQuestionsFromFileToList(jsonPath);

        }
        public void Add(TestQuestion question)
        {
            questions.Add(question);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TestQuestion> GetAll()
        {
            return questions;
        }

        private string GetJsonDirectoryPath()
        {
            var jsonConfigPath = ConfigurationManager.AppSettings["InputJsonPath"];

            if (string.IsNullOrEmpty(jsonConfigPath))
            {
                //TODO: Add dynamic path finder from SqlScriptExecutor
                return Path.GetFullPath("../../JsonQuestions");
            }
            else
            {
                return jsonConfigPath;
            }
        }

        private List<TestQuestion> ParseJsonQuestionsFromFileToList(string fullPath)
        {
            List<TestQuestion> questionsList = new List<TestQuestion>();

            foreach (string file in Directory.EnumerateFiles(fullPath, "*.json"))
            {
                string contents = File.ReadAllText(file);
                var questions = JsonConvert.DeserializeObject<List<TestQuestion>>(contents);
                questionsList.AddRange(questions);
            }
            return questionsList;
        }
    }
}
