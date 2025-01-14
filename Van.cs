namespace ProjetoTransporte
{
    class Van
    {
        public int Id { get; private set; }
        public int Lotacao { get; private set; }
        public int NumViagens { get; private set; }
        public int PassageirosTransportados { get; private set; }

        public Van(int id, int lotacao)
        {
            Id = id;
            Lotacao = lotacao;
            NumViagens = 0;
            PassageirosTransportados = 0;
        }

        public void IniciarViagem(int passageiros)
        {
            NumViagens++;
            PassageirosTransportados += passageiros;
        }
    }
}
