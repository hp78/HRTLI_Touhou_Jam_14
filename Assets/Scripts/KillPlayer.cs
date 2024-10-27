using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            StartCoroutine(DeathSequence());
            AudioManager.instance.PlaySFX("Player Death");
        }       
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(0.2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
