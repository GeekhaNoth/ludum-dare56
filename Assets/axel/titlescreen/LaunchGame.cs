using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LaunchGame : MonoBehaviour
{
    public GameObject tuto;
    public GameObject mainMenu;
    public GameObject fade;
    
    // Start is called before the first frame update
    void Start()
    {
        tuto.gameObject.SetActive(false);
        mainMenu.SetActive(true);
        fade.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchGameOnClick()
    {
        Debug.Log("LaunchGameOnClick");
        //SceneManager.LoadScene("PlaceHolderScene"); //Load the game when Play button is clicked

         StartCoroutine(ActivateTutoAndChangeScene());
    }

    IEnumerator ActivateTutoAndChangeScene()
    {
        tuto.SetActive(true);
        mainMenu.SetActive(false);

        yield return new WaitForSeconds(2f);

        fade.SetActive(true);

        yield return new WaitForSeconds(1f);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void CloseTheGame()
    {
        Debug.Log("CloseTheGame");
        Application.Quit(); //Close the game when Quit button is clicked
    }

    public void RetunMenu()
    {
        SceneManager.LoadScene("Menus");
    }
}
