using System;
using System.Collections.Generic;
using TestExam.Models;

namespace TestExam
{
    public interface IAnswerProccessor
    {
        void ProccessAnswers(List<string> clientAnswers, TestQuestion question, TimeSpan time);
    }
}