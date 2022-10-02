using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Students_CRUD.Models
{
    public class StudentResults
    {
        [Key]
        [Required]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Class_N_Sec { get; set; }
        
        public int English { get; set; }
       
        public int Maths { get; set; }
        public int Language { get; set; }
        public int Science { get; set; }
        public int Social { get; set; }

        public int Total { get; set; }
        public int Average { get; set; }
        public string Result { get; set; }

        public string Class { get; set; }


    }
}
