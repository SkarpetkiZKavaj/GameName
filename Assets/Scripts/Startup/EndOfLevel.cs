using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Managers.Mission.GoToNext();
    }
}
