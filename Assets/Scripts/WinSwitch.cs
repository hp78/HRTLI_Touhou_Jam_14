using UnityEngine;
using System.Collections.Generic;

public class WinSwitch : MonoBehaviour
{
    bool isTriggered = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("DelayAddButtonCount", 0.1f);
    }

    void DelayAddButtonCount()
    {
        GameController.instance.AddWinButtonCount();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Reflection"))
        {
            AudioManager.instance.PlaySFX("Button Click");
            Collected();
        }
    }

    void Collected()
    {
        if (isTriggered)
            return;

        isTriggered = true;
        GameController.instance.TickDownWinButton();
        gameObject.SetActive(false);
    }
}
