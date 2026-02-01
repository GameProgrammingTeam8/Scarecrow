using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitOpened : MonoBehaviour
{
    public int whichDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (whichDoor == 0)
            {
                ScareCrowManager.instance.isWin = true;
                SceneManager.LoadScene("ResultMode");
            }
            else if (whichDoor == 1)
            {
                SceneManager.LoadScene("PlayMode");
            }
        }
    }
}