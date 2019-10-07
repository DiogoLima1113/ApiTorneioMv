namespace api_torneio_mv.Dominio.Entidade
{
    public class RelacionamentoTimeJogador
    {
        public RelacionamentoTimeJogador() {}

        public int Id {get; set;}
        public int IdTime {get; set;}
        public int IdJogador {get; set;}
    }
}