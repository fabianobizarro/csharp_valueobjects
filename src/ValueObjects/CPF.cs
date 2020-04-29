using System;
using System.Collections.Generic;

namespace ValueObjects
{
    public sealed class CPF
    {
        public string Numero { get; }

        public bool ExibirMascara { get; set; }

        public CPF(string valor, bool exibirMascara = false)
        {
            if (string.IsNullOrEmpty(valor))
                throw new ArgumentNullException(nameof(valor));

            if (!Valido(valor))
                throw new ArgumentException("CPF inválido");

            Numero = RemoverMascara(valor);
            ExibirMascara = exibirMascara;
        }

        private static readonly IList<string> NumerosInvalidos = new List<string>
        {
            "00000000000",
            "11111111111",
            "22222222222",
            "33333333333",
            "44444444444",
            "55555555555",
            "66666666666",
            "77777777777",
            "88888888888",
            "99999999999",
        };

        public static string RemoverMascara(string cpf)
        {
            return cpf?.Trim()
                     .Replace(".", "")
                     .Replace("-", "");
        }

        public static string AplicarMascara(string cpf)
        {
            return cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");

        }

        private static bool Valido(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = RemoverMascara(cpf);

            if (cpf.Length != 11)
                return false;

            if (NumerosInvalidos.Contains(cpf))
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;
            string digito;

            int soma;
            int resto;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            resto = resto < 2
                ? 0
                : 11 - resto;

            digito = resto.ToString();
            tempCpf += digito;
            soma = 0;

            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            resto = resto < 2
                ? 0
                : 11 - resto;

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

        public override string ToString() => ToString(ExibirMascara);

        public string ToString(bool mascara)
        {
            return mascara
                ? AplicarMascara(Numero)
                : RemoverMascara(Numero);
        }

        public static bool TryParse(string valor, out CPF cpf)
        {
            try
            {
                cpf = new CPF(valor);
                return true;
            }
            catch
            {
                cpf = null;
                return false;
            }
        }

        public bool Equals(CPF other)
        {
            return other is null
                ? false
                : Numero == other.Numero;
        }

        public static implicit operator CPF(string value) => new CPF(value);

        public static implicit operator string(CPF value) 
            => value?.ToString(value.ExibirMascara);
    }
}
