using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExam.Models;

namespace TestExam.Repositories
{
    public class TestAnswerRepository : ITestAnswerRepository
    {
        List<UserAnswer> userAnswers = new List<UserAnswer>();
        public void Add(UserAnswer answer)
        {
            userAnswers.Add(answer);
        }

        public IEnumerable<UserAnswer> GetAll()
        {
            return userAnswers;
        }
    }
}
