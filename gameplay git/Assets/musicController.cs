using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicController : MonoBehaviour
{
    public Slider _musicSlider;
    public AudioSource musicPlayer;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        musicPlayer.volume = _musicSlider.value;
    }
}
