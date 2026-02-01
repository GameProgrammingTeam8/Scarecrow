using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultExit : MonoBehaviour
{
    public int whichDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (whichDoor==0)
            {
                SceneManager.LoadScene("PlayMode");
            }
            else if (whichDoor==1)
            {
                SceneManager.LoadScene("TutorialMode");
            }
            else if(whichDoor==2)
            {
                //UnityEditor.EditorApplication.isPlaying=false;
                Application.Quit();
            }
        }
    }
}