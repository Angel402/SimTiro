using Unity.Netcode;
using UnityEngine;

namespace NetworkServices
{
    public class PlayerObjectInitializer : NetworkBehaviour
    {
        [SerializeField] private GameObject playerCamera;
        [SerializeField] private OVRManager ovrManager;
        [SerializeField] private GameObject networkSoldier, ownSoldier;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsOwner)
            {
                /*networkSoldier.SetActive(false);
                ownSoldier.SetActive(true);*/
                playerCamera.SetActive(true);
                GetComponentInChildren<OVRManager>().InitOVRManager();
            }
            else
            {
                /*networkSoldier.SetActive(true);
                ownSoldier.SetActive(false);*/
            }
            transform.position = new Vector3(transform.position.x, 1.1f, transform.position.z);
        }
    }
}