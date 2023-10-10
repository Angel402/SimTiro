using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class PlayFabLogin : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI ipAddressText;
        [SerializeField] TMP_InputField ip;

        [SerializeField] string ipAddress;
        [SerializeField] UnityTransport transport;
        [SerializeField] private bool host;
        [SerializeField] private TMP_InputField registerName, registerPassword, loginName, loginPassword;
        [SerializeField] private UnityEvent onRegisteredSuccess, onLoggedSuccess, onRegisteredFailed, onLoggedFailed;
        public void Start()
        {
            /*if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId)){
                /*
                Please change the titleId below to your own titleId from PlayFab Game Manager.
                If you have already set the value in the Editor Extensions, this can be skipped.
                #1#
                PlayFabSettings.staticSettings.TitleId = "42";
            }

            var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true};
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);*/
            /*ipAddress = "0.0.0.0";*/
            GetLocalIPAddress();
        }

        public void StartHost()
        {
            if (NetworkManager.Singleton.StartHost())
            {
                /*NetworkManager.Singleton.SceneManager.LoadScene("Escena de Pruebas", LoadSceneMode.Single);*/
            }
            GetLocalIPAddress();
        }

        public void StartClient() {
            /*ipAddress = ip.text;*/
            SetIpAddress();
            if(NetworkManager.Singleton.StartClient())
            {
				
            }
        }

        private void GetLocalIPAddress() {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList) {
                if (ip.AddressFamily == AddressFamily.InterNetwork) {
                    /*ipAddressText.text = ip.ToString();*/
                    /*ipAddress = ip.ToString();*/
                    return;
                }
            }
            throw new System.Exception("No network adapters with an IPv4 address in the system!");
        }
        
        private void SetIpAddress() {
            transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.ConnectionData.Address = $"192.168.100.140";
        }
        
        public void TryLogin()
        {
            if (loginName.text == String.Empty || loginPassword.text == String.Empty) return;
            var request = new LoginWithPlayFabRequest() { Username = loginName.text, Password = loginPassword.text};
            PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
        }
        
        private void OnLoginSuccess(LoginResult result)
        {
            Debug.Log("logged");
            onLoggedSuccess?.Invoke();
            if (host)
            {
                StartHost();
            }
            else
            {
                StartClient();
            }
        }

        private void OnLoginFailure(PlayFabError error)
        {
            Debug.LogError("couldn't login");
            Debug.LogError(error.GenerateErrorReport());
            onLoggedFailed.Invoke();
        }

        public void TryRegister()
        {
            if (registerName.text == String.Empty || registerPassword.text == String.Empty) return;
            var request = new RegisterPlayFabUserRequest
                {Username = registerName.text, Password = registerPassword.text, RequireBothUsernameAndEmail = false};
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
        }

        private void OnRegisterSuccess(RegisterPlayFabUserResult obj)
        {
            Debug.Log("registred");
            onRegisteredSuccess?.Invoke();
        }
        
        private void OnRegisterFailure(PlayFabError error)
        {
            Debug.LogError("couldn't register");
            Debug.LogError(error.GenerateErrorReport());
            onRegisteredFailed.Invoke();
        }
        
    }
}