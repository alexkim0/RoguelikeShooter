using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI timeText;

    private void Update()
    {
        float currentTime = Time.time;

        int minutes = Mathf.FloorToInt(currentTime / 60F);
        int seconds = Mathf.FloorToInt(currentTime % 60F);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}  