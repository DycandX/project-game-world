using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float totalTime = 120f; // 2 menit
    private float sisaWaktu;

    public Text timerText; // drag teks UI ke sini lewat Inspector
    public GameObject panelSelesai; // panel yang muncul saat waktu habis

    private bool waktuHabis = false;

    private BallController ballController;

    void Start()
    {
        sisaWaktu = totalTime;

        if (panelSelesai != null)
        {
            panelSelesai.SetActive(false);
        }

        ballController = GameObject.FindObjectOfType<BallController>();
    }

    void Update()
    {
        if (BallController.permainanSelesai || waktuHabis)
            return;

        sisaWaktu -= Time.deltaTime;

        if (sisaWaktu <= 0)
        {
            sisaWaktu = 0;
            waktuHabis = true;
            timerText.text = "00:00";

            if (ballController != null)
            {
                ballController.CekPemenangKarenaWaktuHabis();
            }

            Debug.Log("Waktu Habis!");
        }
        else
        {
            int menit = Mathf.FloorToInt(sisaWaktu / 60);
            int detik = Mathf.FloorToInt(sisaWaktu % 60);
            timerText.text = menit.ToString("00") + ":" + detik.ToString("00");
        }
    }

}
