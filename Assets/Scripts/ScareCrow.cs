using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareCrow : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        ScareCrowManager.instance.AddScareCrow(this);
    }

    void Update()
    {
       
    }
    public void GetAnimation()
    {
        anim.SetTrigger("Attacked");
    }
}
