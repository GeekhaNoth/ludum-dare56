using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class TestDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public static float timer = 3;
    public static float actualtimer;
    public static bool ennemiOnscreen = false;
    public static bool handsOnscreen = false;

    public VolumeProfile volumeProfile;
    public static ChromaticAberration chromaticAberration;


    private bool ObjectInFront(Camera camera, GameObject ObjectOnScreen)
    {
        Vector3 directionToTarget = ObjectOnScreen.transform.position - camera.transform.position;
        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, directionToTarget, out hit))
        {
            if (hit.transform.gameObject == ObjectOnScreen)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }
    void Start()
    {
        actualtimer = timer;

        volumeProfile.TryGet(out chromaticAberration);

        chromaticAberration.intensity.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bool ennemy = false;
        Camera camera = Camera.main;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Mob");
        foreach ( GameObject a in taggedObjects)
        {
            if (GeometryUtility.TestPlanesAABB(planes, a.GetComponent<Collider>().bounds))
            {
                if (ObjectInFront(camera, a))
                {
                    ennemy = true;
                }
            }
        }

        if (ennemy == true && handsOnscreen == false)
        {
            ennemiOnscreen = true;
        }
        else
        {
            ennemiOnscreen = false;
        }

        if (ennemiOnscreen == true && handsOnscreen == false)
        {
            actualtimer -= Time.deltaTime;

            chromaticAberration.intensity.value += 0.34f * Time.deltaTime;
        }

        if (actualtimer <= 0)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("GameOver");
        }
        
    }
    
}
