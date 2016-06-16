namespace TestExam.Models
{
    public class Answer
    {
        private static int IdCount = 0;
        public Answer()
        {
            this.Id = IdCount++;
        }
        public int? Id { get; set; }
        public string AnswerProp { get; set; }
        public bool IsCorrectAnswer { get; set; }

        public override string ToString()
        {
            return AnswerProp;
        }
    }
}