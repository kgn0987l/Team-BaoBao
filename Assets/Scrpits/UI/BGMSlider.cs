using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class BGMSlider : MonoBehaviour
{
    public AudioMixer audioMixer; // AudioMixer 참조
    public Slider slider;         // 슬라이더 참조

    void Start()
    {
        // AudioMixer에서 현재 볼륨 값 가져오기
        float currentVolume;
        if (audioMixer.GetFloat("BGMVolume", out currentVolume))
        {
            slider.value = currentVolume; // 슬라이더 값 동기화
        }
        else
        {
            slider.value = -20f; // 기본값 설정
        }
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGMVolume", volume);
    }
}
