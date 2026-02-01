using UnityEngine;
using TMPro;

public class ScarecrowUI : MonoBehaviour
{
    private TextMeshProUGUI _scarecrowUIText;

    private void Start()
    {
        _scarecrowUIText = GetComponent<TextMeshProUGUI>();
        ScareCrowManager.instance.OnChanged.AddListener(UpdateUI);
        _scarecrowUIText.SetText(ScareCrowManager.instance.genScareCrow + "");
    }

    private void UpdateUI()
    {
        _scarecrowUIText.SetText(
            ScareCrowManager.instance.genScareCrow -
            ScareCrowManager.instance.destroyedScareCrow +
            ""
        );
    }

    private void OnDestroy()
    {
        ScareCrowManager.instance.OnChanged.RemoveListener(UpdateUI);
    }
}