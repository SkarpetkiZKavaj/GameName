using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupController : MonoBehaviour
{
    private void Start()
    {
        Managers.Mission.GoToNext();
    }
}
