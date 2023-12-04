using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ADS_ED1I4_20231204.models
{
    internal class Equipamento
    {
        public string NumeroPatrimonio { get; }
        public string Descricao { get; }
        public bool IsAvariado { get; }

        public Equipamento(string numeroPatrimonio, string descricao, bool isAvariado)
        {
            NumeroPatrimonio = numeroPatrimonio;
            Descricao = descricao;
            IsAvariado = isAvariado;
        }

        public override string? ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
