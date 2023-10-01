using Unity.Netcode;
using UnityEngine;

namespace NetworkServices
{
    public class SceneInitializer : NetworkBehaviour
    {
        
        /*public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            var playerObject = NetworkManager.LocalClient.PlayerObject;
            var playerObjectPosition = playerObject.transform.position;
            NetworkManager.LocalClient.PlayerObject.transform.position =
                new Vector3(playerObjectPosition.x, 1, playerObjectPosition.z);
            playerObject.GetComponent<PlayerObjectInitializer>().Initialize();
            playerObject.GetComponentInChildren<OVRManager>().InitOVRManager();
        }*/
    }
}