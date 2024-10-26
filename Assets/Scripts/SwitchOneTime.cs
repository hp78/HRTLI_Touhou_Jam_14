using System.Collections.Generic;
using UnityEngine;

public class SwitchOneTime : MonoBehaviour
{
    bool _isTriggered = false;

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
        if (_isTriggered)
            return;

        if (collision.CompareTag("Player") || collision.CompareTag("Reflection"))
        {
            TriggerEffect();
        }
    }

    void TriggerEffect()
    {
        if (_isTriggered)
            return;

        _isTriggered = true;

        foreach (GameObject go in activateOnTriggerList)
            go.SetActive(true);

        foreach (GameObject go in deactivateOnTriggerList)
            go.SetActive(false);

        gameObject.SetActive(false);
    }
}
