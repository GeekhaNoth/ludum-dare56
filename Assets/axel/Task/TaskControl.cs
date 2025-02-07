using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskControl : MonoBehaviour
{
    private bool DetectedCollision = false;
    private bool BoolNotOnHands = false;
    private GameObject ItemOnHand;

    private GameObject ItemCollided;
    public GameObject Hand;
    public GameObject Item;
    public GameObject toybox;
    public GameObject bedSlot;
    public GameObject pillowSlot;

    private List<GameObject> four;

    private string actualSlot = "";

    private bool chicken = false;
    private bool plush = false;
    private bool toy = false;
    private bool pillow = false;
    private bool garbage = false;
    private int numbertoy = 2;

    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Image image5;
    public Sprite check;
    private Sprite uncheck;


    public GameObject plushie;
    public GameObject pillowGO;
    public Material newMaterial;


    public static bool variableDeBouring = false;
    // Start is called before the first frame update
    void Start()
    {
        four = new List<GameObject>();
        four.AddRange(GameObject.FindGameObjectsWithTag("chicken"));
        four.AddRange(GameObject.FindGameObjectsWithTag("plush"));
        four.AddRange(GameObject.FindGameObjectsWithTag("toy"));
        four.AddRange(GameObject.FindGameObjectsWithTag("pillow"));
        four.AddRange(GameObject.FindGameObjectsWithTag("garbage"));

        image1.sprite = uncheck;
        image2.sprite = uncheck;
        image3.sprite = uncheck;
        image4.sprite = uncheck;
        image5.sprite = uncheck;
    }

    // Update is called once per frame
    void Update()
    {
        // If an object is near and no object is hold, and the "e" is pressed then hold the near object
        if (!BoolNotOnHands && DetectedCollision && Input.GetKeyDown(KeyCode.E))
        {
            BoolNotOnHands = true;
            ItemOnHand = ItemCollided;
            ItemOnHand.GetComponent<Rigidbody>().isKinematic = true;
            ItemOnHand.transform.position = Hand.transform.position;
            ItemOnHand.transform.parent = Hand.transform;
        }

        // If an object is hold and "e" is pressed then drop the held object (or if the eyes is closed)
        else if (Input.GetKeyDown(KeyCode.Space) && BoolNotOnHands)
        {
            BoolNotOnHands = false;
            ItemOnHand.GetComponent<Rigidbody>().isKinematic = false;
            ItemOnHand.transform.parent = Item.transform;
            ItemOnHand = null;
        }
        else if (BoolNotOnHands && Input.GetKeyDown(KeyCode.E))
        {
            if (actualSlot == "hoven" && ItemOnHand.tag == "chicken")
            {
                chicken = true;
                Destroy(ItemOnHand);
                Debug.Log("Poulet d�truite");
                BoolNotOnHands = false;
                ItemOnHand = null;
                image1.sprite = check;
            }
            else if (actualSlot == "bed" && ItemOnHand.tag == "plush")
            {
                plush = true;
                ItemOnHand.transform.position = bedSlot.transform.position;
                ItemOnHand.GetComponent<Rigidbody>().isKinematic = false;
                ItemOnHand.tag = "Untagged";
                ItemOnHand.transform.parent = bedSlot.transform;
                Debug.Log("Peluche posée");
                BoolNotOnHands = false;
                ItemOnHand = null;
                image2.sprite = check;

                plushie.GetComponent<Renderer>().material = newMaterial;
            }
            else if (actualSlot == "toybox" && ItemOnHand.tag == "toy")
            {
                ItemOnHand.GetComponent<Rigidbody>().isKinematic = false;
                ItemOnHand.GetComponent<MeshCollider>().enabled = false;
                ItemOnHand.transform.position = toybox.transform.position;
                Debug.Log("Jouet détruit");
                ItemOnHand.transform.parent = toybox.transform;
                BoolNotOnHands = false;
                ItemOnHand = null;
                numbertoy--;
                if (numbertoy == 0)
                {
                    image3.sprite = check;
                    Debug.Log("Les 2 jouets sont dans la boite");
                    toy = true;
                }
            }
            else if (actualSlot == "sofa" && ItemOnHand.tag == "pillow")
            {
                pillow = true;
                ItemOnHand.transform.position = pillowSlot.transform.position;
                ItemOnHand.GetComponent<Rigidbody>().isKinematic = false;
                ItemOnHand.transform.parent = pillowSlot.transform;
                ItemOnHand.tag = "Untagged";
                Debug.Log("Coussin posé");
                BoolNotOnHands = false;
                ItemOnHand = null;
                image4.sprite = check;


                pillowGO.GetComponent<Renderer>().material = newMaterial;
            }
            else if (actualSlot == "trashcan" && ItemOnHand.tag == "garbage")
            {
                garbage = true;
                Destroy(ItemOnHand);
                Debug.Log("Poubelle d�truite");
                BoolNotOnHands = false;
                ItemOnHand = null;
                image5.sprite = check;
            }
            else
            {
                BoolNotOnHands = false;
                ItemOnHand.GetComponent<Rigidbody>().isKinematic = false;
                ItemOnHand.transform.parent = Item.transform;
                ItemOnHand = null;
            }
        }

        // Victory condition
        if (chicken && plush && toy && pillow && garbage)
        {
            SceneManager.LoadScene("Victory");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "chicken" || other.gameObject.tag == "plush" || other.gameObject.tag == "toy" || other.gameObject.tag == "pillow" || other.gameObject.tag == "garbage")
        {
            ItemCollided = other.gameObject;
            DetectedCollision = true;
        }

        if (other.gameObject.tag == "hoven" || other.gameObject.tag == "bed" || other.gameObject.tag == "toybox" || other.gameObject.tag == "sofa" || other.gameObject.tag == "trashcan")
        {
            actualSlot = other.gameObject.tag;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "chicken" || other.gameObject.tag == "plush" || other.gameObject.tag == "toy" || other.gameObject.tag == "pillow" || other.gameObject.tag == "garbage")
        {
            DetectedCollision = false;
        }
        if (other.gameObject.tag == "hoven" || other.gameObject.tag == "bed" || other.gameObject.tag == "toybox" || other.gameObject.tag == "sofa" || other.gameObject.tag == "trashcan")
        {
            actualSlot = "";
        }
    }
}