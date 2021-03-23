using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SetSelectedButtonsColor : MonoBehaviour
    {
        public Button switchToSignIn;
        public Button switchToLogIn;

        public void LogInSelected()
        {
            switchToLogIn.image.color = Color.green;
            switchToSignIn.image.color = Color.white;
        }
        public void SignInSelected()
        {
            switchToSignIn.image.color = Color.green;
            switchToLogIn.image.color = Color.white;
        }
    }
}