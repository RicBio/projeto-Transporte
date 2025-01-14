
namespace ProjetoTransporte
{
    class Viagem
    {
        public Van Van { get; private set; }
        public string Origem { get; private set; }
        public string Destino { get; private set; }
        public int Passageiros { get; private set; }

        public Viagem(Van van, string origem, string destino, int passageiros)
        {
            Van = van;
            Origem = origem;
            Destino = destino;
            Passageiros = passageiros;
        }
    }
}
