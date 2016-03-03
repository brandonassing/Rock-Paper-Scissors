using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//Controls scene changes 
public class Menu : MonoBehaviour {

	//Loads game
    public void PlayGame()
    {
        SceneManager.LoadScene("Scene_0");
    }

    //Loads menu
    public void OpenMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    //Closes application
    public void QuitGame()
    {
        Application.Quit();
    }
}
