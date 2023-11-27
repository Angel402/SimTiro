using System.Collections.Generic;
using System.Linq;
using ServiceLocatorPath;
using Unity.Netcode;
using UnityEngine;

namespace Michsky.UI.Shift
{
    public class FriendsPanelNetworkBehaviour : NetworkBehaviour
    {
        [SerializeField] private FriendsPanelManager manager;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsServer)
            {
                manager.SetServerActive(NetworkManager);
                NetworkManager.OnClientDisconnectCallback += OnClientDisconnectCallback;
            }
            else
            {
                OnClientConnectedServerRpc(ServiceLocator.Instance.GetService<ILoginUserData>().GetUsername(),
                    NetworkManager.LocalClientId);
            }
        }


        [ServerRpc(RequireOwnership = false)]
        private void OnClientConnectedServerRpc(string username, ulong ownerClientId)
        {
            ulong[] clientIds;
            var activesNames1 = manager.GetActives(out clientIds);
            var maxActiveNameLength = activesNames1.Sum(activeName => activeName.Length + 1);
            char[] activesNames = new char[maxActiveNameLength];
            var cont1 = 0;
            foreach (var activeName in activesNames1)
            {
                foreach (var activeNameChar in activeName)
                {
                    activesNames[cont1] = activeNameChar;
                    cont1++;
                }
                activesNames[cont1] = '*';
            }

            SendConnectedActivesToConnectedClientRpc(activesNames, clientIds, maxActiveNameLength, new ClientRpcParams{Send = new ClientRpcSendParams{TargetClientIds = new ulong[]{ownerClientId}}});
            manager.AddActive(ownerClientId, username);
            OnClientConnectedClientRpc(ownerClientId, username);
        }

        [ClientRpc]
        private void OnClientConnectedClientRpc(ulong ownerClientId, string username)
        {
            if (IsServer) return;
            manager.AddActive(ownerClientId, username);
        }

        [ClientRpc]
        private void SendConnectedActivesToConnectedClientRpc(char[] activesNames, ulong[] activesIds, int maxActiveNameLength, ClientRpcParams clientRpcParams)
        {
            var activesNamesStrings = new string[41];
            var currentString = "";
            var cont = 0;
            foreach (var t in activesNames)
            {
                if (t == '*')
                {
                    activesNamesStrings[cont] = currentString;
                    currentString = "";
                    cont++;
                }
                else
                {
                    currentString += t;
                }
            }
            manager.DrawFriendsInPanel(activesNamesStrings, activesIds);
        }

        private void OnClientDisconnectCallback(ulong id)
        {
            manager.RemoveActive(id);
            OnClientDisconnectCallbackClientRpc(id);
        }
        
        [ClientRpc]
        private void OnClientDisconnectCallbackClientRpc(ulong id)
        {
            if (IsServer) return;
            manager.RemoveActive(id);
        }
    }
}