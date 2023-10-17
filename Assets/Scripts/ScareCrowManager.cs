using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ScareCrowManager : MonoBehaviour
{
    public static ScareCrowManager instance;

    public List<ScareCrow> scarecrows;
    public int genScareCrow = 0;
    public int destroyedScareCrow = 0;
    public UnityEvent onChanged;
    public UnityEvent onOpen;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Duplicated ScareCrowManager, ignoring this one", gameObject);
        }
    }

    public void AddScareCrow(ScareCrow scarecrow)
    {
        scarecrows.Add(scarecrow);
        genScareCrow += 1;
        onChanged.Invoke();
    }

    public void RemoveScareCrow(ScareCrow scarecrow)
    {
        scarecrows.Remove(scarecrow);
        destroyedScareCrow += 1;
        onChanged.Invoke();
        if (destroyedScareCrow>=genScareCrow)
        {
            onOpen.Invoke();
        }
    }
}
