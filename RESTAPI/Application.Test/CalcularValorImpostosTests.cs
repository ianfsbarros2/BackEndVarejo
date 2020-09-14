using System;
using System.Collections.Generic;
using System.Linq;
using Application.DTO.DTO;
using Application.Interfaces;
using Moq;
using Xunit;

namespace Application.Test
{
    public class CalcularValorImpostosTests
    {
        [Fact]
        public void CalcularValorImpostoComListaPedidosNula()
        {
            //Arrange
            var mock = new Mock<IApplicationServiceRelatorio>();
            mock.Setup(m =>
                m.CalculateValorImpostoMensal(null)).Returns(0);

            //Act
            const int expected = 0;
            var actual = mock.Object.CalculateValorImpostoMensal(null);

            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void CalcularValorImpostoComListaPedidosVazia()
        {
            //Arrange
            var mock = new Mock<IApplicationServiceRelatorio>();
            mock.Setup(m =>
                m.CalculateValorImpostoMensal(Enumerable.Empty<PedidoDTO>())).Returns(0);

            //Act
            const decimal expected = 0;
            var actual = mock.Object.CalculateValorImpostoMensal(Enumerable.Empty<PedidoDTO>());

            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void CalcularValorImpostoComUmOuMaisPedidosNulos()
        {
            //Arrange
            var objTestePedidos = new List<PedidoDTO>
            {
                null,
                new PedidoDTO
                {
                    Cliente = null,
                    Observacao = null,
                    CodigoPedido = 1,
                    DataPedido = DateTime.Now,
                    Produtos = new List<ProdutoDTO>()
                    {
                        new ProdutoDTO
                        {
                            Fabricacao = 2,
                            Nome = null,
                            Tamanho = null,
                            Valor = 100,
                            CodigoProduto = 1
                        }
                    },
                    FormaPagamento = 1
                }
            };

            var mock = new Mock<IApplicationServiceRelatorio>();
            mock.Setup(m =>
                m.CalculateValorImpostoMensal(objTestePedidos)).Returns(15);
            
            //Act
            const decimal expected = 15;
            var actual = mock.Object.CalculateValorImpostoMensal(objTestePedidos);

            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void CalcularValorImpostoComUmOuMaisPedidosDeValorZeroNaoGeraErro()
        {
            //Arrange
            var objTestePedidos = new List<PedidoDTO>
            {
                new PedidoDTO
                {
                    Cliente = null,
                    Observacao = null,
                    CodigoPedido = 2,
                    DataPedido = DateTime.Now,
                    Produtos = new List<ProdutoDTO>()
                    {
                        new ProdutoDTO
                        {
                            Fabricacao = 1,
                            Nome = null,
                            Tamanho = null,
                            Valor = 200,
                            CodigoProduto = 2
                        }
                    },
                    FormaPagamento = 1
                },
                new PedidoDTO
                {
                    Cliente = null,
                    Observacao = null,
                    CodigoPedido = 1,
                    DataPedido = DateTime.Now,
                    Produtos = new List<ProdutoDTO>()
                    {
                        new ProdutoDTO
                        {
                            Fabricacao = 2,
                            Nome = null,
                            Tamanho = null,
                            Valor = 0,
                            CodigoProduto = 1
                        }
                    },
                    FormaPagamento = 1
                }
            };

            var mock = new Mock<IApplicationServiceRelatorio>();
            mock.Setup(m =>
                m.CalculateValorImpostoMensal(objTestePedidos)).Returns(20);
            
            //Act
            const decimal expected = 20;
            var actual = mock.Object.CalculateValorImpostoMensal(objTestePedidos);

            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void CalcularValorImpostoComUmOuMaisPedidosSemFabricacaoDefinida()
        {
            //Arrange
            var objTestePedidos = new List<PedidoDTO>
            {
                new PedidoDTO
                {
                    Cliente = null,
                    Observacao = null,
                    CodigoPedido = 99,
                    DataPedido = new DateTime(2000,12,21),
                    Produtos = new List<ProdutoDTO>()
                    {
                        new ProdutoDTO
                        {
                            Fabricacao = 1,
                            Nome = null,
                            Tamanho = null,
                            Valor = 100,
                            CodigoProduto = 43
                        }
                    },
                    FormaPagamento = 2
                },
                new PedidoDTO
                {
                    Cliente = null,
                    Observacao = null,
                    CodigoPedido = 1,
                    DataPedido = DateTime.Now,
                    Produtos = new List<ProdutoDTO>()
                    {
                        new ProdutoDTO
                        {
                            Fabricacao = 0,
                            Nome = null,
                            Tamanho = null,
                            Valor = 100,
                            CodigoProduto = 1
                        }
                    },
                    FormaPagamento = 1
                }
            };

            var mock = new Mock<IApplicationServiceRelatorio>();
            mock.Setup(m =>
                m.CalculateValorImpostoMensal(objTestePedidos)).Throws<Exception>();
            
            //Act
            //Assert
            Assert.Throws<Exception>(() => mock.Object.CalculateValorImpostoMensal(objTestePedidos));
        }
    }
}