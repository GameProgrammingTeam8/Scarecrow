using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareCrow : MonoBehaviour
{
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        ScareCrowManager.instance.AddScareCrow(this);
    }

    public void GetAnimation()
    {
        anim.SetTrigger("Attacked");
    }
}
