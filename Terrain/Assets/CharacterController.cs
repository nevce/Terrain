using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    CharacterController controller;                       //Karakter kontrolcüsü bileþenine eriþmek için bir referans tanýmlar.

    public float movementSpeed = 1f;                      //Karakterin hareket hýzýný ayarlamak için bir deðiþken tanýmlar.
    public float rotationSpeed = 10f;                     //Karakterin dönüþ hýzýný ayarlamak için bir deðiþken tanýmlar.

    Animator anim;                                        //Karakterin animasyon kontrolcüsüne eriþmek için bir referans tanýmlar.
    public Transform cam; // Kamera transformunu tutmak için bir deðiþken tanýmladýk



    private void Start()
    {
        anim = GetComponent<Animator>();                 //Script'in baðlý olduðu oyun nesnesinin Animator bileþenine eriþir ve anim deðiþkenine atar.
        controller = GetComponent<CharacterController>();// Script'in baðlý olduðu oyun nesnesinin CharacterController bileþenine eriþir ve controller deðiþkenine atar.
        
    }
    private void Update()
    {

        float hzInput = Input.GetAxisRaw("Horizontal"); //"Horizontal" adlý bir giriþ ekseni üzerinde kullanýcýnýn yatay (sol-sað) giriþini alýr ve hzInput deðiþkenine atar. Deðer -1 ile 1 arasýnda olabilir.
        float vInput = Input.GetAxisRaw("Vertical"); //"Vertical" adlý bir giriþ ekseni üzerinde kullanýcýnýn dikey (ileri-geri) giriþini alýr ve vInput deðiþkenine atar. Deðer -1 ile 1 arasýnda olabilir.

        if (hzInput != 0 || vInput != 0) //Kullanýcýnýn yatay veya dikey giriþi olduðunu kontrol eder.
        {
            //anim.SetFloat("Walk", 1f); //Animator bileþeninde "Walk" adlý bir float parametreye, 1 deðerini atayarak yürüme animasyonunu baþlatýr.
            ////Girdi bilgisine göre hedef dönüþü hesaplama
            //float targetRotation = Mathf.Atan2(hzInput, vInput)*Mathf.Rad2Deg; //Kullanýcýnýn giriþine baðlý olarak hedef dönüþ açýsýný hesaplar. Mathf.Atan2() iþlevi, yatay ve dikey deðerler arasýndaki açýyý radyan cinsinden hesaplar ve Mathf.Rad2Deg ile dereceye dönüþtürür.

            ////Karakterin döndüðü yöne yumuþatýlmýþ geçiþ saðlama
            //Quaternion targetQuaternion = Quaternion.Euler(0, targetRotation, 0); //Hedef dönüþ açýsýný kullanarak bir hedef Quaternion oluþturur. Euler açýlarý kullanarak bir Quaternion oluþtururken, yalnýzca y dönüþünü (targetRotation) kullanýr.
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, rotationSpeed * Time.deltaTime); //Karakterin dönüþünü yumuþatýlmýþ bir geçiþle hedef dönüþe doðru yapar. Quaternion.Slerp() iþlevi, mevcut dönüþ (transform.rotation) ile hedef dönüþ (targetQuaternion) arasýnda yumuþak bir geçiþ saðlar. rotationSpeed * Time.deltaTime deðeri, dönüþ hýzýný zamanla çarparak düþük FPS durumlarýnda da düzgün bir geçiþ saðlar.
            ////Hedef dönüþüne göre yön hesaplama
            //Vector3 moveDirection = targetQuaternion * Vector3.forward; // Hedef dönüþe göre bir hareket yönü hesaplar. Vector3.forward, (0, 0, 1) vektörünü temsil eder ve yerel z eksenine doðru bir hareketi ifade eder. Hedef dönüþ ile bu vektörü çarparak, karakterin dönüþ yönünde hareket etmesini saðlar.

            //controller.Move(moveDirection*movementSpeed*Time.deltaTime); //Karakter kontrolcüsünü kullanarak karakteri belirtilen yönde hareket ettirir. moveDirection vektörünü movementSpeed ile çarparak hareket hýzýný belirler ve Time.deltaTime ile zaman ölçeðine göre düzenler.
            anim.SetFloat("Walk", 1f);

            // Kamera yönünü alarak karakterin dönüþünü hesapla
            Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;// Scale, iki vektörün elemanlarýný çarpan bir iþlem yapar.
            Vector3 moveDirection = (vInput * camForward + hzInput * cam.right).normalized;

            // Karakteri yürüt
            controller.Move(moveDirection * movementSpeed * Time.deltaTime);

            // Karakterin yönünü kameraya döndür
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            anim.SetFloat("Walk", 0); // Kullanýcýnýn giriþi yoksa, "Walk" adlý float parametreye 0 deðerini atayarak yürüme animasyonunu durdurur.
        }

        //Quaternion = Dört bileþene sahiptir : (x, y, z, w). Ýlk üç bileþen (x, y, z) bir vektörü temsil ederken, dördüncü bileþen (w) ise döndürme iþleminin büyüklüðünü ve yönlendirilmesini belirler. 
        //Quaternion.Slerp = Ýki quaternion arasýnda yumuþak bir geçiþ saðlar. Ýki nokta arasýnda düz bir çizgi yerine, bir yay üzerinde yumuþak bir geçiþ gerçekleþtirir.
       
    }


}
