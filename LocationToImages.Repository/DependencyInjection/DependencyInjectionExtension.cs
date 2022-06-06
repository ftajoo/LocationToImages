using System.Net.Http;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace LocationToImages.Repository.DependencyInjection
{
    public class DependencyInjectionExtension : Unity.Extension.UnityContainerExtension
    {
        protected override void Initialize()
        {
            // ContainerControlledLifetimeManager => create a singleton of the class
            Container.RegisterFactory<HttpClient>(x => new HttpClient(), new ContainerControlledLifetimeManager());
            
            Container.RegisterType<Interfaces.IHttpClientProvider, HttpClientProvider.HttpClientProvider>();
            Container.RegisterType<Interfaces.IFoursquareApiRepository, FoursquareApi.FoursquareApiRepository>();
            Container.RegisterType<Interfaces.IFlickrApiRepository, FlickrApi.FlickrApiRepository>();
            Container.RegisterType<Interfaces.IUserRepository, User.UserRepository>();
            Container.RegisterType<Interfaces.ILocationRepository, Location.LocationRepository>();
            Container.RegisterType<Interfaces.IPhotoRepository, Photo.PhotoRepository>();
        }
    }
}