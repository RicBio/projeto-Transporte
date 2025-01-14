using System;
using System.Collections.Generic;

namespace ProjetoTransporte
{
    class EmpresaFretamento
    {
        public string Nome { get; private set; }
        private List<Van> frota;
        private List<Garagem> garagens;
        private List<Viagem> viagens;
        private bool jornadaIniciada;

        public EmpresaFretamento(string nome)
        {
            Nome = nome;
            frota = new List<Van>();
            garagens = new List<Garagem>();
            viagens = new List<Viagem>();
            jornadaIniciada = false;
        }

        public void CadastrarVan(Van van)
        {
            if (jornadaIniciada)
                throw new InvalidOperationException("Não é possível cadastrar veículos durante a jornada.");

            if (frota.Exists(v => v.Id == van.Id))
                throw new InvalidOperationException($"Já existe uma van com o ID {van.Id}.");

            frota.Add(van);
        }

        public void CadastrarGaragem(Garagem garagem)
        {
            if (jornadaIniciada)
                throw new InvalidOperationException("Não é possível cadastrar garagens durante a jornada.");

            if (garagens.Exists(g => g.Nome == garagem.Nome))
                throw new InvalidOperationException($"Já existe uma garagem com o nome {garagem.Nome}.");

            garagens.Add(garagem);
        }

        public void IniciarJornada()
        {
            if (jornadaIniciada)
                throw new InvalidOperationException("A jornada já foi iniciada.");

            if (frota.Count == 0)
                throw new InvalidOperationException("Não há vans cadastradas para iniciar a jornada.");

            if (garagens.Count < 2)
                throw new InvalidOperationException("São necessárias pelo menos duas garagens cadastradas para iniciar a jornada.");

            int i = 0;
            foreach (var van in frota)
            {
                garagens[i % garagens.Count].AdicionarVeiculo(van);
                i++;
            }

            jornadaIniciada = true;
        }

        public void EncerrarJornada()
        {
            if (!jornadaIniciada)
                throw new InvalidOperationException("A jornada não foi iniciada.");

            Console.Clear();
            Console.WriteLine("Relatório de Jornada:");
            foreach (var van in frota)
            {
                Console.WriteLine($"Van {van.Id}: {van.NumViagens} viagens, {van.PassageirosTransportados} passageiros transportados.");
            }

            jornadaIniciada = false;
        }

        public bool LiberarViagem(string origem, string destino, int passageiros)
        {
            if (!jornadaIniciada)
            {
                Console.WriteLine("Erro: A jornada ainda não foi iniciada.");
                return false;
            }

            var garagemOrigem = garagens.Find(g => g.Nome == origem);

            if (garagemOrigem == null)
            {
                Console.WriteLine("Erro: Origem não cadastrada.");
                return false;
            }

            if (garagemOrigem.EstaVazia())
            {
                Console.WriteLine("Erro: Não há veículos disponíveis na garagem de origem.");
                return false;
            }

            var garagemDestino = garagens.Find(g => g.Nome == destino);

            if (garagemDestino == null)
            {
                Console.WriteLine("Erro: Destino não cadastrado.");
                return false;
            }

            var van = garagemOrigem.RemoverVeiculo(); // Uso de pilha

            if (passageiros > van.Lotacao)
            {
                Console.WriteLine($"Erro: A van não comporta {passageiros} passageiros. Lotação máxima: {van.Lotacao}");
                garagemOrigem.AdicionarVeiculo(van); // Retorna a van para a garagem
                return false;
            }

            van.IniciarViagem(passageiros);
            viagens.Add(new Viagem(van, origem, destino, passageiros));
            garagemDestino.AdicionarVeiculo(van); // Uso de pilha

            return true;
        }

        public void ListarVeiculosGaragem(string nomeGaragem)
        {
            if (!jornadaIniciada)
            {
                Console.WriteLine("Erro: A jornada ainda não foi iniciada.");
                return;
            }

            var garagem = garagens.Find(g => g.Nome == nomeGaragem);
            if (garagem == null)
            {
                Console.WriteLine("Erro: Garagem não encontrada.");
                return;
            }

            Console.Clear();
            Console.WriteLine($"Veículos na garagem {nomeGaragem}:");
            foreach (var van in garagem.VeiculosEstacionados)
            {
                Console.WriteLine($"Van {van.Id} - Lotação: {van.Lotacao}");
            }
            Console.WriteLine($"Potencial de transporte: {garagem.PotencialTransporte()} passageiros.");
        }

        public int InformarQuantidadeViagens(string origem, string destino)
        {
            if (!jornadaIniciada)
            {
                Console.WriteLine("Erro: A jornada ainda não foi iniciada.");
                return 0;
            }

            var viagensFiltradas = viagens.FindAll(v => v.Origem == origem && v.Destino == destino);

            if (viagensFiltradas.Count == 0)
            {
                Console.WriteLine("Nenhuma viagem encontrada.");
            }

            return viagensFiltradas.Count;
        }

        public void ListarViagens(string origem, string destino)
        {
            if (!jornadaIniciada)
            {
                Console.WriteLine("Erro: A jornada ainda não foi iniciada.");
                return;
            }

            var viagensFiltradas = viagens.FindAll(v => v.Origem == origem && v.Destino == destino);
            if (viagensFiltradas.Count == 0)
            {
                Console.WriteLine("Nenhuma viagem encontrada.");
                return;
            }

            Console.Clear();
            Console.WriteLine($"Viagens de {origem} para {destino}:");
            foreach (var viagem in viagensFiltradas)
            {
                Console.WriteLine($"Van {viagem.Van.Id}: {viagem.Passageiros} passageiros.");
            }
        }

        public int InformarPassageirosTransportados(string origem, string destino)
        {
            if (!jornadaIniciada)
            {
                Console.WriteLine("Erro: A jornada ainda não foi iniciada.");
                return 0;
            }

            int totalPassageiros = 0;
            var viagensFiltradas = viagens.FindAll(v => v.Origem == origem && v.Destino == destino);
            foreach (var viagem in viagensFiltradas)
            {
                totalPassageiros += viagem.Passageiros;
            }
            return totalPassageiros;
        }

        // Propriedade pública para acessar o estado de "JornadaIniciada"
        public bool JornadaIniciada
        {
            get { return jornadaIniciada; }
        }
    }
}
