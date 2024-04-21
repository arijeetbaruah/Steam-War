using Baruah.Configs;
using Baruah.Service;
using Newtonsoft.Json;

namespace Baruah.AbilitySystem
{
    public class AbilityService : IService
    {
        public AbilityDatabase database;

        public void Initialize()
        {
            database = ServiceManager.Get<ConfigService>().Get<AbilityDatabase>();
        }

        public void Release()
        {
            database = null;
        }

        public void Update(float deltaTime)
        {
            
        }

        public TAbility Get<TAbility>(AbiltiyDatabaseElementData data) where TAbility : class, IAbility, new()
        {
            TAbility ability = JsonConvert.DeserializeObject<TAbility>(data.additionalData);

            return ability;
        }
    }
}
