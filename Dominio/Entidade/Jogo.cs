namespace api_torneio_mv.Dominio.Entidade
{
    public class Jogo
    {
        public Jogo() {}
        
        public int Id {get; set;}
        public int IdTimeCasa {get; set;}
        public int IdTimeVisitante {get; set;}
        public int PontuacaoTimeCasa {get; set;}
        public int PontuacaoTimeVisitante {get; set;}
    }
}