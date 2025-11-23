using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointController : MonoBehaviour
{
    public string cpName;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_cp")) 
        {
            if (PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "_cp") == cpName)
            {
                PlayeController.instance.GetComponent<CharacterController>().enabled = false;
                PlayeController.instance.transform.position = transform.position;//teleport player to check point position
                PlayeController.instance.transform.rotation = transform.rotation;
                PlayeController.instance.GetComponent<CharacterController>().enabled = true;
                Debug.Log("Player starts at = " + cpName);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", cpName);
            Debug.Log("Player hit = " + cpName);
        }
    }
}
