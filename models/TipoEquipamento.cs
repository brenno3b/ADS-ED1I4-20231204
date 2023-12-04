using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS_ED1I4_20231204.models
{
    internal class TipoEquipamento
    {
        public int Id { get; }

        public string Descricao { get; }

        public Stack<Equipamento> Equipamentos { get; }
        public double ValorDiaria { get; }

        public TipoEquipamento(int id, string descricao, double valorDiaria)
        {
            Id = id;
            Descricao = descricao;
            Equipamentos = new Stack<Equipamento>();
            ValorDiaria = valorDiaria;
        }

        public void AddEquipamento(Equipamento equipamento)
        {
            Equipamentos.Push(equipamento);
        }

        public Equipamento? RemoveEquipamento()
        {
            if (Equipamentos.Count == 0) return null;

            return Equipamentos.Pop();
        }

        public override string? ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
