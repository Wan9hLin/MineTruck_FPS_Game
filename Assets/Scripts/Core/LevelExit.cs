using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string nextLevel;
    public float waitToEndLevel;


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
        if(other.tag == "Player")
        {
            GameManager.instance.ending = true;
            StartCoroutine(EndLvelCol());
        }
    }

    private IEnumerator EndLvelCol()
    {
        PlayerPrefs.SetString(nextLevel + "_cp", "");//reset all check points to be empty
        PlayerPrefs.SetString("CurrentLevel", nextLevel);//store the info about next level, so that we acan press continue

        yield return new WaitForSeconds(waitToEndLevel);

        SceneManager.LoadScene(nextLevel);
    }
}
