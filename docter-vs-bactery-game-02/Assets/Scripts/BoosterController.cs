using UnityEngine;

public class BoosterController : MonoBehaviour
{
    public float speedBoost = 5f;
    public float duration = 3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            PaddleController paddle = other.GetComponent<PaddleController>();
            if (paddle != null)
            {
                StartCoroutine(BoostSpeed(paddle));
            }
            Destroy(gameObject); // hilangkan booster setelah disentuh
        }
    }

    private System.Collections.IEnumerator BoostSpeed(PaddleController paddle)
    {
        float originalSpeed = paddle.kecepatan;
        paddle.kecepatan += speedBoost;
        yield return new WaitForSeconds(duration);
        paddle.kecepatan = originalSpeed;
    }
}
