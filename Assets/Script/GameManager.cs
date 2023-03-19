using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject WinPanel;
    public GameObject Patron;
    void Start()
    {
        
    }

   public void Kazandin()
    {

        Patron.GetComponent<Animator>().Play("patron_kaybetme");
        Cursor.lockState = CursorLockMode.None;
        WinPanel.SetActive(true);
        

    }
    public void Kaybettin()
    {
        Cursor.lockState = CursorLockMode.None;
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void yenidenoyna()
    {
        Time.timeScale = 1.1f;
        SceneManager.LoadScene(1);
    }
    public void Anamenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
