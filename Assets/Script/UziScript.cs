using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UziScript : MonoBehaviour
{
    [Header("AYARLAR")]
    float AtesEtmeSikligi_1;
    public float AtesEtmeSikliği_2;
    public float menzil;
    int ToplamMermiSayisi=640;
    int JarjorKapasitesi=32;
    int KalanMermi;
    float DarbeGucu=10f;
    public TextMeshProUGUI ToplamMermi_text;
    public TextMeshProUGUI KalanMermi_text;

    [Header("SESLER")]
    public AudioSource[] Sesler;
    [Header("EFEKTLER")]
    public ParticleSystem[] Efektler;
    [Header("GENELİSLEMLER")]
    public Camera BenimKameram;
    public Animator KarakterinAnimatoru;
    



    void Start()
    {
        KalanMermi = JarjorKapasitesi;
        ToplamMermi_text.text = ToplamMermiSayisi.ToString();
        KalanMermi_text.text = JarjorKapasitesi.ToString();
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            ReloadKontrol();

        }
        if (KarakterinAnimatoru.GetBool("reload"))
        {
            ReloadIslemiTeknikFonksiyonu();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Time.time>AtesEtmeSikligi_1 && KalanMermi!=0)
            {
                AtesEt();
                AtesEtmeSikligi_1 = Time.time + AtesEtmeSikliği_2;
            }
            if (KalanMermi==0)
            {
                Sesler[1].Play();
            }
            
        }  
    }
    void AtesEt()
    {
        KalanMermi--;
        KalanMermi_text.text = KalanMermi.ToString();
        Efektler[0].Play();
        Sesler[0].Play();
        KarakterinAnimatoru.Play("Egilme_ates_etme");
        RaycastHit Hit;
        if (Physics.Raycast(BenimKameram.transform.position,BenimKameram.transform.forward,out Hit,menzil))
        {
            if (Hit.transform.gameObject.CompareTag("Dusman"))
            {
                Hit.transform.gameObject.GetComponent<Dusman>().SaglikDurumu(DarbeGucu);

                Instantiate(Efektler[2], Hit.point, Quaternion.LookRotation(Hit.normal));
            }
            else
            {
                Instantiate(Efektler[1], Hit.point, Quaternion.LookRotation(Hit.normal));
            }                 
        }
    }
    
    void ReloadKontrol()
    {
        if (KalanMermi < JarjorKapasitesi && ToplamMermiSayisi != 0)    
        {
            KarakterinAnimatoru.Play("jarjordegistir");
            if (!Sesler[2].isPlaying)
                Sesler[2].Play();
        }
        
    }
    void ReloadIslemiTeknikFonksiyonu()
    {
        if (KalanMermi == 0)
        {
            if (ToplamMermiSayisi <= JarjorKapasitesi)
            {

                KalanMermi = ToplamMermiSayisi;
                ToplamMermiSayisi = 0;
            }
            else
            {
                ToplamMermiSayisi -= JarjorKapasitesi;
                KalanMermi = JarjorKapasitesi;
            }



        }
        else
        {
            if (ToplamMermiSayisi <= JarjorKapasitesi)
            {
                int OlusanDeger = JarjorKapasitesi + KalanMermi;
                if (OlusanDeger > JarjorKapasitesi)
                {
                    KalanMermi = JarjorKapasitesi;
                    ToplamMermiSayisi = OlusanDeger - JarjorKapasitesi;
                }
                else
                {
                    KalanMermi += ToplamMermiSayisi;
                    ToplamMermiSayisi = 0;
                }


            }
            else
            {
                int MevcutMermi = JarjorKapasitesi - KalanMermi;
                ToplamMermiSayisi -= MevcutMermi;
                KalanMermi = JarjorKapasitesi;
            }

        }
        ToplamMermi_text.text = ToplamMermiSayisi.ToString();
        KalanMermi_text.text = JarjorKapasitesi.ToString();
        KarakterinAnimatoru.SetBool("reload", false);
    }
        

}
