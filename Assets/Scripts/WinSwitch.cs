using UnityEngine;
using System.Collections.Generic;

public class WinSwitch : MonoBehaviour
{
    bool isTriggered = false;

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
            NextLevel();
        }
    }

    void NextLevel()
    {
        if (isTriggered)
            return;

        isTriggered = true;
        GameController.instance.LoadNextLevel();
    }
}
