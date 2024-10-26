using UnityEngine;

public class MirrorController : MonoBehaviour
{
    public PlayerController pc;
    public ReflectionController rc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnDisable()
    {
        rc.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // is in mirror view
        if(pc.transform.position.y < (transform.position.y + 1.1) && pc.transform.position.y > (transform.position.y - 1.1))
        {
            rc.gameObject.SetActive(true);
            rc.transform.position = new Vector3((-pc.transform.position.x + transform.position.x + transform.position.x), pc.transform.position.y);
            rc.UpdateSpriteAlpha(0.75f - 0.7f * Mathf.Abs(pc.transform.position.y - transform.position.y));
        }
        else
        {
            rc.gameObject.SetActive(false);
        }
    }
}
