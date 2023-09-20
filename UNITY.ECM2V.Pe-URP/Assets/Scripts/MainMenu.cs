using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioSource mainMenuSong;

    void Awake()
    {
        //mainMenuSong. = true; 
    }
    public void Play()
    {
        SceneManager.LoadScene("Prototype");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void Quit()  
    {
        Application.Quit();
    }
}
