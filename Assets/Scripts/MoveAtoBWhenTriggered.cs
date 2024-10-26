using UnityEngine;

public class MoveAtoBWhenTriggered : MonoBehaviour
{
    public GameObject triggerRef;

    public Transform movingObject;
    public Transform pointA;
    public Transform pointB;

    public float moveSpeed = 1f;
    public float currDelta = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(triggerRef.activeInHierarchy)
        {
            currDelta = Mathf.Clamp(moveSpeed * Time.deltaTime + currDelta, 0, 1);
        }
        else
        {
            currDelta = Mathf.Clamp(-moveSpeed * Time.deltaTime + currDelta, 0, 1);
        }

        movingObject.position = Vector3.Lerp(pointA.position, pointB.position, currDelta);
    }


}
