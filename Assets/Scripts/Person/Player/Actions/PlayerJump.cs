using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D body;
    private BoxCollider2D coll2D;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        coll2D = GetComponent<BoxCollider2D>();
    }

    private void OnJump()
    {
        Vector2 castPosition = new Vector2(body.position.x, body.position.y - 0.55f);
        Vector2 boxSize = new Vector2(0.55f, 0.01f);
        RaycastHit2D hit = Physics2D.BoxCast(castPosition, boxSize, 0f, -Vector2.up);

        if (hit.collider != null && body.gravityScale != 0)
            if (Mathf.Approximately(hit.distance, 0))
                body.AddForce(Vector2.up * Constatnts.jumpForce, ForceMode2D.Impulse);
    }
}
