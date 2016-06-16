using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExam.Models
{
    public class UserAnswer
    {
        private static int IdCount = 0;
        public UserAnswer()
        {
            this.Id = IdCount++;
        }
        public int Id { get; private set; }

        public TestQuestion TestQuestion { get; set; }
        public List<Answer> ClientAnswers { get; set; }
        public List<Answer> RightAnswers { get; set; }
        public bool IsCorrect { get; set; }
        public string Time { get; set; }
    }
}
