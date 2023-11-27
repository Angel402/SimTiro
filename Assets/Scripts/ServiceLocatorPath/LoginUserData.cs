namespace ServiceLocatorPath
{
    public class LoginUserData : ILoginUserData
    {
        private string _username;

        public void SetUsername(string loginNameText)
        {
            _username = loginNameText;
        }

        public string GetUsername()
        {
            return _username;
        }
    }
}