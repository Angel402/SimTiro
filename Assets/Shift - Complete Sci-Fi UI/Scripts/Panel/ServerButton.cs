using System;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Michsky.UI.Shift
{
    public class ServerButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI serverName, maxPlayersText, mapName;
        [SerializeField] private Image mapImage;
        private int _currentMapSelected;
        private int _currentPlayersConnected, _currentMapMaxPlayers;
        private List<ulong> _activesInMap;
        public string MapName => mapName.text;
        public List<ulong> ActivesInMap => _activesInMap;

        public void ChangeMap()
        {
            _currentMapSelected++;
            Configure(MapsData.Instance.GetMapData(_currentMapSelected, out var wasFirst));
            if (wasFirst) _currentMapSelected = 0;
        }

        public void Configure(MapsData.MapData mapData, int mapCount = -1)
        {
            _activesInMap = new List<ulong>();
            _currentMapMaxPlayers = mapData.maxActives;
            maxPlayersText.text = $"{_currentPlayersConnected}/{_currentMapMaxPlayers}";
            mapName.text = mapData.name;
            mapImage.sprite = mapData.preview;
            gameObject.SetActive(true);
            if (mapCount != -1) serverName.text = $"server {mapCount}";
        }

        public void GetActiveIntoServer(FriendButton friendButton)
        {
            if (_activesInMap.Count >= _currentMapMaxPlayers || _activesInMap.Contains(friendButton.UserId)) return;
            _activesInMap.Add(friendButton.UserId);
            _currentPlayersConnected++;
            maxPlayersText.text = $"{_currentPlayersConnected}/{_currentMapMaxPlayers}";
            friendButton.ActiveAddedInMap(this, serverName.text);
        }

        public void RemoveActiveFromServer(FriendButton friendButton)
        {
            _activesInMap.Remove(friendButton.UserId);
            _currentPlayersConnected--;
            maxPlayersText.text = $"{_currentPlayersConnected}/{_currentMapMaxPlayers}";
        }
    }
}