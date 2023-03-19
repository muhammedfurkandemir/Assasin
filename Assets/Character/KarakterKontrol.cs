using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterKontrol : MonoBehaviour
{
    float inputX;
    float inputY;
    Animator anim;
    Vector3 mevcutYon;
    Camera mainCam;
    float maksimumUzunluk=1;
    float rotationSpeed=10;
    float maxSpeed;



    void Start()
    {
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    
    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.RightShift))
        {
            maxSpeed = 1f;
            inputX = 1;
            
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            maxSpeed = .2f;
            inputX = 1;
            
        }
        else
        {
            maxSpeed = 0f;
            inputX = 0;
            
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetBool("solayuru", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetBool("solayuru", false);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetBool("sagayuru", true);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetBool("sagayuru", false);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetBool("geriyuru", true);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            anim.SetBool("geriyuru", false);
        }

        mevcutYon = new Vector3(inputX, 0, inputY);

        inputMove();
        inputRotation();
    }
    void inputMove()
    {
       
        anim.SetFloat("Speed", Vector3.ClampMagnitude(mevcutYon,maxSpeed).magnitude,maksimumUzunluk,Time.deltaTime*10);

    }
    void inputRotation()
    {
        Vector3 CamOfSet = mainCam.transform.forward;
        CamOfSet.y = 0;
        transform.forward = Vector3.Slerp(transform.forward, CamOfSet, Time.deltaTime * rotationSpeed);
    }
}
