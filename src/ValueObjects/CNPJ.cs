using System;

namespace ValueObjects
{
    public sealed class CNPJ
    {
        public string Numero { get; }

        public CNPJ(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
                throw new ArgumentException("message", nameof(numero));

            if (!Valido(numero))
                throw new Exception("excecao");

            Numero = numero;
        }

        private static bool Valido(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
            {
                return false;
            }

            var tempCnpj = cnpj.Substring(0, 12);
            var soma = 0;

            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }

            var resto = (soma % 11);

            resto = resto < 2
                ? 0
                : 11 - resto;

            var digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            }

            resto = (soma % 11);

            resto = resto < 2
                ? 0
                : 11 - resto;

            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public override string ToString() => ToString(true);

        public string ToString(bool removerMascara = false)
        {
            return !removerMascara
                ? Numero
                : Numero.Replace(".", string.Empty)
                        .Replace("/", string.Empty)
                        .Replace("-", string.Empty);
        }

        public static bool TryParse(string valor, out CNPJ cnpj)
        {
            try
            {
                cnpj = new CNPJ(valor);
                return true;
            }
            catch (Exception)
            {
                cnpj = null;
                return false;
            }
        }

        public bool Equals(CNPJ other)
        {
            return other is null
                ? false
                : Numero == other.Numero;
        }

        public static implicit operator CNPJ(string value) => new CNPJ(value);
    }
}
