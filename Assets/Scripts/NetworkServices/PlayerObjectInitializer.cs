using Unity.Netcode;
using UnityEngine;

namespace NetworkServices
{
    public class PlayerObjectInitializer : NetworkBehaviour
    {
        [SerializeField] private GameObject playerCamera;
        [SerializeField] private OVRManager ovrManager;
        [SerializeField] private GameObject networkSoldier, ownSoldier;
        public void Initialize()
        {
            if (NetworkObject == NetworkManager.LocalClient.PlayerObject)
            {
                Debug.Log("initialize");
                playerCamera.SetActive(true);
            }
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (NetworkObject == NetworkManager.LocalClient.PlayerObject)
            {
                networkSoldier.SetActive(false);
                ownSoldier.SetActive(true);
            }
            else
            {
                networkSoldier.SetActive(true);
                ownSoldier.SetActive(false);
            }
        }
    }
}