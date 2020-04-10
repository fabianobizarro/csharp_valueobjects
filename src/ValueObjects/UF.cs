using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ValueObjects
{
    public sealed class UF
    {
        private static readonly Dictionary<string, string> _dict = new Dictionary<string, string>
        {
            { "AC", "Acre" },
            { "AL", "Alagoas" },
            { "AP", "Amapá" },
            { "AM", "Amazonas" },
            { "BA", "Bahia" },
            { "CE", "Ceará" },
            { "DF", "Distrito Federal" },
            { "ES", "Espírito Santo" },
            { "GO", "Goiás" },
            { "MA", "Maranhão" },
            { "MT", "Mato Grosso" },
            { "MS", "Mato Grosso do Sul" },
            { "MG", "Minas Gerais" },
            { "PA", "Pará" },
            { "PB", "Paraíba" },
            { "PR", "Paraná" },
            { "PE", "Pernambuco" },
            { "PI", "Piauí" },
            { "RJ", "Rio de Janeiro" },
            { "RN", "Rio Grande do Norte" },
            { "RS", "Rio Grande do Sul" },
            { "RO", "Rondônia" },
            { "RR", "Roraima" },
            { "SC", "Santa Catarina" },
            { "SP", "São Paulo" },
            { "SE", "Sergipe" },
            { "TO" ,"Tocantins" }
        };

        public string Sigla { get; }

        public string Nome { get; }

        public UF(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new ArgumentException("message", nameof(valor)); // TODO: MEnsagemde erro 
            }

            var nomeNormalizado = Normalizado(valor);

            if (_dict.Keys.Contains(nomeNormalizado))
            {
                Sigla = nomeNormalizado;
                Nome = _dict[nomeNormalizado];
            }
            else if (_dict.Values.Any(v => Normalizado(v) == nomeNormalizado))
            {
                var kv = _dict.FirstOrDefault(p => Normalizado(p.Value) == nomeNormalizado);
                Sigla = kv.Key;
                Nome = kv.Value;
            }
            else
            {
                throw new Exception("sdfsdfsdf"); // TODO: MEnsagemde erro 
            }
        }

        private static string RemoverAcentos(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return valor;
            }

            var normalized = valor.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString()
                .Normalize(NormalizationForm.FormC);
        }

        private static string Normalizado(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return valor;
            }

            return RemoverAcentos(valor).ToUpper();
        }

        public string ToString(bool sigla) => sigla ? Sigla : Nome;

        public override string ToString() => ToString(true);

        public static bool TryParse(string valor, out UF uf)
        {
            try
            {
                uf = new UF(valor);
                return true;
            }
            catch
            {
                uf = null;
                return false;
            }
        }
    }
}
