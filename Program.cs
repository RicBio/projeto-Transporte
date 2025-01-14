using System;
using System.Collections.Generic;

namespace ProjetoTransporte
{
    class Program
    {
        static void Main(string[] args)
        {
            EmpresaFretamento empresa = new EmpresaFretamento("Empresa Fretamento");
            int opcao;

            do
            {
                Console.WriteLine(); // Linha em branco no menu
                Console.WriteLine("Menu de Opções:");
                Console.WriteLine("0. Finalizar");
                Console.WriteLine("1. Cadastrar veículo");
                Console.WriteLine("2. Cadastrar garagem");
                Console.WriteLine("3. Iniciar jornada");
                Console.WriteLine("4. Encerrar jornada");
                Console.WriteLine("5. Liberar viagem");
                Console.WriteLine("6. Listar veículos em garagem");
                Console.WriteLine("7. Informar quantidade de viagens");
                Console.WriteLine("8. Listar viagens");
                Console.WriteLine("9. Informar passageiros transportados");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    opcao = 10;
                    Console.Clear();
                    Console.WriteLine("Opção inválida. Digite um número.");
                    continue;
                }

                try
                {
                    switch (opcao)
                    {
                        case 0:
                            Console.Clear();
                            Console.WriteLine("Finalizando...");
                            break;

                        case 1:
                            Console.Write("Digite o ID do veículo: ");
                            int id = int.Parse(Console.ReadLine());
                            Console.Write("Digite a lotação do veículo: ");
                            int lotacao = int.Parse(Console.ReadLine());
                            empresa.CadastrarVan(new Van(id, lotacao));
                            Console.Clear();
                            Console.WriteLine("Veículo cadastrado com sucesso.");
                            break;

                        case 2:
                            Console.Write("Digite o nome da garagem: ");
                            string nomeGaragem = Console.ReadLine();
                            empresa.CadastrarGaragem(new Garagem(nomeGaragem));
                            Console.Clear();
                            Console.WriteLine("Garagem cadastrada com sucesso.");
                            break;

                        case 3:
                            Console.Clear();
                            empresa.IniciarJornada();
                            Console.WriteLine("Jornada iniciada.");
                            break;

                        case 4:
                            if (!empresa.JornadaIniciada)
                            {
                                Console.Clear();
                                Console.WriteLine("A jornada não foi iniciada. Opção indisponível.");
                                break;
                            }
                            empresa.EncerrarJornada();
                            break;

                        case 5:
                            if (!empresa.JornadaIniciada)
                            {
                                Console.Clear();
                                Console.WriteLine("A jornada não foi iniciada. Opção indisponível.");
                                break;
                            }
                            Console.Write("Digite a garagem de origem: ");
                            string origem = Console.ReadLine();
                            Console.Write("Digite a garagem de destino: ");
                            string destino = Console.ReadLine();
                            Console.Write("Digite o número de passageiros: ");
                            int passageiros = int.Parse(Console.ReadLine());
                            Console.Clear();
                            if (empresa.LiberarViagem(origem, destino, passageiros))
                            Console.WriteLine("Viagem liberada com sucesso.");
                            break;

                        case 6:
                            if (!empresa.JornadaIniciada)
                            {
                                Console.Clear();
                                Console.WriteLine("A jornada não foi iniciada. Opção indisponível.");
                                break;
                            }
                            Console.Write("Digite o nome da garagem: ");
                            nomeGaragem = Console.ReadLine();
                            Console.Clear();
                            empresa.ListarVeiculosGaragem(nomeGaragem);
                            break;

                        case 7:
                            if (!empresa.JornadaIniciada)
                            {
                                Console.Clear();
                                Console.WriteLine("A jornada não foi iniciada. Opção indisponível.");
                                break;
                            }
                            Console.Write("Digite a origem: ");
                            origem = Console.ReadLine();
                            Console.Write("Digite o destino: ");
                            destino = Console.ReadLine();
                            int qtdViagens = empresa.InformarQuantidadeViagens(origem, destino);
                            Console.Clear();
                            Console.WriteLine($"Quantidade de viagens de {origem} para {destino}: {qtdViagens}");
                            break;

                        case 8:
                            if (!empresa.JornadaIniciada)
                            {
                                Console.Clear();
                                Console.WriteLine("A jornada não foi iniciada. Opção indisponível.");
                                break;
                            }
                            Console.Write("Digite a origem: ");
                            origem = Console.ReadLine();
                            Console.Write("Digite o destino: ");
                            destino = Console.ReadLine();
                            Console.Clear();
                            empresa.ListarViagens(origem, destino);
                            break;

                        case 9:
                            if (!empresa.JornadaIniciada)
                            {
                                Console.Clear();
                                Console.WriteLine("A jornada não foi iniciada. Opção indisponível.");
                                break;
                            }
                            Console.Write("Digite a origem: ");
                            origem = Console.ReadLine();
                            Console.Write("Digite o destino: ");
                            destino = Console.ReadLine();
                            int passageirosTransportados = empresa.InformarPassageirosTransportados(origem, destino);
                            Console.Clear();
                            Console.WriteLine($"Quantidade de passageiros transportados de {origem} para {destino}: {passageirosTransportados}");
                            break;

                        default:
							Console.Clear();
                            Console.WriteLine("Opção inválida. Tente novamente.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            } while (opcao != 0);
        }
    }
}
