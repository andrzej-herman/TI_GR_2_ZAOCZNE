using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Entities.Entities
{
	public class Answer
	{
        [Column("AnswerId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("AnswerContent")]
        [StringLength(300)]
        public string? Content { get; set; }

        [Column("AnswerIsCorrect")]
        public bool IsCorrect { get; set; }

        [ForeignKey("QuestionId")]
        public Guid QuestionId { get; set; }
    }
}
