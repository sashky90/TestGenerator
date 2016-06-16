using System;
using System.Collections.Generic;
using TestExam.Models;

namespace TestExam.Repositories
{
    public interface ITestRepositoryQuestions : IDisposable
    {

        void Add(TestQuestion question);

        IEnumerable<TestQuestion> GetAll();

    }
}
