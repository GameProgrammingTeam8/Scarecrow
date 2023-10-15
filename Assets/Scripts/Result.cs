using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Result : MonoBehaviour
{
    TextMeshProUGUI resultboard;
    private void Awake()
    {
        resultboard = this.GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        resultboard.SetText("Total Enemy: " + EnemyManager.instance.genEnemy + "\n" + "Kill: " + EnemyManager.instance.destroyedEnemy);
    }
}
