using UnityEngine;

public class AIPaddleController : MonoBehaviour
{
    public enum TipeAI { Easy, Normal, Hard }
    public TipeAI tipeAI = TipeAI.Easy;

    public Transform bola;
    public float kecepatan;
    public float batasAtas;
    public float batasBawah;
    public float batasKiri;
    public float batasKanan;
    public string boosterTag = "Booster";

    void Update()
    {
        switch (tipeAI)
        {
            case TipeAI.Easy:
                GerakVertikal();
                break;
            case TipeAI.Normal:
                Gerak2D();
                break;
            case TipeAI.Hard:
                GerakPintar();
                break;
        }
    }

    void GerakVertikal()
    {
        if (bola == null) return;

        float arahY = bola.position.y > transform.position.y ? 1 : -1;
        float gerakY = arahY * kecepatan * Time.deltaTime;

        Vector3 nextPos = transform.position + new Vector3(0, gerakY, 0);
        if (nextPos.y > batasAtas || nextPos.y < batasBawah) return;

        transform.position = nextPos;
    }

    void Gerak2D()
    {
        if (bola == null) return;

        Vector3 arah = (bola.position - transform.position).normalized;
        Vector3 gerak = arah * kecepatan * Time.deltaTime;

        Vector3 nextPos = transform.position + gerak;
        if (nextPos.y > batasAtas || nextPos.y < batasBawah) gerak.y = 0;
        if (nextPos.x > batasKanan || nextPos.x < batasKiri) gerak.x = 0;

        transform.position += gerak;
    }

    void GerakPintar()
    {
        if (bola == null) return;

        // Cek jika ada booster
        GameObject booster = GameObject.FindGameObjectWithTag(boosterTag);
        Vector3 target;

        if (booster != null && Vector3.Distance(transform.position, booster.transform.position) < 5f)
        {
            // Kejar booster jika dekat
            target = booster.transform.position;
        }
        else
        {
            // Prediksi posisi bola ke depan
            Rigidbody2D rb = bola.GetComponent<Rigidbody2D>();
            Vector3 prediksi = bola.position + (Vector3)(rb.linearVelocity.normalized * 2f);
            target = prediksi;
        }

        Vector3 arah = (target - transform.position).normalized;
        Vector3 gerak = arah * kecepatan * Time.deltaTime;

        Vector3 nextPos = transform.position + gerak;
        if (nextPos.y > batasAtas || nextPos.y < batasBawah) gerak.y = 0;
        if (nextPos.x > batasKanan || nextPos.x < batasKiri) gerak.x = 0;

        transform.position += gerak;
    }
}
