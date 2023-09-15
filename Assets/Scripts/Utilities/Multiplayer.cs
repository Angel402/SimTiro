using System.Net;
using System.Net.Sockets;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class Multiplayer : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI ipAddressText;
        [SerializeField] TMP_InputField ip;

        [SerializeField] string ipAddress;
        [SerializeField] UnityTransport transport;

        void Start()
        {
            ipAddress = "0.0.0.0";
            SetIpAddress();
        }
		

        public void StartHost()
        {
            if (NetworkManager.Singleton.StartHost())
            {
                NetworkManager.Singleton.SceneManager.LoadScene("Escena de Pruebas", LoadSceneMode.Single);
            }
            GetLocalIPAddress();
        }

        public void StartClient() {
            ipAddress = ip.text;
            SetIpAddress();
            if(NetworkManager.Singleton.StartClient())
            {
				
            }
        }

        private void GetLocalIPAddress() {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList) {
                if (ip.AddressFamily == AddressFamily.InterNetwork) {
                    ipAddressText.text = ip.ToString();
                    ipAddress = ip.ToString();
                    return;
                }
            }
            throw new System.Exception("No network adapters with an IPv4 address in the system!");
        }

        private void SetIpAddress() {
            transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.ConnectionData.Address = ip.text;
        }
    }
}