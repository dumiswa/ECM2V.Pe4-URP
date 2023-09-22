using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioSource mainMenuSong;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Options()
    {
        SceneManager.LoadScene("Controls");
    }

    public void Quit()  
    {
        Application.Quit();
    }
}
