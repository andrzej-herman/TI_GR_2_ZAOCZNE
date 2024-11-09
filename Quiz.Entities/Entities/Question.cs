using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Entities.Entities
{
    [Table("Questions")]
    public class Question
    {

        [Column("QuestionId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; } = Guid.NewGuid();


        [Column("QuestionCategory")]
        public int Category { get; set; }


        [Column("QuestionContent")]
        [StringLength(1000)]
        public string? Content { get; set; }

        public virtual ICollection<Answer> Answers { get; set; } = [];
    }
}
