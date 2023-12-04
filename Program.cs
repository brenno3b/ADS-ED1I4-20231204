using ADS_ED1I4_20231204.controllers;
using ADS_ED1I4_20231204.models;

EmpresaController _controller = new();

void addTipoEquipamento()
{
    Console.WriteLine("--- Cadastrar tipo equipamento ---");
    Console.WriteLine();

    Console.Write("Digite o ID: ");
    int id = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    Console.Write("Digite a descrição: ");
    string descricao = Console.ReadLine();
    Console.WriteLine();

    Console.Write("Digite o valor da diária: ");
    double valorDiaria = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine();

    TipoEquipamento tipoEquipamento = new(id, descricao, valorDiaria);

    _controller.AddTipoEquipamento(tipoEquipamento);

    Console.WriteLine(tipoEquipamento);

    Console.WriteLine();

    Console.WriteLine("Tipo equipamento adicionado.");

    Console.WriteLine("\n--- Fim do cadastro de tipo equipamento ---\n");
}

void getTipoEquipamento()
{
    Console.WriteLine("--- Consultar tipo equipamento ---");
    Console.WriteLine();

    Console.Write("Digite o ID: ");
    int id = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    TipoEquipamento? tipoEquipamento = _controller.GetTipoEquipamento(id);

    if (tipoEquipamento == null)
    {
        Console.WriteLine("Tipo equipamento não encontrado.");

        Console.WriteLine("\n--- Fim da consulta de tipo de equipamento ---\n");

        return;
    }

    Console.WriteLine(tipoEquipamento);

    Console.WriteLine("\n--- Fim da consulta de tipo equipamento ---\n");
}

void addEquipamento()
{
    Console.WriteLine("--- Cadastrar equipamento ---");
    Console.WriteLine();

    Console.Write("Digite o ID do tipo equipamento: ");
    int id = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    if (_controller.GetTipoEquipamento(id) == null)
    {
        Console.WriteLine("Tipo equipamento não encontrado.");

        Console.WriteLine("\n--- Fim do cadastro de equipamento ---\n");

        return;
    }

    Console.Write("Digite a identificação de patrimônio: ");
    string numeroPatrimonio = Console.ReadLine();
    Console.WriteLine();

    Console.Write("Digite a descricao: ");
    string descricao = Console.ReadLine();
    Console.WriteLine();

    Equipamento equipamento = new(numeroPatrimonio, descricao, false);

    _controller.AddEquipamento(id, equipamento);

    Console.WriteLine(equipamento);

    Console.WriteLine();

    Console.WriteLine($"Equipamento adicionado: {equipamento}");

    Console.WriteLine("\n--- Fim do cadastro de equipamento ---\n");
}

void registrarContratoLocacao()
{
    Console.WriteLine("--- Registrar contrato locação ---");
    Console.WriteLine();

    Console.Write("Digite o dia de saída: ");
    int diaSaida = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    Console.Write("Digite o mês de saída: ");
    int mesSaida = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    Console.Write("Digite o ano de saída: ");
    int anoSaida = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    DateTime dataSaida = new DateTime(anoSaida, mesSaida, diaSaida);

    Console.WriteLine($"Data de saída: {diaSaida}/{mesSaida}/{anoSaida}");

    Console.Write("Digite o dia de retorno: ");
    int diaRetorno = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    Console.Write("Digite o mês de retorno: ");
    int mesRetorno = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    Console.Write("Digite o ano de retorno: ");
    int anoRetorno = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    DateTime dataRetorno = new DateTime(anoRetorno, mesRetorno, diaRetorno);

    Console.WriteLine($"Data de retorno: {diaRetorno}/{mesRetorno}/{anoRetorno}");

    List<ItemContrato> itemContratoList = new List<ItemContrato>();

    while (true)
    {
        Console.Write("Digite o ID do tipo de equipamento: ");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine();

        TipoEquipamento? tipoEquipamento = _controller.GetTipoEquipamento(id);

        if (tipoEquipamento == null)
        {
            Console.WriteLine("Tipo equipamento não encontrado.");

            continue;
        }

        Console.Write("Digite a quantidade de equipamentos: ");
        int quantidade = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine();

        TipoEquipamento newTipoEquipamento = new(tipoEquipamento.Id, tipoEquipamento.Descricao, tipoEquipamento.ValorDiaria);

        ItemContrato itemContrato = new(newTipoEquipamento, quantidade);

        itemContratoList.Add(itemContrato);

        Console.Write("Deseja adicionar um novo tipo de item ? (y/n) ");
        string option = Console.ReadLine();
        Console.WriteLine();

        if (option.ToUpper() == "N") break;
    }

    ContratoLocacao contrato = new(dataSaida, dataRetorno);

    foreach (var item in itemContratoList)
    {
        contrato.AddItensContrato(item);
    }

    ContratoLocacao? newContract = _controller.RegistrarContratoLocacao(contrato);

    if (newContract == null)
    {
        Console.WriteLine("Houve um erro ao registrar o contrato.");

        Console.WriteLine("\n--- Fim do registro do contrato de locação ---\n");

        return;
    }

    Console.WriteLine($"Contrato registrado: {newContract}");

    Console.WriteLine("\n--- Fim do registro do contrato de locação ---\n");
}

