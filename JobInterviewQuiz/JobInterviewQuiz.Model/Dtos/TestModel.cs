using System.Collections.Generic;

namespace JobInterviewQuiz.Model.Dtos
{
    public class TestCustom
    {
        public int Id { get; set; }

        public int TestTypeId { get; set; }

        public string Name { get; set; }

        public string LevelTest { get; set; }

        public int NumberOfQuestions { get; set; }

        public string TestTypeString { get; set; }

        public List<int> TechValuesList { get; set; }



    }
}
