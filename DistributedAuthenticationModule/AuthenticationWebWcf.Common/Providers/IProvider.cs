namespace AuthenticationWebWcf.Common.Providers
{
    public interface IProvider
    {
        TService Get<TService>();

        TService Get<TService>(string namedBinding);
    }
}