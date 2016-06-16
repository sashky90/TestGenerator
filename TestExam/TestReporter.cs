using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExam.Models;
using TestExam.Repositories;

namespace TestExam
{
    public class TestReporter
    {
        ITestAnswerRepository answerRepository;
        public TestReporter(ITestAnswerRepository answerRepository)
        {
            this.answerRepository = answerRepository;
        }

        public decimal CalculateAvgSucessRate()
        {
            List<UserAnswer> userAnswers = answerRepository.GetAll().ToList();
            var correctAnswers = GetRightAnswersOfUser();

            return (decimal)correctAnswers.Count / (decimal)userAnswers.Count;
        }

        public List<string> GetErrorsReport()
        {
            List<string> errorsMessages = new List<string>();

            var wrongAnswers = getWrongAnswersOfUser();

            foreach (var userAnswer in wrongAnswers)
            {
                StringBuilder errorMessage = new StringBuilder();
                errorMessage.AppendFormat("Error on question: ID:", userAnswer.Id)
                    .AppendFormat(" Question: {0}", userAnswer.TestQuestion.Question)
                    .AppendLine()
                    .AppendFormat("Your answer: {0}", GetAnswerListToString(userAnswer.ClientAnswers))
                    .AppendLine()
                    .AppendFormat("Correct answer is: {0}", GetAnswerListToString(userAnswer.RightAnswers));

                errorsMessages.Add(errorMessage.ToString());
            }

            return errorsMessages;
        }

        public string GetUserSkillLevel(decimal score, string name, bool isJoke = false)
        {
            if (isJoke)
            {
                switch (name)
                {
                    case "niki":
                    case "eli":
                    case "sasho":
                    case "dido":
                        return "                                                                                                                                                                                         \r\n                                                                                         dddddddd                                                                                         \r\nNNNNNNNN        NNNNNNNN                   kkkkkkkk                                      d::::::d                                                                iiii  kkkkkkkk           \r\nN:::::::N       N::::::N                   k::::::k                                      d::::::d                                                               i::::i k::::::k           \r\nN::::::::N      N::::::N                   k::::::k                                      d::::::d                                                                iiii  k::::::k           \r\nN:::::::::N     N::::::N                   k::::::k                                      d:::::d                                                                       k::::::k           \r\nN::::::::::N    N::::::N    eeeeeeeeeeee    k:::::k    kkkkkkkaaaaaaaaaaaaa      ddddddddd:::::dyyyyyyy           yyyyyyyrrrrr   rrrrrrrrr   nnnn  nnnnnnnn    iiiiiii  k:::::k    kkkkkkk\r\nN:::::::::::N   N::::::N  ee::::::::::::ee  k:::::k   k:::::k a::::::::::::a   dd::::::::::::::d y:::::y         y:::::y r::::rrr:::::::::r  n:::nn::::::::nn  i:::::i  k:::::k   k:::::k \r\nN:::::::N::::N  N::::::N e::::::eeeee:::::eek:::::k  k:::::k  aaaaaaaaa:::::a d::::::::::::::::d  y:::::y       y:::::y  r:::::::::::::::::r n::::::::::::::nn  i::::i  k:::::k  k:::::k  \r\nN::::::N N::::N N::::::Ne::::::e     e:::::ek:::::k k:::::k            a::::ad:::::::ddddd:::::d   y:::::y     y:::::y   rr::::::rrrrr::::::rnn:::::::::::::::n i::::i  k:::::k k:::::k   \r\nN::::::N  N::::N:::::::Ne:::::::eeeee::::::ek::::::k:::::k      aaaaaaa:::::ad::::::d    d:::::d    y:::::y   y:::::y     r:::::r     r:::::r  n:::::nnnn:::::n i::::i  k::::::k:::::k    \r\nN::::::N   N:::::::::::Ne:::::::::::::::::e k:::::::::::k     aa::::::::::::ad:::::d     d:::::d     y:::::y y:::::y      r:::::r     rrrrrrr  n::::n    n::::n i::::i  k:::::::::::k     \r\nN::::::N    N::::::::::Ne::::::eeeeeeeeeee  k:::::::::::k    a::::aaaa::::::ad:::::d     d:::::d      y:::::y:::::y       r:::::r              n::::n    n::::n i::::i  k:::::::::::k     \r\nN::::::N     N:::::::::Ne:::::::e           k::::::k:::::k  a::::a    a:::::ad:::::d     d:::::d       y:::::::::y        r:::::r              n::::n    n::::n i::::i  k::::::k:::::k    \r\nN::::::N      N::::::::Ne::::::::e         k::::::k k:::::k a::::a    a:::::ad::::::ddddd::::::dd       y:::::::y         r:::::r              n::::n    n::::ni::::::ik::::::k k:::::k   \r\nN::::::N       N:::::::N e::::::::eeeeeeee k::::::k  k:::::ka:::::aaaa::::::a d:::::::::::::::::d        y:::::y          r:::::r              n::::n    n::::ni::::::ik::::::k  k:::::k  \r\nN::::::N        N::::::N  ee:::::::::::::e k::::::k   k:::::ka::::::::::aa:::a d:::::::::dd d::::d       y:::::y           r:::::r              n::::n    n::::ni::::::ik::::::k   k:::::k \r\nNNNNNNNN         NNNNNNN    eeeeeeeeeeeeee kkkkkkkk    kkkkkkkaaaaaaaaaa  aaaa  ddddddddd   ddddd      y:::::y            rrrrrrr              nnnnnn    nnnnnniiiiiiiikkkkkkkk    kkkkkkk\r\n                                                                                                      y:::::y                                                                             \r\n                                                                                                     y:::::y                                                                              \r\n                                                                                                    y:::::y                                                                               \r\n                                                                                                   y:::::y                                                                                \r\n                                                                                                  yyyyyyy";
                    default:
                        break;
                }
            }     

            if (score < 0.25M)
            {
                return "Ше те правим QA";
            }
            else if (score < 0.5M)
            {
                return "Spec flow master!";
            }
            else if (score < 0.8M)
            {
                return "Trainee";
            }
            else if (score < 0.99M)
            {
                return "You are good performer!";
            }
            else
            {
                return "Top Dev! Welcome to the team!";
            }
        }

        private List<UserAnswer> GetRightAnswersOfUser()
        {
            return answerRepository.GetAll().Select(x => x).Where(x => x.IsCorrect == true).ToList();
        }

        private List<UserAnswer> getWrongAnswersOfUser()
        {
            return answerRepository.GetAll().Select(x => x).Where(x => x.IsCorrect == false).ToList();
        }

        private string GetAnswerListToString(List<Answer> answers)
        {
            var answerStringList = "";
            foreach (var a in answers)
            {
                answerStringList += a.ToString() + ", ";
            }
            return answerStringList.TrimEnd(',');
        }
    }
}
