using Baruah.Service;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Baruah.Configs
{
    public class ConfigService : IService
    {
        private ConfigRegistry configRegistry;

        public void Initialize()
        {
            configRegistry = Addressables.LoadAssetAsync<ConfigRegistry>("ConfigRegistry").WaitForCompletion();
        }

        public void Release()
        {
            Addressables.Release(configRegistry);
        }

        public void Update(float deltaTime)
        {
        }

        public T Get<T>() where T : class, IConfig
        {
            if (configRegistry.registry.TryGetValue(typeof(T).FullName, out ScriptableObject so))
            {
                return (T)(so as IConfig);
            }

            return null;
        }
    }
}
