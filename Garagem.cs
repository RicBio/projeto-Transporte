using System.Collections.Generic;

namespace ProjetoTransporte
{
    class Garagem
    {
        public string Nome { get; private set; }
        private Stack<Van> veiculosEstacionados;

        public Garagem(string nome)
        {
            Nome = nome;
            veiculosEstacionados = new Stack<Van>();
        }

        public void AdicionarVeiculo(Van van)
        {
            veiculosEstacionados.Push(van); // Uso de pilha
        }

        public Van RemoverVeiculo()
        {
            return veiculosEstacionados.Pop(); // Uso de pilha
        }

        public bool EstaVazia()
        {
            return veiculosEstacionados.Count == 0;
        }

        public IEnumerable<Van> VeiculosEstacionados => veiculosEstacionados;

        public int PotencialTransporte()
        {
            int potencial = 0;
            foreach (var van in veiculosEstacionados)
            {
                potencial += van.Lotacao;
            }
            return potencial;
        }
    }
}
