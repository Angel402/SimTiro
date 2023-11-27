using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Michsky.UI.Shift
{
    public class FriendButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private TextMeshProUGUI friendName, mapWhereIsFriend;
        [SerializeField] private Image image;
        public TextMeshProUGUI FriendName => friendName;
        private RectTransform _rectTransform, _friendsContainerTransform, _transformForDrag;
        private CanvasGroup _canvasGroup;
        private ulong _userId;
        private ServerButton _serverWhereIsActive;
        public ulong UserId => _userId;

        private void OnEnable()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _friendsContainerTransform = (RectTransform)_rectTransform.parent;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("pointerDown");
            _canvasGroup.alpha = 0.6f;
            _canvasGroup.blocksRaycasts = false;
            _rectTransform.SetParent(_transformForDrag);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("Drag");
            _rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("EndDrag");
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;

            if (eventData.pointerEnter != null)
            {
                if (eventData.pointerEnter.TryGetComponent(out ServerButton serverButton))
                {
                    serverButton.GetActiveIntoServer(this);
                }
            }
            _rectTransform.SetParent(_friendsContainerTransform);
        }

        public void Configure(string username, RectTransform transformForScroll, ulong id)
        {
            friendName.text = username;
            _transformForDrag = transformForScroll;
            _userId = id;
        }

        public void ActiveAddedInMap(ServerButton serverButton, string mapName)
        {
            if (_serverWhereIsActive != null) _serverWhereIsActive.RemoveActiveFromServer(this);
            _serverWhereIsActive = serverButton;
            mapWhereIsFriend.text = mapName;
        }

        public void DestroyActive()
        {
            if (_serverWhereIsActive != null) _serverWhereIsActive.RemoveActiveFromServer(this);
            Destroy(gameObject);
        }
    }
}