using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    GameObject targetObject;
    private Vector3 startingPos;
    private Vector3 finalPos;
    void Start()
    {
        startingPos = new Vector3(0, 6.5f, -10);
        finalPos = new Vector3(30, 6.5f, -10);
    }


    void Update()
    {
        Vector3 currentPos = Vector3.zero;
        currentPos.x = Mathf.Clamp(targetObject.transform.position.x, startingPos.x, finalPos.x);
        currentPos.y = 6.5f;
        currentPos.z = -10f;
        transform.position = currentPos;
    }
}
