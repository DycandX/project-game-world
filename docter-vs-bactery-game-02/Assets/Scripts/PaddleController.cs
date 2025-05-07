using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float batasAtas;
    public float batasBawah;
    public float batasKiri;
    public float batasKanan;
    public float kecepatan;
    public string axisX;
    public string axisY;

    public float kecepatanBooster = 10f;
    public float durasiBooster = 5f;

    private float kecepatanSaatIni;
    private float boosterTimer = 0f;
    private bool sedangBooster = false;

    void Start()
    {
        kecepatanSaatIni = kecepatan;
    }

    void Update()
    {
        // Booster countdown
        if (sedangBooster)
        {
            boosterTimer -= Time.deltaTime;
            if (boosterTimer <= 0f)
            {
                kecepatanSaatIni = kecepatan;
                sedangBooster = false;
            }
        }

        float gerakX = Input.GetAxis(axisX) * kecepatanSaatIni * Time.deltaTime;
        float gerakY = Input.GetAxis(axisY) * kecepatanSaatIni * Time.deltaTime;
        Vector3 nextPos = transform.position + new Vector3(gerakX, gerakY, 0);

        // Cek batas-batas
        if (nextPos.y > batasAtas)
            gerakY = 0;
        if (nextPos.y < batasBawah)
            gerakY = 0;
        if (nextPos.x < batasKiri)
            gerakX = 0;
        if (nextPos.x > batasKanan)
            gerakX = 0;

        transform.Translate(gerakX, gerakY, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Booster"))
        {
            kecepatanSaatIni = kecepatanBooster;
            sedangBooster = true;
            boosterTimer = durasiBooster;
            Destroy(other.gameObject); // hilangkan booster setelah disentuh
        }
    }
}
