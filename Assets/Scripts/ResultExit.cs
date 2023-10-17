using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ResultExit : MonoBehaviour
{
    public int whichDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(whichDoor==0)
            {
                SceneManager.LoadScene("PlayMode");
            }else if(whichDoor==1)
            {
                UnityEditor.EditorApplication.isPlaying=false;
                Application.Quit();
            }
        }
    }
}
