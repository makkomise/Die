using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Play()  //Lataa ekan kentän kun painaa play-nappia
    {
        SceneManager.LoadScene("Level1");   
    }

    public void Quit()  //Poistuu pelistä 
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
