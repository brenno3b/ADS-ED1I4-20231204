using ADS_ED1I4_20231204.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_ED1I4_20231204.controllers
{
    public class ContratoNotFoundException : Exception { }

    public class TipoEquipamentoNotFoundException : Exception { }
    public class EquipamentoOutOfStockException : Exception { }

    internal class EmpresaController
    {
        private int _contratoCount = 0;
        public List<TipoEquipamento> Estoque { get; }
        public List<ContratoLocacao> Contratos { get; }

        public EmpresaController()
        {
            Estoque = new List<TipoEquipamento>();
            Contratos = new List<ContratoLocacao>();
        }

        public void AddTipoEquipamento(TipoEquipamento tipoEquipamento)
        {
            Estoque.Add(tipoEquipamento);
        }

        public TipoEquipamento? GetTipoEquipamento(int id)
        {
            int index = Estoque.FindIndex(e => e.Id.Equals(id));

            if (index == -1) return null;

            return Estoque.ElementAt(index);
        }

        public bool AddEquipamento(int tipoEquipamentoId, Equipamento equipamento)
        {
            int index = Estoque.FindIndex(e => e.Id.Equals(tipoEquipamentoId));

            if (index == -1) return false;

            TipoEquipamento tipoEquipamento = Estoque.ElementAt(index);

            tipoEquipamento.AddEquipamento(equipamento);

            return true;
        }

        public ContratoLocacao? RegistrarContratoLocacao(ContratoLocacao contrato)
        {
            if (contrato.ItensContrato.Count == 0) return null;

            ContratoLocacao newContrato = new(_contratoCount++, contrato.DataSaida, contrato.DataRetorno, contrato.ItensContrato, false);

            Contratos.Add(newContrato);

            return newContrato;
        }

        public ContratoLocacao? GetContratoLocacao(int id)
        {
            int index = Contratos.FindIndex(e => e.Id.Equals(id));

            if (index == -1) return null;

            return Contratos.ElementAt(index);
        }

        public ContratoLocacao LiberarContratoLocacao(int id)
        {
            int contratoIndex = Contratos.FindIndex(e => e.Id.Equals(id));

            if (contratoIndex == -1) throw new ContratoNotFoundException();

            ContratoLocacao contrato = Contratos.ElementAt(contratoIndex);

            for (int i = 0; i < contrato.ItensContrato.Count; i++)
            {
                ItemContrato itemContrato = contrato.ItensContrato.ElementAt(i);

                int tipoEquipamentoIndex = Estoque.FindIndex(e => e.Id.Equals(itemContrato.TipoEquipamento.Id));

                if (tipoEquipamentoIndex == -1) throw new TipoEquipamentoNotFoundException();

                TipoEquipamento tipoEquipamento = Estoque.ElementAt(tipoEquipamentoIndex);

                for (int j = 0; j < itemContrato.Quantidade; j++)
                {
                    Equipamento equipamento = tipoEquipamento.RemoveEquipamento() ?? throw new EquipamentoOutOfStockException();

                    itemContrato.TipoEquipamento.AddEquipamento(equipamento);
                }
            }

            ContratoLocacao contratoLiberado = new(contrato.Id, contrato.DataSaida, contrato.DataRetorno, contrato.ItensContrato, true);

            Contratos.RemoveAt(contratoIndex);
            Contratos.Add(contratoLiberado);

            return contratoLiberado;
        }

        public List<ContratoLocacao> GetContratosLiberados()
        {
            return Contratos.Where(e => e.IsLiberado).ToList();
        }

        public double EncerrarContrato(int id)
        {
            int contratoIndex = Contratos.FindIndex(e => e.Id.Equals(id));

            if (contratoIndex == -1) throw new ContratoNotFoundException();

            ContratoLocacao contrato = Contratos.ElementAt(contratoIndex);

            for (int i = 0; i < contrato.ItensContrato.Count; i++)
            {
                ItemContrato itemContrato = contrato.ItensContrato.ElementAt(i);

                int tipoEquipamentoIndex = Estoque.FindIndex(e => e.Id.Equals(itemContrato.TipoEquipamento.Id));

                if (tipoEquipamentoIndex == -1) throw new TipoEquipamentoNotFoundException();

                TipoEquipamento itemEstoque = Estoque.ElementAt(tipoEquipamentoIndex);

                for (int j = 0; j < itemContrato.TipoEquipamento.Equipamentos.Count; j++)
                {
                    Equipamento equipamento = itemContrato.TipoEquipamento.Equipamentos.ElementAt(j);

                    itemEstoque.AddEquipamento(equipamento);
                }
            }

            Contratos.RemoveAt(contratoIndex);

            return GetValorTotal(contrato);
        }

        private double GetValorTotal(ContratoLocacao contrato)
        {
            double valorTotal = 0;

            foreach (var item in contrato.ItensContrato)
            {
                valorTotal += item.ValorDiarioTotal;
            }

            return valorTotal;
        }
    }
}
