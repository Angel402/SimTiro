using UnityEngine;

namespace Michsky.UI.Shift
{
    public class FriendsPanelManager : MonoBehaviour
    {
        [SerializeField] private FriendButton friendButtonTemplate;
            Animator windowAnimator;
        bool isOn = false;

        void Start()
        {
            windowAnimator = gameObject.GetComponent<Animator>();
            friendButtonTemplate.gameObject.SetActive(false);
            DrawFriendsInPanel();
        }

        private void DrawFriendsInPanel()
        {
            
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
    }
}