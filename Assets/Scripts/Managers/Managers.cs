using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PersonManager))]
[RequireComponent(typeof(MissionManager))]
public class Managers : MonoBehaviour
{
    private List<IGameManager> startSequence;
    public static MissionManager Mission { get; private set; }
    public static PersonManager Persones { get; private set; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Mission = GetComponent<MissionManager>();
        Persones = GetComponent<PersonManager>();
        startSequence = new List<IGameManager>();
        startSequence.Add(Mission);
        startSequence.Add(Persones);
        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        foreach (IGameManager manager in startSequence)
            manager.Startup();

        yield return null;

        int numModules = startSequence.Count;
        int numReady = 0;
        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;
            foreach (IGameManager manager in startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }


            if (numReady > lastReady)
                Debug.Log("Progress: " + numReady + "/" + numModules);

            yield return null;
        }

        Debug.Log("All managers started up");
    }
  
}
