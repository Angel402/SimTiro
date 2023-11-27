using System;
using System.Collections.Generic;
using Michsky.UI.Shift;
using ServiceLocatorPath;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class ServersFacade : MonoBehaviour
    {
        [SerializeField] private Transform serversListParent, addServerButtonTransform;
        [SerializeField] private ServerButton serverButtonTemplate;
        private int _mapCont;
        private List<ServerButton> _servers;

        private void Start()
        {
            serverButtonTemplate.gameObject.SetActive(false);
            _servers = new List<ServerButton>();
        }

        public void CreateNewServer()
        {
            _mapCont++;
            var newServer = Instantiate(serverButtonTemplate, serversListParent);
            newServer.Configure(MapsData.Instance.GetMapData(0, out _), _mapCont);
            addServerButtonTransform.SetParent(null);
            addServerButtonTransform.SetParent(serversListParent);
            _servers.Add(newServer);
        }

        public void StartTraining()
        {
            var mapsData = new List<TrainingData>();
            foreach (var server in _servers)
            {
                var data = new TrainingData(server.MapName, server.ActivesInMap);
                mapsData.Add(data);
            }
            ServiceLocator.Instance.GetService<ITrainingDataService>().SetTrainingData(mapsData);
            NetworkManager.Singleton.SceneManager.LoadScene("Escena de Pruebas", LoadSceneMode.Single);
        }
    }
}