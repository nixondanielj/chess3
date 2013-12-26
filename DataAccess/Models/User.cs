using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class User
    {
        public int Id { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
