using System.Configuration;

namespace AuthenticationWebWcf.Service.Config
{
    [ConfigurationCollection(typeof(ReBindElement))]
    public class ReBindElementCollection : ConfigurationElementCollection
    {
        #region Overrides of ConfigurationElementCollection

        protected override ConfigurationElement CreateNewElement()
        {
            return new ReBindElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var rebind = (ReBindElement) element;
            return rebind.InterfaceType;
        }


        #endregion
    }
}
