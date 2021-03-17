using UnityEngine;
using UnityEngine.EventSystems;

public class WateringMiniGame : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject metronome;
    public float speed = 60f;
    private bool clockWise = true;
    private float dir = -1.0f;
    private bool metronomeStopped = false;
    private float pendulumLength;
    
    private void OnEnable()
    {
        metronome.transform.rotation = Quaternion.Euler(0.0f,0.0f,90.0f);
        pendulumLength = metronome.GetComponent<RectTransform>().rect.height;
        Debug.Log($"Height of pendulum is: {pendulumLength}");
    }
    private void FixedUpdate()
    {
        if(!metronomeStopped)
            MovePendulum();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked on the water thing");
        metronomeStopped = !metronomeStopped;
        Debug.Log("Horizontal miss distance: " + Mathf.Sin(Mathf.Deg2Rad * metronome.transform.localEulerAngles.z) * pendulumLength) ;
        
    }

    private void MovePendulum()
    {
        dir = clockWise ? -1.0f : 1.0f;
        metronome.transform.Rotate(Vector3.forward, dir * speed * Time.deltaTime);
        float currentDegree = metronome.transform.localEulerAngles.z;

        if (clockWise && currentDegree <= 270.0f && currentDegree > 89.0f)
            clockWise = false;
        else if (!clockWise && currentDegree > 90.0f && currentDegree < 269.0f)
            clockWise = true;
    }
    
    public void OnPointerUp(PointerEventData eventData) { } // not implemented, required for onpointerdown
    
}
