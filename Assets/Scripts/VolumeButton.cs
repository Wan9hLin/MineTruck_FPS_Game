using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{
    public Sprite volumeOnImage;
    public Sprite volumeOffImage;
    public Button volumeButton;

    public void ButtonClicked()
    {
        if (GameManager.instance.isMute)
        {
            volumeButton.image.sprite = volumeOffImage;
        }
        else
        {
            volumeButton.image.sprite = volumeOnImage;
        }
    }
}
