using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using Xunit;

namespace ValueObjects.Tests
{
    public class CPFTests
    {
        [Theory]
        [InlineData("11111111111")]
        [InlineData("22222222222")]
        [InlineData("33333333333")]
        [InlineData("44444444444")]
        [InlineData("55555555555")]
        [InlineData("66666666666")]
        [InlineData("77777777777")]
        [InlineData("88888888888")]
        [InlineData("99999999999")]
        [InlineData("00000000000")]
        public void TryParse_NumerosRepetidos_DeveRetornarFalse(string number)
        {
            // Arrange, Act
            var result = CPF.TryParse(number, out var cpf);

            // Assert
            result.ShouldBeFalse();
            cpf.ShouldBeNull();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("asb1231")]
        [InlineData("s6dfsd51f2%¨6$@3459")]
        [InlineData("12345678912348949")]
        [InlineData("1234567981")]
        [InlineData("87654321--4165")]
        public void TryParse_(string number)
        {
            // Arrange, Act
            var result = CPF.TryParse(number, out var cpf);

            // Assert
            result.ShouldBeFalse();
            cpf.ShouldBeNull();
        }

        [Theory]
        [InlineData("17417013333")]
        [InlineData("07525481156")]
        [InlineData("32581412666")]
        [InlineData("28097865858")]
        [InlineData("33101899885")]
        [InlineData("568.497.178-47")]
        [InlineData("102.348.290-87")]
        [InlineData("258.837.477-87")]
        [InlineData("332.449.331-57")]
        public void TryParse_NumerosValidos_DeveRetornarTrue(string number)
        {
            // Arrange, Act
            var result = CPF.TryParse(number, out var cpf);

            // Assert
            result.ShouldBeTrue();
            cpf.ShouldNotBeNull();
            cpf.Numero.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public void teste()
        {
            // Arrange
            const string ValorEsperado = "288.777.748-35";
            var cpf = new CPF("28877774835");

            // Act
            var result = cpf.ToString(true);

            // Assert
            result.ShouldNotBeNullOrWhiteSpace();
            result.ShouldBe(ValorEsperado);
        }

    }
}
