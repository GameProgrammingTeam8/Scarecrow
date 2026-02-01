using System.Collections;
using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    private TextMeshProUGUI _resultboard;

    public TextMeshProUGUI resultTxt;
    
    private void Start()
    {
        _resultboard = GetComponent<TextMeshProUGUI>();
        _resultboard.SetText(
            "Total Enemy: " +
            EnemyManager.instance.genEnemy +
            "\n" + "Kill: " +
            EnemyManager.instance.destroyedEnemy
        );
        
        if (ScareCrowManager.instance.isWin == true)
        {
            StartCoroutine(SetWin());
        }
    }

    private IEnumerator SetWin()
    {
        resultTxt.SetText("You Win");
        yield return new WaitForSeconds(1);
        GameObject.Find("Player").GetComponent<Player>().Victory();
    }
}