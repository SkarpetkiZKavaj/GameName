using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float deltaX = 0, deltaY = 0;
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue value)
    {
        deltaX = value.Get<float>() * Constatnts.horizontalSpeed;
    }

    private void OnClimb(InputValue value)
    {
        Managers.Persones.IsOnStairs(gameObject.GetInstanceID(), true);

        deltaY = value.Get<float>() * Constatnts.verticalSpeed;
    }

    private void FixedUpdate()
    {
        Vector2 movement;
        if (body.gravityScale == 0)
            movement = new Vector2(deltaX, deltaY);
        else
            movement = new Vector2(deltaX, body.velocity.y);

        body.velocity = movement;

        if (!Mathf.Approximately(deltaX, 0))
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / 2, 0.5f, 1);
    }
}