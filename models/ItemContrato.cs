using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_ED1I4_20231204.models
{
    internal class ItemContrato
    {
        public TipoEquipamento TipoEquipamento { get; }
        public int Quantidade { get; }

        public double ValorDiarioTotal => TipoEquipamento.ValorDiaria * Quantidade;

        public ItemContrato(TipoEquipamento tipoEquipamento, int quantidade)
        {
            TipoEquipamento = tipoEquipamento;
            Quantidade = quantidade;
        }

        public override string? ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
