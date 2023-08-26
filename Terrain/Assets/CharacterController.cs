using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    CharacterController controller;                       //Karakter kontrolc�s� bile�enine eri�mek i�in bir referans tan�mlar.

    public float movementSpeed = 1f;                      //Karakterin hareket h�z�n� ayarlamak i�in bir de�i�ken tan�mlar.
    public float rotationSpeed = 10f;                     //Karakterin d�n�� h�z�n� ayarlamak i�in bir de�i�ken tan�mlar.

    Animator anim;                                        //Karakterin animasyon kontrolc�s�ne eri�mek i�in bir referans tan�mlar.
    public Transform cam; // Kamera transformunu tutmak i�in bir de�i�ken tan�mlad�k



    private void Start()
    {
        anim = GetComponent<Animator>();                 //Script'in ba�l� oldu�u oyun nesnesinin Animator bile�enine eri�ir ve anim de�i�kenine atar.
        controller = GetComponent<CharacterController>();// Script'in ba�l� oldu�u oyun nesnesinin CharacterController bile�enine eri�ir ve controller de�i�kenine atar.
        
    }
    private void Update()
    {

        float hzInput = Input.GetAxisRaw("Horizontal"); //"Horizontal" adl� bir giri� ekseni �zerinde kullan�c�n�n yatay (sol-sa�) giri�ini al�r ve hzInput de�i�kenine atar. De�er -1 ile 1 aras�nda olabilir.
        float vInput = Input.GetAxisRaw("Vertical"); //"Vertical" adl� bir giri� ekseni �zerinde kullan�c�n�n dikey (ileri-geri) giri�ini al�r ve vInput de�i�kenine atar. De�er -1 ile 1 aras�nda olabilir.

        if (hzInput != 0 || vInput != 0) //Kullan�c�n�n yatay veya dikey giri�i oldu�unu kontrol eder.
        {
            //anim.SetFloat("Walk", 1f); //Animator bile�eninde "Walk" adl� bir float parametreye, 1 de�erini atayarak y�r�me animasyonunu ba�lat�r.
            ////Girdi bilgisine g�re hedef d�n��� hesaplama
            //float targetRotation = Mathf.Atan2(hzInput, vInput)*Mathf.Rad2Deg; //Kullan�c�n�n giri�ine ba�l� olarak hedef d�n�� a��s�n� hesaplar. Mathf.Atan2() i�levi, yatay ve dikey de�erler aras�ndaki a��y� radyan cinsinden hesaplar ve Mathf.Rad2Deg ile dereceye d�n��t�r�r.

            ////Karakterin d�nd��� y�ne yumu�at�lm�� ge�i� sa�lama
            //Quaternion targetQuaternion = Quaternion.Euler(0, targetRotation, 0); //Hedef d�n�� a��s�n� kullanarak bir hedef Quaternion olu�turur. Euler a��lar� kullanarak bir Quaternion olu�tururken, yaln�zca y d�n���n� (targetRotation) kullan�r.
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, rotationSpeed * Time.deltaTime); //Karakterin d�n���n� yumu�at�lm�� bir ge�i�le hedef d�n��e do�ru yapar. Quaternion.Slerp() i�levi, mevcut d�n�� (transform.rotation) ile hedef d�n�� (targetQuaternion) aras�nda yumu�ak bir ge�i� sa�lar. rotationSpeed * Time.deltaTime de�eri, d�n�� h�z�n� zamanla �arparak d���k FPS durumlar�nda da d�zg�n bir ge�i� sa�lar.
            ////Hedef d�n���ne g�re y�n hesaplama
            //Vector3 moveDirection = targetQuaternion * Vector3.forward; // Hedef d�n��e g�re bir hareket y�n� hesaplar. Vector3.forward, (0, 0, 1) vekt�r�n� temsil eder ve yerel z eksenine do�ru bir hareketi ifade eder. Hedef d�n�� ile bu vekt�r� �arparak, karakterin d�n�� y�n�nde hareket etmesini sa�lar.

            //controller.Move(moveDirection*movementSpeed*Time.deltaTime); //Karakter kontrolc�s�n� kullanarak karakteri belirtilen y�nde hareket ettirir. moveDirection vekt�r�n� movementSpeed ile �arparak hareket h�z�n� belirler ve Time.deltaTime ile zaman �l�e�ine g�re d�zenler.
            anim.SetFloat("Walk", 1f);

            // Kamera y�n�n� alarak karakterin d�n���n� hesapla
            Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;// Scale, iki vekt�r�n elemanlar�n� �arpan bir i�lem yapar.
            Vector3 moveDirection = (vInput * camForward + hzInput * cam.right).normalized;

            // Karakteri y�r�t
            controller.Move(moveDirection * movementSpeed * Time.deltaTime);

            // Karakterin y�n�n� kameraya d�nd�r
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            anim.SetFloat("Walk", 0); // Kullan�c�n�n giri�i yoksa, "Walk" adl� float parametreye 0 de�erini atayarak y�r�me animasyonunu durdurur.
        }

        //Quaternion = D�rt bile�ene sahiptir : (x, y, z, w). �lk �� bile�en (x, y, z) bir vekt�r� temsil ederken, d�rd�nc� bile�en (w) ise d�nd�rme i�leminin b�y�kl���n� ve y�nlendirilmesini belirler. 
        //Quaternion.Slerp = �ki quaternion aras�nda yumu�ak bir ge�i� sa�lar. �ki nokta aras�nda d�z bir �izgi yerine, bir yay �zerinde yumu�ak bir ge�i� ger�ekle�tirir.
       
    }


}
