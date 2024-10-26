using UnityEngine;
using System.Collections.Generic;

public class GroupTrigger : MonoBehaviour
{
    public bool _isTriggeredOnce = true;
    bool _isTriggered = false;
    public List<GameObject> triggers = new List<GameObject>();

    public List<GameObject> activateOnTriggerList = new List<GameObject>();
    public List<GameObject> deactivateOnTriggerList = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTriggeredOnce && _isTriggered)
            return;

        bool isAllTriggered = true;
        foreach(GameObject go in triggers)
        {
            if (!go.activeInHierarchy)
                isAllTriggered = false;
        }

        if(_isTriggeredOnce && isAllTriggered)
        {
            TriggerEffect(true);
            _isTriggered = true;
        }
        else
        {
            TriggerEffect(isAllTriggered);
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
