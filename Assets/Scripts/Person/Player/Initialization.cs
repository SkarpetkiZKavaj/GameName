using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    void Start()
    {
        Managers.Persones.AddPerson(gameObject.GetInstanceID());
    }
}
