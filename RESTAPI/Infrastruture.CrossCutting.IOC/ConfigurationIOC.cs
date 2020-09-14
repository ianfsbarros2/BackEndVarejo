using Application.Interfaces;
using Application.Services;
using Autofac;
using Domain.Core.Interfaces.Repositories;
using Domain.Core.Interfaces.Services;
using Domain.Services.Services;
using Infrastruture.CrossCutting.Adapter.Interfaces;
using Infrastruture.CrossCutting.Adapter.Map;
using Infrastruture.Repository.Repositories;

namespace Infrastruture.CrossCutting.IOC
{
    public class ConfigurationIOC
    {
        public static void Load(ContainerBuilder builder)
        {
            #region Registra IOC

            #region IOC Application
            builder.RegisterType<ApplicationServiceCliente>().As<IApplicationServiceCliente>();
            builder.RegisterType<ApplicationServiceProduto>().As<IApplicationServiceProduto>();
            builder.RegisterType<ApplicationServicePedido>().As<IApplicationServicePedido>();
            builder.RegisterType<ApplicationServiceRelatorio>().As<IApplicationServiceRelatorio>();
            #endregion

            #region IOC Services
            builder.RegisterType<ServiceCliente>().As<IServiceCliente>();
            builder.RegisterType<ServiceProduto>().As<IServiceProduto>();
            builder.RegisterType<ServicePedido>().As<IServicePedido>();
            #endregion

            #region IOC Repositories SQL
            builder.RegisterType<RepositoryCliente>().As<IRepositoryCliente>();
            builder.RegisterType<RepositoryProduto>().As<IRepositoryProduto>();
            builder.RegisterType<RepositoryPedido>().As<IRepositoryPedido>();
            #endregion

            #region IOC Mapper
            builder.RegisterType<MapperCliente>().As<IMapperCliente>();
            builder.RegisterType<MapperProduto>().As<IMapperProduto>();
            builder.RegisterType<MapperPedido>().As<IMapperPedido>();
            #endregion

            #endregion

        }

    }
}