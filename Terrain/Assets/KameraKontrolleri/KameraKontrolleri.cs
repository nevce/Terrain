using UnityEngine;

public class KameraKontrolleri : MonoBehaviour
{
    public Transform takipEdilecekNesne;
    public Vector3 mesafe = new Vector3(0f, 2f, -5f);
    public float yumusamaHizi = 5f;

    void LateUpdate()
    {
        if (takipEdilecekNesne == null)
            return;

        Vector3 hedefPozisyon = takipEdilecekNesne.position + mesafe;
        transform.position = Vector3.Lerp(transform.position, hedefPozisyon, yumusamaHizi * Time.deltaTime);
    }
}
