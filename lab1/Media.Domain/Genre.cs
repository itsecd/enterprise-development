using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Domain
{
    internal class Genre
    {
        public string Name { get; set; }
        public List<Artist> Artists { get; set; } = new List<Artist>();
        //промежуточный класс для реляции моноги ко многим для жанра и артиста
    }
}
