
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Dusman : MonoBehaviour
{
    [Header("DİGER AYARLAR")]
    NavMeshAgent NavMesh;
    Animator Animatorum;
    public GameObject Hedef;
    public GameObject Anahedef;

    [Header("GENEL AYARLAR")]
    float AtesEtmeMenzilDeger = 7;
    float SuphelenmeMenzilDeger = 10;
    Vector3 BaslangıcNoktasi;
    bool Suphelenme = false;
    bool AtesEdiliyormu = false;
    public GameObject AtesEtmeNoktasi;
    

    [Header("DEVRİYE AYARLARI")]
    public GameObject[] Devriye_Noktalari_1;
    public GameObject[] Devriye_Noktalari_2;
    public GameObject[] Devriye_Noktalari_3;

    [Header("SİLAH AYARLAR")]
    float AtesEtmeSikligi_1;
    public float AtesEtmeSikliği_2;
    public float menzil;

    public float DarbeGucu ;


    [Header("SİLAH SESLER")]
    public AudioSource[] Sesler;
    [Header("SİLAH EFEKTLER")]
    public ParticleSystem[] Efektler;

    GameObject[] AktifOlanNoktaListeleri;

    bool DevriyeVarMi;
    Coroutine DevriyeAt;
    Coroutine DevriyeZaman;
    bool DevriyeKilit=true;
    public bool DevriyeAtabılirMi;
    float Saglik;


   
   
    void Start()
    {
        NavMesh = GetComponent<NavMeshAgent>();
        Animatorum = GetComponent<Animator>();
        BaslangıcNoktasi = transform.position;
        Saglik = 50;
    }



    GameObject[]  DevriyeKontrol()
    {
        int Deger = Random.Range(1, 3);
        switch (Deger)
        {
            case 1:
                AktifOlanNoktaListeleri = Devriye_Noktalari_1;
                break;
            case 2:
                AktifOlanNoktaListeleri = Devriye_Noktalari_2;
                break;
            case 3:
                AktifOlanNoktaListeleri = Devriye_Noktalari_3;
                break;
        }
        return AktifOlanNoktaListeleri;
    }

    IEnumerator DevriyeZamanKontrol()
    {
        while (true && !DevriyeVarMi && DevriyeAtabılirMi)
        {
            
            
                yield return new WaitForSeconds(5f);
                DevriyeKilit = true;
                StopCoroutine(DevriyeZaman);
           
            
        }
    }

    IEnumerator DevriyeTeknikIslem(GameObject[] GelenObjeler)
    {
        NavMesh.isStopped = false;
        DevriyeKilit = false;
        DevriyeVarMi = true;
        Animatorum.SetBool("Yuru", true);
        int ToplamNokta = GelenObjeler.Length-1;
        int BaslangıcDeger = 0;
        NavMesh.SetDestination(GelenObjeler[BaslangıcDeger].transform.position);
        while (true && DevriyeAtabılirMi)
        {
            if (Vector3.Distance (transform.position, GelenObjeler[BaslangıcDeger].transform.position)<=1f)
            {
                if (ToplamNokta>BaslangıcDeger)
                {
                    ++BaslangıcDeger;
                    NavMesh.SetDestination(GelenObjeler[BaslangıcDeger].transform.position);
                }
                else
                {
                    NavMesh.stoppingDistance = 1;
                    NavMesh.SetDestination(BaslangıcNoktasi);
                    
                }
                
            }
            else
            {
                if (ToplamNokta > BaslangıcDeger)
                {
                    NavMesh.SetDestination(GelenObjeler[BaslangıcDeger].transform.position);
                }
                
            }
            yield return null;

        }
    }
    
    void AtesEtmeMenzil()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, AtesEtmeMenzilDeger);   

        foreach (var objeler in hitColliders)
        {
            if (objeler.gameObject.CompareTag("Player"))
            {
                AtesEt(objeler.gameObject);
            }
            else
            {
                if (AtesEdiliyormu)
                {
                    Animatorum.SetBool("ates_et", false);
                    NavMesh.isStopped = false;
                    Animatorum.SetBool("Yuru", true);
                    AtesEdiliyormu = false;
                }
                
            }
        }
    }
    void AtesEt(GameObject Hedef)
    {
        AtesEdiliyormu = true;

        Vector3 AradakiFark = Hedef.gameObject.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(AradakiFark, Vector3.up);
        transform.rotation = rotation;      
        Animatorum.SetBool("Yuru", false);
        NavMesh.isStopped = true;
        Animatorum.SetBool("ates_et", true);

        RaycastHit Hit;

        if (Physics.Raycast(AtesEtmeNoktasi.transform.position, AtesEtmeNoktasi.transform.forward, out Hit, menzil))
        {
            Color color = Color.blue;
            Vector3 DegisenPozisyon = new Vector3(Hedef.transform.position.x, Hedef.transform.position.y + 2.1f, Hedef.transform.position.z);
            Debug.DrawLine(AtesEtmeNoktasi.transform.position, DegisenPozisyon, color);

            if (Time.time > AtesEtmeSikligi_1)
            {
                if (Hit.transform.gameObject.CompareTag("Player"))
                {
                    Hit.transform.gameObject.GetComponent<KarakterControl>().SaglikDurumu(DarbeGucu);

                    Instantiate(Efektler[1], Hit.point, Quaternion.LookRotation(Hit.normal));
                }
                else
                {
                    Instantiate(Efektler[2], Hit.point, Quaternion.LookRotation(Hit.normal));
                }
 
               

                if (!Sesler[0].isPlaying)
                {
                    Sesler[0].Play();
                    Efektler[0].Play();
                }
                AtesEtmeSikligi_1 = Time.time + AtesEtmeSikliği_2;

            }

            
           
        }

        //ates etme teknik islemleri
    }
   void SuphelenmeMenzil()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, SuphelenmeMenzilDeger);

        foreach (var objeler in hitColliders)
        {
            if (objeler.gameObject.CompareTag("Player"))
            {
                if (Animatorum.GetBool("kosma"))
                {
                    Animatorum.SetBool("kosma", false);
                    Animatorum.SetBool("Yuru", true);
                }
                else
                {
                    Animatorum.SetBool("Yuru", true);
                }
                
                Hedef = objeler.gameObject;                
                NavMesh.SetDestination(Hedef.transform.position);
                Suphelenme = true;
                if (DevriyeAtabılirMi)
                {
                    StopCoroutine(DevriyeAt);
                }
                
            }
            else
            {
                if (Suphelenme)
                {
                    Hedef = null;
                   

                    if (transform.position != BaslangıcNoktasi)
                    {
                        NavMesh.stoppingDistance = 1;
                        NavMesh.SetDestination(BaslangıcNoktasi);
                        if (NavMesh.remainingDistance <= 1)
                        {
                            Animatorum.SetBool("Yuru", false);
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                        }
                    }
                    Suphelenme = false;
                    if (DevriyeAtabılirMi)
                    {
                        DevriyeAt = StartCoroutine(DevriyeTeknikIslem(DevriyeKontrol()));
                    }
                    
                }

            }
        }
    }

    public void SaglikDurumu(float Darbegucu)
    {
        Saglik -= Darbegucu;
        if (!Suphelenme)
        {
            Animatorum.SetBool("kosma", true);
            NavMesh.SetDestination(Anahedef.transform.position);
        }
        
        if (Saglik <= 0)
        {
            Animatorum.Play("olme");
            Destroy(gameObject,5f);
        }
          
    }
    private void LateUpdate()
    {

        if (NavMesh.stoppingDistance==1 && NavMesh.remainingDistance <= 1)
        {
            Animatorum.SetBool("Yuru", false);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (DevriyeAtabılirMi)
            {
                DevriyeVarMi = false;
                DevriyeZaman = StartCoroutine(DevriyeZamanKontrol());
                StopCoroutine(DevriyeAt);
            }
            DevriyeVarMi = false;
          /*DevriyeZaman = StartCoroutine(DevriyeZamanKontrol());
            StopCoroutine(DevriyeAt);*/
            NavMesh.stoppingDistance = 0;
            NavMesh.isStopped = true;
        }


        if (DevriyeKilit && DevriyeAtabılirMi)
        {
            DevriyeAt = StartCoroutine(DevriyeTeknikIslem(DevriyeKontrol())); 
        }

       SuphelenmeMenzil();
       AtesEtmeMenzil();


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AtesEtmeMenzilDeger);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, SuphelenmeMenzilDeger);
    }
    
}
