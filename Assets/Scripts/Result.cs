using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Result : MonoBehaviour
{
    public TextMeshProUGUI resultTxt;
    TextMeshProUGUI resultboard;
    private void Awake()
    {
        resultboard = this.GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        resultboard.SetText("Total Enemy: " + EnemyManager.instance.genEnemy + "\n" + "Kill: " + EnemyManager.instance.destroyedEnemy);
        if (ScareCrowManager.instance.isWin == true)
        {
            StartCoroutine(SetWin());
        }
    }

    IEnumerator SetWin()
    {
        resultTxt.SetText("You Win");
        yield return new WaitForSeconds(1);
        GameObject.Find("Player").GetComponent<Player>().Victory();
    }
}
