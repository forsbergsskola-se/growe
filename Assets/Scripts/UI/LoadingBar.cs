using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Slider))]
public class LoadingBar : MonoBehaviour
{
    Slider slider;

    void Start(){
        slider = GetComponent<Slider>();
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync ()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        while ( !op.isDone )
        {
            float progress = Mathf.Clamp01(op.progress / .9f);
    
            slider.value = progress;
    
            yield return null;
        }
    }
}