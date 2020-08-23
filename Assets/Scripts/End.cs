using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
     // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))     //Pääsee takaisin main menuun esciä painamalla
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
