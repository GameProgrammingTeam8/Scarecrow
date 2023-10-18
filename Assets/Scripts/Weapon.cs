using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public float damage;
    AudioSource aus;
    Player player;
    public AudioClip slice;
    public TextMeshProUGUI scarecrowTxt;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        aus = GetComponent<AudioSource>();
        scarecrowTxt.SetText(ScareCrowManager.instance.genScareCrow +"");
        aus.clip = slice;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (player.isAttack == true || player.isSkill == true)
        {
            aus.PlayOneShot(aus.clip);
            HP hp = other.GetComponent<HP>();

            if (hp != null)
            {
                
                hp.amount -= damage;
                if (hp.amount <= 0)
                {
                    hp.hitCount++;
                    if (hp.hitCount == 1)
                    {
                        if(other.tag=="Enemy")
                        {
                            Enemy enemy = other.GetComponent<Enemy>();
                            enemy.enabled = false;
                            EnemyManager.instance.RemoveEnemy(enemy);
                        }
                        else if(other.tag=="ScareCrow")
                        {
                            ScareCrow scarecrow = other.GetComponent<ScareCrow>();
                            scarecrow.GetAnimation();
                            scarecrow.enabled = false;
                            ScareCrowManager.instance.RemoveScareCrow(scarecrow);
                            scarecrowTxt.SetText((ScareCrowManager.instance.genScareCrow - ScareCrowManager.instance.destroyedScareCrow) + "");
                        }
                        Destroy(other.gameObject, 2);
                    }
                }
            }
        }
    }
}
