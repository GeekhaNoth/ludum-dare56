using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HandsOnScreen : MonoBehaviour
{

    public GameObject Black1; 
    public GameObject Black2; //Two variable for the two hands//

    private float timerhands = 0;

    private Animator animBlack1;
    private Animator animBlack2;
    // Start is called before the first frame update
    void Start()
    {
        //Black1.gameObject.SetActive(false);
        //Black2.gameObject.SetActive(false); //Make the two hands doesn't appear at start of the game//

        animBlack1 = Black1.GetComponent<Animator>();
        animBlack2 = Black2.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            //Black1.gameObject.SetActive(true);
            //Black2.gameObject.SetActive(true); //Two hands appear 'til e on keyboard is pressed// 

            animBlack1.Play("Covered");
            animBlack2.Play("CoveringLeft");

            TestDetection.handsOnscreen = true;
            TestDetection.ennemiOnscreen = false;
            timerhands += Time.deltaTime;
            
            if (timerhands >= 1f)
            {
                // Incrémente actualtimer jusqu'à 3 de manière progressive
                if (TestDetection.actualtimer < 3)
                {
                    TestDetection.actualtimer += Time.deltaTime;
                    TestDetection.chromaticAberration.intensity.value -= 0.34f * Time.deltaTime;
                    if (TestDetection.actualtimer > 3) 
                    {
                        TestDetection.actualtimer = 3; // S'assure qu'il ne dépasse pas 3
                    }
                }
            }
        }
        if (Input.GetKeyUp("space"))
        {
            //Black1.gameObject.SetActive(false);
            //Black2.gameObject.SetActive(false); //Two Hands disappear when e is unpressed

            animBlack1.Play("UncoveringRight");
            animBlack2.Play("UncoveringLeft");

            TestDetection.handsOnscreen = false;
            timerhands = 0;
        }
    }
}
