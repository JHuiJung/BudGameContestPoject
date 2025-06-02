using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class AudioController : MonoBehaviour
{
    public Slider AudioSlider;

    public List<PlaylistController> controllers = new List<PlaylistController>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetVolume()
    {
        float vol = AudioSlider.value;
        MasterAudio.PlaylistMasterVolume = vol;
        MasterAudio.SetBusVolumeByName("3D", vol);
    }
}
