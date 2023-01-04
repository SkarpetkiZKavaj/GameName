using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseClickMovement : MonoBehaviour
{
    private Vector2 finalPosition;
    private Vector2 nextPos;
    private Rigidbody2D body;
    private BoxCollider2D coll;
    private Stack<Vector2> path;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        finalPosition = body.position;
        nextPos = body.position;
    }

    private void OnClickMove()
    {
        finalPosition = GetFinalPosition();
        path = NavigationGraph.GetPath(body.position, finalPosition);
    }

    private void Update()
    {

        if (path != null)
        {
            float XWay = nextPos.x - body.position.x, YWay = nextPos.y - body.position.y;

            if (Mathf.Abs(XWay) > 0.1)
            {
                Vector2 pos = new Vector2(body.position.x + Mathf.Sign(XWay) * Constatnts.clickDisplacement * Time.deltaTime, body.position.y);
                body.MovePosition(pos);
            }
            else if (Mathf.Abs(YWay) > 0.1)
            {
                Managers.Persones.IsOnStairs(gameObject.GetInstanceID(), true);
                Vector2 pos = new Vector2(body.position.x, body.position.y + Mathf.Sign(YWay) * Constatnts.clickDisplacement * Time.deltaTime);
                body.MovePosition(pos);
            }
            else
            {
                body.MovePosition(nextPos);
                if (path.Count > 0)
                    nextPos = path.Pop();
            }
        }
    }
    private Vector2 GetFinalPosition()
    {
        Vector2 ClickPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(ClickPoint, Vector2.down);

        if (hit.collider != null)
            return new Vector2(hit.point.x, hit.point.y + coll.size.y * coll.transform.localScale.y / 2);
        else
            return transform.position;
    }

}
