using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExam.Models
{
    public class TestQuestion
    {
        private static int IdCount = 0;
        public TestQuestion()
        {
            this.Id = IdCount++;
        }
        public int? Id { get; set; }
        public int Level { get; set; }
        public string Question { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
