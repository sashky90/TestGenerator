using System.Collections.Generic;
using TestExam.Models;

namespace TestExam.Repositories
{
    public interface ITestAnswerRepository
    {
        void Add(UserAnswer answer);
        IEnumerable<UserAnswer> GetAll();
    }
}