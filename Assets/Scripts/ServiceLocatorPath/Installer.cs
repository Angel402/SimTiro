using UnityEngine;

namespace ServiceLocatorPath
{
    public class Installer : MonoBehaviour
    {
        private void Awake()
        {
            if (FindObjectsOfType<Installer>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }
            ServiceLocator.Instance.RegisterService<ILoginUserData>(new LoginUserData());
            ServiceLocator.Instance.RegisterService<ITrainingDataService>(new TrainingDataService());
            /*ServiceLocator.Instance.RegisterService<ILocalVoiceChatServer>(new LocalVoiceChatServer());*/
        }   
    }
}