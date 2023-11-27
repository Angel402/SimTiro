using System;
using ServiceLocatorPath;
using Unity.Netcode;
using UnityEngine;

namespace NetworkServices
{
    public class SceneInitializer : NetworkBehaviour
    {
        [SerializeField] private Transform sceneCanvas;
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
        private void Start()
        {
            var playerHealth = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<SimplePlayerHealth>();
            playerHealth.enabled = true;
            playerHealth.canvas = sceneCanvas;
            if (IsServer)
            {
                var mapsData = ServiceLocator.Instance.GetService<ITrainingDataService>().GetTrainingData();
                foreach (var mapData in mapsData)
                {
                    foreach (var activeId in mapData.activesInMap)
                    {
                        NetworkManager.ConnectedClients[activeId].PlayerObject.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}