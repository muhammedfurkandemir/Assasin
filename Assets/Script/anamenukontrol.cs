using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class anamenukontrol : MonoBehaviour
{
    public GameObject EminmisinPnel;
    void Start()
    {
        
    }
    public void OyunaBasla()
    {
        SceneManager.LoadScene(1);
    }
    public void cıkıs()
    {
        EminmisinPnel.SetActive(true);
        
    }
    public void cikisCevap(string cevap)
    {
        switch (cevap)
        {
            case "Evet":
                Debug.Log("Çıktın");
                Application.Quit();
                break;
            case "Hayir":
                EminmisinPnel.SetActive(false);
                break;
        }
    }
   
}