void getAllContratos()
{
    Console.WriteLine("--- Consultar contratos ---");
    Console.WriteLine();

    foreach (var item in _controller.Contratos)
    {
        Console.WriteLine($"{item}");
    }

    Console.WriteLine("\n--- Fim da consulta de contratos ---\n");
}

void liberarContrato()
{
    Console.WriteLine("--- Liberação de contrato de locação ---");
    Console.WriteLine();

    Console.Write("Digite o ID do contrato: ");
    int id = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    try
    {
        ContratoLocacao contrato = _controller.LiberarContratoLocacao(id);

        Console.WriteLine($"Contrato liberado: {contrato}");
    } catch (ContratoNotFoundException)
    {
        Console.WriteLine("Contrato não encontrado.");
    } catch (TipoEquipamentoNotFoundException)
    {
        Console.WriteLine("Tipo equipamento não encontrado");
    } catch (EquipamentoOutOfStockException)
    {
        Console.WriteLine("Não há equipamentos suficientes ou disponíveis em estoque");
    } finally
    {
        Console.WriteLine("\n--- Fim da liberação de contrato ---\n");
    }
}

void getContratosLiberados()
{
    Console.WriteLine("--- Consultar contratos liberados ---");
    Console.WriteLine();

    foreach (var item in _controller.GetContratosLiberados())
    {
        Console.WriteLine($"{item}");
    }

    Console.WriteLine("\n--- Fim da consulta de contratos ---\n");
}

void devolverEquipamentos()
{
    Console.WriteLine("--- Devolução de equipamentos ---");
    Console.WriteLine();

    Console.Write("Digite o ID do contrato: ");
    int id = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine();

    try
    {
        double valorDevido = _controller.EncerrarContrato(id);

        Console.WriteLine($"Você deve o valor de: {valorDevido} reais");
    }
    catch (ContratoNotFoundException)
    {
        Console.WriteLine("Contrato não encontrado.");
    }
    catch (TipoEquipamentoNotFoundException)
    {
        Console.WriteLine("Tipo equipamento não encontrado");
    }
    finally
    {
        Console.WriteLine("\n--- Fim da devolução de equipamentos ---\n");
    }
}

while (true)
{
    Console.WriteLine("0. Finalizar processo");
    Console.WriteLine("1. Cadastrar tipo equipamento");
    Console.WriteLine("2. Consultar tipo equipamento");
    Console.WriteLine("3. Cadastar equipamento");
    Console.WriteLine("4. Registrar contrato de locação");
    Console.WriteLine("5. Consultar contratos de locação");
    Console.WriteLine("6. Liberar contrato de locação");
    Console.WriteLine("7. Consultar contratos de locação liberados");
    Console.WriteLine("6. Devolver equipamentos de locação liberados");

    Console.WriteLine();

    int option = Convert.ToInt32(Console.ReadLine());
    Console.Clear();

    if (option == 0) break;
    if (option == 1) addTipoEquipamento();
    if (option == 2) getTipoEquipamento();
    if (option == 3) addEquipamento();
    if (option == 4) registrarContratoLocacao();
    if (option == 5) getAllContratos();
    if (option == 6) liberarContrato();
    if (option == 7) getContratosLiberados();
    if (option == 8) devolverEquipamentos();
}