using UnityEngine;
using System.Collections.Generic;

public class SwitchStepOn : MonoBehaviour
{
    public List<GameObject> activateOnTriggerList = new List<GameObject>();
    public List<GameObject> deactivateOnTriggerList = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Reflection"))
        {
            AudioManager.instance.PlaySFX("Button Click");
            TriggerEffect(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Reflection"))
        {
            TriggerEffect(false);
        }
    }

    void TriggerEffect(bool isTriggered)
    {
        foreach (GameObject go in activateOnTriggerList)
            go.SetActive(isTriggered);

        foreach (GameObject go in deactivateOnTriggerList)
            go.SetActive(!isTriggered);
    }
}
