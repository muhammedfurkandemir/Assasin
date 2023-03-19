using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ByFurkanDemir
{
    public class Animasyon
    {
        private float MaxSpeedClass;
        private float MaxInputXClass;

        public float YonuDisarıCikar()
        {
            return MaxInputXClass;
        }

        public void Sol_Hareket(Animator anim,string AnaParametreAdi,string KontrolParametresi
            ,List<float> ParametreDegerleri)
        {
            if (Input.GetKey(KeyCode.A))
            {
                anim.SetBool(KontrolParametresi, true);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    anim.SetFloat(AnaParametreAdi, ParametreDegerleri[1]);   //sol kosma parametresini tutar
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    anim.SetFloat(AnaParametreAdi,ParametreDegerleri[2] );   //sol_ileri parametresini tutar
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    anim.SetFloat(AnaParametreAdi, ParametreDegerleri[3]);  //sol_geri parametresini tutar
                }
                else
                {
                    anim.SetFloat(AnaParametreAdi, ParametreDegerleri[0]);  //sol yurume parametresini tutar
                }
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetFloat(AnaParametreAdi, 0f);
                anim.SetBool(KontrolParametresi, false);
            }
        }

        public void Sag_Hareket(Animator anim, string AnaParametreAdi, string KontrolParametresi
            , List<float> ParametreDegerleri)
        {
            if (Input.GetKey(KeyCode.D))
            {
                anim.SetBool(KontrolParametresi, true);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    anim.SetFloat(AnaParametreAdi, ParametreDegerleri[1]);   //sag kosma parametresini tutar
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    anim.SetFloat(AnaParametreAdi, ParametreDegerleri[2]);   //sag_ileri parametresini tutar
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    anim.SetFloat(AnaParametreAdi, ParametreDegerleri[3]);  //sag_geri parametresini tutar
                }
                else
                {
                    anim.SetFloat(AnaParametreAdi, ParametreDegerleri[0]);  //sag yurume parametresini tutar
                }
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetFloat(AnaParametreAdi, 0f);
                anim.SetBool(KontrolParametresi, false);
            }
        }

        public void Geri_Hareket(Animator anim, string AnaParametreAdi)
        {
            if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool(AnaParametreAdi, true);
            }
            else
            {
                anim.SetBool(AnaParametreAdi, false);
            }
        }

        public void Egilme_Hareket(Animator anim, string AnaParametreAdi
            , List<float> ParametreDegerleri)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    anim.SetFloat(AnaParametreAdi, ParametreDegerleri[1]);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    anim.SetFloat(AnaParametreAdi, ParametreDegerleri[2]);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    anim.SetFloat(AnaParametreAdi, ParametreDegerleri[3]);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    anim.SetFloat(AnaParametreAdi, ParametreDegerleri[4]);
                }
                else
                {
                    anim.SetFloat(AnaParametreAdi, ParametreDegerleri[0]);
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                anim.SetFloat(AnaParametreAdi, 0f);
            }
        }

        public void Karakter_Hareket(Animator anim,string hizdegeri,float maksimumUzunluk, float TamHiz, float YurumeHizi)
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                MaxSpeedClass = TamHiz;
                MaxInputXClass = 1;

            }
            else if (Input.GetKey(KeyCode.W))
            {
                MaxSpeedClass = YurumeHizi;
                MaxInputXClass = 1;

            }
            else
            {
                MaxSpeedClass = 0f;
                MaxInputXClass = 0;

            }

            anim.SetFloat("Speed", Vector3.ClampMagnitude(new Vector3(MaxInputXClass, 0, 0), MaxSpeedClass).magnitude, maksimumUzunluk, Time.deltaTime * 10);
        }

        public void Karakter_Rotation(Camera mainCam,float rotationSpeed,GameObject Karakter)
        {
            Vector3 CamOfSet = mainCam.transform.forward;
            CamOfSet.y = 0;
            Karakter.transform.forward = Vector3.Slerp(Karakter.transform.forward, CamOfSet, Time.deltaTime * rotationSpeed);
        }

        public List<float> ParametreOlustur(float[] degerler)
        {
            List<float> Yon_Parametreleri = new List<float>();
            foreach ( float item  in degerler)
            {
                Yon_Parametreleri.Add(item);
            }
            return Yon_Parametreleri;
        }
    }
    
}
