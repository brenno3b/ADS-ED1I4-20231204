using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_ED1I4_20231204.models
{
    internal class ContratoLocacao
    {
        public int Id { get; }
        public DateTime DataSaida { get; }
        public DateTime DataRetorno { get; }
        public List<ItemContrato> ItensContrato { get; }
        public bool IsLiberado { get; }

        public ContratoLocacao(int id, DateTime dataSaida, DateTime dataRetorno, List<ItemContrato> itensContrato, bool isLiberado)
        {
            Id = id;
            DataSaida = dataSaida;
            DataRetorno = dataRetorno;
            ItensContrato = itensContrato;
            IsLiberado = isLiberado;
        }

        public ContratoLocacao(DateTime dataSaida, DateTime dataRetorno)
        {
            DataSaida = dataSaida;
            DataRetorno = dataRetorno;
            ItensContrato = new List<ItemContrato>();
            IsLiberado = false;
        }

        public void AddItensContrato(ItemContrato itemContrato)
        {
            ItensContrato.Add(itemContrato);
        }

        public override string? ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
