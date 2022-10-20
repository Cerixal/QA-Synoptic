using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Quiz.Data.QA
{
    public partial class QuizContext : DbContext
    {
        public QuizContext()
        {
        }

        public QuizContext(DbContextOptions<QuizContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<Quiz> Quizzes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Answer1)
                    .HasColumnType("character varying")
                    .HasColumnName("Answer");

                entity.Property(e => e.Character).HasColumnType("char");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Questions_id_seq\"'::regclass)");

                entity.Property(e => e.Answer)
                    .HasColumnType("character varying")
                    .HasColumnName("answer");

                entity.Property(e => e.Question1)
                    .HasColumnType("character varying")
                    .HasColumnName("question");

                entity.Property(e => e.QuizId)
                    .HasColumnName("quiz_id")
                    .HasDefaultValueSql("nextval('\"Questions_quiz_id_seq\"'::regclass)");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable("Quiz");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Quizes_id_seq\"'::regclass)");

                entity.Property(e => e.Title)
                    .HasColumnType("character varying")
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Role1)
                    .HasColumnType("character varying")
                    .HasColumnName("role");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Password)
                    .HasColumnType("character varying")
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasColumnType("character varying")
                    .HasColumnName("role");

                entity.Property(e => e.Username)
                    .HasColumnType("character varying")
                    .HasColumnName("username");
            });

            modelBuilder.HasSequence<int>("Questions_quiz_id_seq").HasMax(2);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
