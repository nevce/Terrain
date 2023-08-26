using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float hareketHizi = 5f;

    void Update()
    {
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");

        Vector3 hareket = new Vector3(yatay, 0f, dikey) * hareketHizi * Time.deltaTime;
        transform.Translate(hareket);
    }
}
