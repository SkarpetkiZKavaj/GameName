using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonManager : MonoBehaviour, IGameManager
{
    public delegate void onStairsDelegate(int InstanceID, bool state);
    public event onStairsDelegate onStairsEvent;

    public ManagerStatus status { get; private set; }
    private Dictionary<int , (bool onStairs, int health)> personQuality;

    public void Startup()
    {
        personQuality = new Dictionary<int, (bool onStairs, int health)>();
        status = ManagerStatus.Started;
    }
    public void IsOnStairs(int InstanceID, bool state)
    {
        personQuality[InstanceID] = (state, personQuality[InstanceID].health);
        if (onStairsEvent != null)
            onStairsEvent(InstanceID, personQuality[InstanceID].onStairs);
    }
    public void AddPerson(int InstanceID) =>
        personQuality.Add(InstanceID, (false, 10));    
}
