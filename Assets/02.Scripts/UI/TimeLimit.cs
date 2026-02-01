using UnityEngine;
using TMPro;

public class TimeLimit : MonoBehaviour
{
    private int _min;
    private float _sec;
    private float _setTime = 300;

    [SerializeField] private TMP_Text gameTimeUI;

    private void Update()
    {
        _setTime -= Time.deltaTime;

        if (_setTime > 0)
        {
            _min = (int)_setTime / 60;
            _sec = _setTime % 60;
            gameTimeUI.text = string.Format("{0:D2}:{1:D2}", _min, (int)_sec);
        }

        if (_setTime <= 0)
        {
            gameTimeUI.text = "00:00";
            GameObject.Find("Player").GetComponent<Player>().TakeDamage();
            return;
        }
    }
}