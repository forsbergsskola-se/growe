using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void PlayScene(int scIndex)
    {
        SceneManager.LoadScene(scIndex);
    }
}
