using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBattleMove : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        Vector3 newPosition = new Vector3(transform.position.x + deltaX, transform.position.y, transform.position.z);
        gameObject.transform.position = newPosition;
    }
}
