using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
   public class Base
    {  
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.Now;
        public DateTime? LastEditDate { get; set; } = DateTime.Now;
        public bool? IsDelete { get; set; } = false;
    }
}
