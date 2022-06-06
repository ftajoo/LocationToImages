using Unity;

namespace LocationToImages.Business.DependencyInjection
{
    public class DependencyInjectionExtension : Unity.Extension.UnityContainerExtension
    {
        protected override void Initialize()
        {
            ConfigureDependencies();

            Container.RegisterType<Interfaces.IUserService, User.UserService>();
            Container.RegisterType<Interfaces.ILocationService, Location.LocationService>();
            Container.RegisterType<Interfaces.IPhotoService, Photo.PhotoService>();
        }

        private void ConfigureDependencies()
        {
            Container.AddNewExtension<Repository.DependencyInjection.DependencyInjectionExtension>();
        }
    }
}
