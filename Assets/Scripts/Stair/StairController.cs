using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairController : MonoBehaviour
{
    [SerializeField]
    private Collider2D platform;
    private Dictionary<int,(Rigidbody2D body, bool onStairs)> bodies = new Dictionary<int, (Rigidbody2D body, bool onStairs)>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bodies.Add(collision.gameObject.GetInstanceID(), (collision.attachedRigidbody, false));
        Managers.Persones.onStairsEvent += IsOnStairs;

    }
 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Person" && bodies[collision.gameObject.GetInstanceID()].onStairs)
        {
            bodies[collision.gameObject.GetInstanceID()].body.gravityScale = 0;
            if (platform != null)
                Physics2D.IgnoreCollision(collision, platform, true);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Person")
        {
            bodies[collision.gameObject.GetInstanceID()].body.gravityScale = 1;
            Managers.Persones.IsOnStairs(collision.gameObject.GetInstanceID(), false);
            if (platform != null)
                Physics2D.IgnoreCollision(collision, platform, false);
        }

        bodies.Remove(collision.gameObject.GetInstanceID());
        Managers.Persones.onStairsEvent -= IsOnStairs;
    }
    public void IsOnStairs(int InstanceID, bool onStairs) =>
        bodies[InstanceID] = (bodies[InstanceID].body, onStairs);
}
