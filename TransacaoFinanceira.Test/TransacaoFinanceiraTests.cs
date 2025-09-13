using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TransacaoFinanceira.Main.Models;
using TransacaoFinanceira.Main.Repositories.Interfaces;
using TransacaoFinanceira.Main.Services;

namespace TransacaoFinanceira.Test
{
    [TestClass]
    public class TransacaoFinanceiraTests
    {
        [TestMethod]
        public void Transferencia_ComSaldoSuficiente_DeveSerEfetivada()
        {
            // Arrange
            var origem = new ContasSaldo(938485762, 200);
            var destino = new ContasSaldo(2147483649, 50);

            var mockRepo = new Mock<IContaRepository>();
            mockRepo.Setup(r => r.GetSaldo(origem.Conta)).Returns(origem);
            mockRepo.Setup(r => r.GetSaldo(destino.Conta)).Returns(destino);
            mockRepo.Setup(r => r.Atualizar(It.IsAny<ContasSaldo>())).Returns(true);

            var service = new TransacaoService(mockRepo.Object);

            // Act
            var resultado = service.ExecutarTransferencia(1, origem.Conta, destino.Conta, 100);

            // Assert
            Assert.IsTrue(resultado);
            Assert.AreEqual(100, origem.Saldo);
            Assert.AreEqual(150, destino.Saldo);
        }

        [TestMethod]
        public void Transferencia_ComSaldoInsuficiente_DeveSerCancelada()
        {
            // Arrange
            var origem = new ContasSaldo(2147483649, 0);
            var destino = new ContasSaldo(210385733, 10);

            var mockRepo = new Mock<IContaRepository>();
            mockRepo.Setup(r => r.GetSaldo(origem.Conta)).Returns(origem);
            mockRepo.Setup(r => r.GetSaldo(destino.Conta)).Returns(destino);

            var service = new TransacaoService(mockRepo.Object);

            // Act
            var resultado = service.ExecutarTransferencia(2, origem.Conta, destino.Conta, 50);

            // Assert
            Assert.IsFalse(resultado);
            Assert.AreEqual(0, origem.Saldo);
            Assert.AreEqual(10, destino.Saldo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Transferencia_ContaOrigemInexistente_DeveLancarExcecao()
        {
            var mockRepo = new Mock<IContaRepository>();
            mockRepo.Setup(r => r.GetSaldo(It.IsAny<long>())).Returns((ContasSaldo)null);

            var service = new TransacaoService(mockRepo.Object);

            service.ExecutarTransferencia(3, 999999999, 210385733, 50);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Transferencia_ContaDestinoInexistente_DeveLancarExcecao()
        {
            var origem = new ContasSaldo(938485762, 200);

            var mockRepo = new Mock<IContaRepository>();
            mockRepo.Setup(r => r.GetSaldo(origem.Conta)).Returns(origem);
            mockRepo.Setup(r => r.GetSaldo(999999999)).Returns((ContasSaldo)null);

            var service = new TransacaoService(mockRepo.Object);

            service.ExecutarTransferencia(4, origem.Conta, 999999999, 50);
        }
    }
}
