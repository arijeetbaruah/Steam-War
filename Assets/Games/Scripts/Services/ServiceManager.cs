using System.Collections.Generic;

namespace Baruah.Service
{
    public static class ServiceManager
    {
        private static Dictionary<System.Type, IService> services;

        public static bool isReady = false;

        static ServiceManager()
        {
            services = new Dictionary<System.Type, IService>();
        }

        /// <summary>
        /// Add Service to ServiceManager
        /// </summary>
        /// <typeparam name="T">IService</typeparam>
        /// <param name="service">IService Object</param>
        public static void Add<T>(T service) where T : IService
        {
            services.Add(typeof(T), service);
            service.Initialize();
        }

        /// <summary>
        /// removes Service to ServiceManager
        /// </summary>
        /// <typeparam name="T">IService</typeparam>
        /// <returns>is the IService Object removed</returns>
        public static bool Remove<T>()
        {
            services[typeof(T)].Release();
            return services.Remove(typeof(T));
        }

        public static IEnumerable<IService> GetAll()
        {
            return services.Values;
        }

        /// <summary>
        /// Retrives Service from ServiceManager
        /// </summary>
        /// <typeparam name="T">IService</typeparam>
        /// <returns>IService Object</returns>
        public static T Get<T>() where T : IService
        {
            return (T) Get(typeof(T));
        }

        /// <summary>
        /// Removes Service to ServiceManager
        /// </summary>
        /// <param name="type">Type of the service</param>
        /// <returns>IService Object</returns>
        public static IService Get(System.Type type)
        {
            if (services.TryGetValue(type, out var service))
            {
                return service;
            }

            return null;
        }
    }

    /// <summary>
    /// Interface to implement the services
    /// </summary>
    public interface IService
    {
        void Initialize();
        void Update(float deltaTime);
        void Release();
    }
}
