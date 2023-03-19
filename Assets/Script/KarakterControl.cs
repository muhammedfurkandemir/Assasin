using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByFurkanDemir;
using UnityEngine.UI;

public class KarakterControl : MonoBehaviour
{
    float inputX;
    public Transform karakter;
    Animator anim;
    Vector3 mevcutYon;
    Camera mainCam;
    float maksimumUzunluk=1;
    float rotationSpeed=10;
    float maxSpeed;

    Animasyon animasyon = new Animasyon();
    public Image Healthbar;

    public static float Saglik;
    public GameObject GameManager;

    float[] Sol_Yon_Parametreleri = { .12F, .34F, .63F, .92F };
    float[] Sag_Yon_Parametreleri = { 0.12f, 0.34f, 0.63f, 0.92f };
    float[] Egilme_Yon_Parametreleri = { 0.2f, 0.35f, 0.40f, 0.45f, 1f };

    void Start()
    {        
        anim = GetComponent<Animator>();
        mainCam = Camera.main;

        Saglik = 100;
    }
    public void SaglikDurumu(float Darbegucu)
    {
        Saglik -= Darbegucu;
        Healthbar.fillAmount = Saglik / 100;
        Debug.Log(Saglik);
        if (Saglik <= 0)
        {
            GameManager.GetComponent<GameManager>().Kaybettin();
            
        }
            
    }


    void LateUpdate()
    {
        animasyon.Sol_Hareket(anim, "Sol_hareket", "Sol_aktifmi", animasyon.ParametreOlustur(Sol_Yon_Parametreleri));
        animasyon.Sag_Hareket(anim, "Sag_hareket", "Sag_aktifmi", animasyon.ParametreOlustur(Sag_Yon_Parametreleri));
        animasyon.Geri_Hareket(anim, "geriyuru");
        animasyon.Egilme_Hareket(anim, "Egilme_hareket", animasyon.ParametreOlustur(Egilme_Yon_Parametreleri));
        animasyon.Karakter_Hareket(anim, "Speed", maksimumUzunluk, 1 , .2f);
        animasyon.Karakter_Rotation(mainCam, rotationSpeed,gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Oyunsonu"))
        {
            GameManager.GetComponent<GameManager>().Kazandin();
            Debug.Log("bitti");
        }
    }


}
