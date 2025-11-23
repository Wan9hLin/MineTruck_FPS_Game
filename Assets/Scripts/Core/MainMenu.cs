using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject volumePanel;
    public Slider volumeSlider;
    public bool isMute = false;
    public Sprite volumeOnImage;
    public Sprite volumeOffImage;
    public Button volumeButton;
    public GameObject FadeTransition;
    public GameObject[] backgrounds;

    private void Start()
    {
        FadeTransition.SetActive(true);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(false);
        }

        StartCoroutine(SlideShow());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSettings();
        }
    }

    public void StartGame()
    {
        FadeTransition.GetComponent<Animator>().SetTrigger("FadeIn");
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Debug.Log("Game Quitted Successfully");
        Application.Quit();
    }

    public IEnumerator SlideShow()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            FadeTransition.SetActive(true);
            backgrounds[i].SetActive(true);
            
            if (i > 0)
            {
                backgrounds[i - 1].SetActive(false);
            }

            yield return new WaitForSeconds(2.5f);
            FadeTransition.GetComponent<RawImage>().texture = backgrounds[i].GetComponent<RawImage>().texture;
            FadeTransition.GetComponent<Animator>().SetTrigger("FadeIn");
            yield return new WaitForSeconds(1.0f);

            if (i == backgrounds.Length - 1)
            {
                backgrounds[i].SetActive(false);
                i = -1;
            }

            FadeTransition.SetActive(false);
        }
    }
    public void VolumePanel()
    {
        volumePanel.SetActive(true);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumePanel.GetComponentInChildren<Slider>().value;
    }

    public void Mute()
    {
        isMute = !isMute;
        AudioListener.volume = isMute ? 0 : volumeSlider.value;
    }

    public void ButtonClicked()
    {
        if (isMute)
        {
            volumeButton.image.sprite = volumeOffImage;
        }
        else
        {
            volumeButton.image.sprite = volumeOnImage;
        }
    }

    public void CloseSettings()
    {
        volumePanel.SetActive(false);
    }
}
