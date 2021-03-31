using UnityEngine;

namespace UI {
    public class OpenURL : MonoBehaviour
    {


        public void Open(string url)
        {
            Application.OpenURL(url);
        }
    }
}
