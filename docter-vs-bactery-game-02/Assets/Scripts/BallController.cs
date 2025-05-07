using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public int force;
    Rigidbody2D rigid;

    public static int scoreP1;
    public static int scoreP2;

    Text scoreUIP1;
    Text scoreUIP2;

    public GameObject panelselesai;
    public Text txtPemenang;

    AudioSource audio;
    public AudioClip hitSound;

    public static bool permainanSelesai = false;

    // Use this for initialization
    void Start()
    {   

        rigid = GetComponent<Rigidbody2D>();
        Vector2 arah = new Vector2(2, 0).normalized;
        rigid.AddForce(arah * force);

        scoreP1 = 0;
        scoreP2 = 0;
        
        scoreUIP1 = GameObject.Find("Score1").GetComponent<Text> ();
        scoreUIP2 = GameObject.Find("Score2").GetComponent<Text> ();

        panelselesai = GameObject.Find("PanelSelesai");
        panelselesai.SetActive(false);

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
{
    float minX = 2f;
    if (Mathf.Abs(rigid.linearVelocity.x) < minX)
    {
        float arahX = rigid.linearVelocity.x >= 0 ? 1 : -1;
        rigid.linearVelocity = new Vector2(arahX * minX, rigid.linearVelocity.y);
    }
}


    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Pemukul" || coll.gameObject.name == "Pemukul2")
        {
            float sudut = (transform.position.y - coll.transform.position.y) * 5f;
            float arahX = coll.gameObject.name == "Pemukul" || coll.gameObject.name == "Pemukul2" ? 1 : -1;

            Vector2 arah = new Vector2(arahX, sudut).normalized;
            rigid.linearVelocity = Vector2.zero;
            rigid.AddForce(arah * force * 2);
        }

        audio.PlayOneShot(hitSound);
            if (coll.gameObject.name == "TepiKanan")
        {
            scoreP1 += 1;
            TampilkanScore();
            if (scoreP1 == 5)
            {
                txtPemenang.text = "Player Biru Menang!";
                panelselesai.SetActive(true);
                GameObject.Find("Timer Text").GetComponent<Text>().text = "00:00";
                BallController.permainanSelesai = true;
                Destroy(gameObject);
                return;
            }
            ResetBall();
            Vector2 arah = new Vector2(2, 0).normalized;
            rigid.AddForce(arah * force);
        }
        if (coll.gameObject.name == "TepiKiri")
        {
            scoreP2 += 1;
            TampilkanScore();
            if (scoreP2 == 5)
            {
                txtPemenang.text = "Player Biru Menang!";
                panelselesai.SetActive(true);
                GameObject.Find("Timer Text").GetComponent<Text>().text = "00:00";
                BallController.permainanSelesai = true;
                Destroy(gameObject);
                return;
            }
            ResetBall();
            Vector2 arah = new Vector2(-2, 0).normalized;
            rigid.AddForce(arah * force);
        }
        if (coll.gameObject.name == "Pemukul" || coll.gameObject.name == "Pemukul2")
        {
            float sudut = (transform.position.y - coll.transform.position.y) * 5f;
            float arahX = coll.gameObject.name == "Pemukul" || coll.gameObject.name == "Pemukul2" ? 1 : -1;
            Vector2 arah = new Vector2(arahX, sudut).normalized;

            rigid.linearVelocity = new Vector2(0, 0);
            rigid.AddForce(arah * force * 2);
        }
    }

    public void CekPemenangKarenaWaktuHabis()
    {
        if (panelselesai == null || txtPemenang == null)
        {
        Debug.LogError("PanelSelesai atau txtPemenang belum di-assign di Inspector!");
        return;
        }


        panelselesai.SetActive(true);

        if (scoreP1 > scoreP2)
        {
            txtPemenang.text = "Player Merah Menang!";
        }
        else if (scoreP2 > scoreP1)
        {
            txtPemenang.text = "Player Biru Menang!";
        }
        else
        {
            txtPemenang.text = "Seri!";
        }

        Destroy(gameObject); // stop bola saat waktu habis
    }



    void ResetBall()
    {
        transform.localPosition = new Vector2(0, 0);
        rigid.linearVelocity = new Vector2(0, 0);
    }

    void TampilkanScore()
    {
        Debug.Log("Score P1: " + scoreP1 + "Score P2: " + scoreP2);
        scoreUIP1.text = scoreP1 + "";
        scoreUIP2.text = scoreP2 + "";
    }

}
