using System;

namespace SmatphoneRegister.Models
{
    public class Smartphones
    {

        public int id;
        public string marca;
        public string modelo;
        public decimal valor;

        public Smartphones(string marca, string modelo, decimal valor)
        {
            Marca = marca;
            Modelo = modelo;
            Valor = valor;
        }

        public Smartphones(int id, string marca, string modelo, decimal valor) : this(marca, modelo, valor)
        {
            Id = id;
       
        }

        public int Id { get => id; set => id = value; }
        public string Marca { get => marca; set => marca = value.ToLower(); }
        public string Modelo { get => modelo; set => modelo = value.ToUpper(); }
        public decimal Valor { get => valor; set => valor = value; }
    }
}
