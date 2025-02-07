using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerGeneral : MonoBehaviour
{
    private float timergameover = 300;
    
    public TextMeshProUGUI timertext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timergameover -= Time.deltaTime;
        float minute = Mathf.Floor(timergameover / 60);
        float second = Mathf.Floor(timergameover % 60);
        if (timertext != null)
        {
            timertext.text = string.Format("{0:0}:{1:00}", minute, second);
        }
        if (timergameover <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
