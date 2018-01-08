using System.Configuration;

namespace AuthenticationWebWcf.Service.Config
{
    public class ReBindElement : ConfigurationElement
    {
        private const string InterfaceTypeName = "InterfaceType";
        private const string ImplementationTypeName = "ImplementationType";

        [ConfigurationProperty(InterfaceTypeName, IsRequired = true, IsKey = true)]
        public string InterfaceType
        {
            get { return (string)this[InterfaceTypeName]; }
            set { this[InterfaceTypeName] = value; }
        }

        [ConfigurationProperty(ImplementationTypeName, IsRequired = true)]
        public string ImplementationType
        {
            get { return (string)this[ImplementationTypeName]; }
            set { this[ImplementationTypeName] = value; }
        }
    }
}
