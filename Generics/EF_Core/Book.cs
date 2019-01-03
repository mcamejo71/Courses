using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Test1.EF_Core
{
    [Table("Books")]
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(0)]
        public string Publisher { get; set; }
    }
}
