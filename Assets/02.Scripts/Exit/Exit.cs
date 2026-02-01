using UnityEngine;

public class Exit : MonoBehaviour
{
    public void OpenExit()
    {
        Instantiate(GameObject.Find("ExitOpened"), gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}