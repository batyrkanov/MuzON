using Microsoft.AspNet.Identity;
using MuzON.BLL.Interfaces;
using MuzON.BLL.Services;
using MuzON.DAL.EF;
using MuzON.DAL.Identity;
using MuzON.DAL.Repositories;
using MuzON.Domain.Identity;
using MuzON.Domain.Interfaces;
using System;

using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

namespace MuzON.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();
            container.RegisterType<MuzONContext>(new PerRequestLifetimeManager());
            container.RegisterType<IArtistService, ArtistService>(new PerRequestLifetimeManager());
            container.RegisterType<IBandService, BandService>(new PerRequestLifetimeManager());
            container.RegisterType<ICountryService, CountryService>(new PerRequestLifetimeManager());
            container.RegisterType<IUserService, UserService>(new PerRequestLifetimeManager());
            container.RegisterType<ISongService, SongService>(new PerRequestLifetimeManager());
            container.RegisterType<IGenreService, GenreService>(new PerRequestLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager(), new InjectionConstructor("DefaultConnection"));
            container.RegisterType<IUserStore<User, Guid>, UserStore>(new PerRequestLifetimeManager());
            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
        }
    }
}