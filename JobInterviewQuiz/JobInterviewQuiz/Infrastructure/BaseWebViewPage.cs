using System;
using System.Web.Mvc;

namespace JobInterviewQuiz.Infrastructure
{
    public class BaseWebViewPage<T> : WebViewPage<T>
    {
        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public QuizUser QuizUser
        {
            get
            {
                return this.Session["QuizUser"] as QuizUser;
            }
        }
    }
}