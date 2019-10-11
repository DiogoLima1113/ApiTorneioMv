using System.Collections.Generic;

namespace ApiTorneioMv.Dominio.Entidade
{
    public class Time
    {
        public Time() {}
        
        public int Id {get; set;}
        public string Nome {get; set;}
        public IEnumerable<Jogador> ListaJogadores {get; set;}
    }
}