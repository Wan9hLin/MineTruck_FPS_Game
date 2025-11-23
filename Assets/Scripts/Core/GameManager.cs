using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // public int enemiesAlive = 0;
    // public int wave = 0;
    public int counter;
    public bool ending;
    // public GameObject[] spawnPoints;
    // public GameObject enemyPrefab;
    //public TextMeshProUGUI waveNum;

    public GameObject pauseMenu;
    public GameObject volumePanel;
    public bool isMute = false;

    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject endGamePanel;
    public GameObject[] instructionPanel;
    private int buildIndex;

    public GameObject FadeTransition;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        FadeTransition.SetActive(true);
        buildIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(InstructionPanel());
    }

    void Update()
    {
        // if (enemiesAlive == 0)
        // {
        //     wave++;
        //     NextWave(wave);
        //     //waveNum.text = "Wave: " + wave.ToString();
        // }

        if (Input.GetKeyDown(KeyCode.Escape) && !(winPanel.activeInHierarchy || losePanel.activeInHierarchy))
        {
            if (pauseMenu.activeInHierarchy)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // public void NextWave(int round)
    // {
    //     for (int i = 0; i < round; i++)
    //     {
    //         GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
    //
    //         GameObject enemySpawned = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
    //         //enemySpawned.GetComponent<MonsterController>().gameManager = GetComponent<GameManager>();
    //         enemiesAlive++;
    //     }
    // }

    public void NextLevel()
    {
        FadeTransition.GetComponent<Animator>().SetTrigger("FadeIn");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReplayLevel()
    {
        FadeTransition.GetComponent<Animator>().SetTrigger("FadeIn");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        //wave = 0;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1;
        FadeTransition.GetComponent<Animator>().SetTrigger("FadeIn");
        Invoke("LoadMainMenuScene", .4f);
    }

    void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        volumePanel.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        AudioListener.volume = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);

        if (!instructionPanel[buildIndex - 1].activeInHierarchy)
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            AudioListener.volume = 1;
        }
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        }

    public void Quit()
    {
        Debug.Log("Game Quitted Successfully");
        Application.Quit();
    }

    public void VolumePanel()
    {
        pauseMenu.SetActive(false);
        volumePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ChangeVolume()
    {
        AudioListener.volume = UIManager.instance.volumeSlider.value;
    }

    public void Mute()
    {
        isMute = !isMute;
        AudioListener.volume = isMute ? 0 : UIManager.instance.volumeSlider.value;
    }

    public void WinPanel()
    {
        Cursor.lockState = CursorLockMode.None;
        winPanel.SetActive(true);
        winPanel.GetComponentInChildren<Animator>().SetTrigger("FadeIn");
    }

    public void LosePanel()
    {
        Cursor.lockState = CursorLockMode.None;
        losePanel.SetActive(true);
        losePanel.GetComponentInChildren<Animator>().SetTrigger("FadeIn");
    }

    public void EndGamePanel()
    {
        Cursor.lockState = CursorLockMode.None;
        endGamePanel.SetActive(true);
        endGamePanel.GetComponentInChildren<Animator>().SetTrigger("FadeIn");
    }

    public IEnumerator InstructionPanel()
    {
        instructionPanel[buildIndex - 1].SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        //Time.timeScale = 0f;
        yield return new WaitForSeconds(1.0f);
    }

    public void CloseInstruction()
    {
        instructionPanel[buildIndex - 1].SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        AudioListener.volume = 1;
    }
}

