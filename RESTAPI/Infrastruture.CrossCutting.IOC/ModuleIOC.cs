using Autofac;

namespace Infrastruture.CrossCutting.IOC
{
    public class ModuleIOC: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Carrega IOC

            ConfigurationIOC.Load(builder);

            #endregion
        }
    }
}