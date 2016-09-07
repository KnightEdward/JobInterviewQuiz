using JobInterviewQuiz.Model.Dtos;

namespace JobInterviewQuiz.Dal.Sql
{
    using System.Data.Entity;

    public partial class QuizEntities : DbContext
    {
        public QuizEntities()
            : base("name=QuizEntities")
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<TechnologyQuestion> TechnologyQuestions { get; set; }
        public virtual DbSet<TestQuestion> TestQuestions { get; set; }
        public virtual DbSet<TestQuestionsAnswer> TestQuestionsAnswers { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestType> TestTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserTest> UserTests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TechnologyQuestion>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.TechnologyQuestion)
                .HasForeignKey(e => e.QuestionTechnologyID);

            modelBuilder.Entity<TestType>()
                .HasMany(e => e.Tests)
                .WithOptional(e => e.TestType)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.TestQuestionsAnswers)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete();
        }
    }
}
