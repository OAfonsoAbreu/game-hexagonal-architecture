using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Game Game { get; set; }
    }
}
