using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeOut : MonoBehaviour
{

    public Canvas _canvas;

    // Start is called before the first frame update
    void Start()
    {
        _canvas.gameObject.SetActive(true);
        StartCoroutine(WaitForAnimationToEnd());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaitForAnimationToEnd()
    {
        yield return new WaitForSeconds(1f);

        _canvas.gameObject.SetActive(false);
    }
}
