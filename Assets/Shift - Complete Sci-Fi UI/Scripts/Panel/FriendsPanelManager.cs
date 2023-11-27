using System;
using System.Collections.Generic;
using Meta.WitAi;
using PlayFab;
using ServiceLocatorPath;
using Unity.Netcode;
using UnityEngine;

namespace Michsky.UI.Shift
{
    public class FriendsPanelManager : MonoBehaviour
    {
        [SerializeField] private FriendButton friendButtonTemplate;
            Animator windowAnimator;
        bool isOn = false;
        private Dictionary<ulong, FriendButton> _connectedActives;
        [SerializeField] private RectTransform transformForScroll;

        void Start()
        {
            windowAnimator = gameObject.GetComponent<Animator>();
            friendButtonTemplate.gameObject.SetActive(false);
            _connectedActives = new Dictionary<ulong, FriendButton>();
        }

        public void SetServerActive(NetworkManager networkManager)
        {
            AddActive(networkManager.LocalClient.ClientId,
                ServiceLocator.Instance.GetService<ILoginUserData>().GetUsername());
        }

        public void AnimateWindow()
        {
            if (isOn == false)
            {
                windowAnimator.CrossFade("Window In", 0.1f);
                isOn = true;
            }

            else
            {
                windowAnimator.CrossFade("Window Out", 0.1f);
                isOn = false;
            }
        }

        public void WindowIn()
        {
            if (isOn == false)
            {
                windowAnimator.CrossFade("Window In", 0.1f);
                isOn = true;
            }
        }

        public void WindowOut()
        {
            if (isOn == true)
            {
                windowAnimator.CrossFade("Window Out", 0.1f);
                isOn = false;
            }
        }

        public void RemoveActive(ulong id)
        {
            var active = _connectedActives[id];
            _connectedActives.Remove(id);
            active.DestroyActive();
        }
        
        public void AddActive(ulong id, string username)
        {
            Debug.Log("friendAdded");
            var button = Instantiate(friendButtonTemplate, friendButtonTemplate.transform.parent);
            button.gameObject.SetActive(true);
            button.Configure(username, transformForScroll, id);
            _connectedActives.Add(id, button);
        }

        public string[] GetActives(out ulong[] clientIds)
        {
            var strings = new List<string>();
            var ulongs = new List<ulong>();
            foreach (var active in _connectedActives)
            {
                strings.Add(active.Value.FriendName.text);
                ulongs.Add(active.Key);
            }
            clientIds = ulongs.ToArray();
            return strings.ToArray();
        }

        public void DrawFriendsInPanel(string[] activesNames, ulong[] activesIds)
        {
            for (int i = 0; i < activesNames.Length; i++)
            {
                if (double.IsNaN(activesIds[i])) return;
                AddActive(activesIds[i], activesNames[i]);
            }
        }
    }
}