using Baruah.AbilitySystem;
using Baruah.Configs;
using Baruah.Logger;
using Baruah.Service;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Baruah
{
    public class ServiceBinder : MonoBehaviour
    {
        public static ServiceBinder instance;


        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private IEnumerator Start()
        {
            ServiceManager.isReady = false;

            string loggerPath = Path.Combine(Application.persistentDataPath, "game.log");
            Logger.Log.SetLogger(new UnityLogger(loggerPath));

            ServiceManager.Add(new ConfigService());
            ServiceManager.Add(new AbilityService());
            ServiceManager.isReady = true;

            yield return SceneManager.LoadSceneAsync(1);
        }

        private void Update()
        {
            foreach(IService service in ServiceManager.GetAll())
            {
                service.Update(Time.deltaTime);
            }
        }
    }
}
