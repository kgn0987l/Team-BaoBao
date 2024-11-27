using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class BGMSlider : MonoBehaviour
{
    public AudioMixer audioMixer; // AudioMixer ����
    public Slider slider;         // �����̴� ����

    void Start()
    {
        // AudioMixer���� ���� ���� �� ��������
        float currentVolume;
        if (audioMixer.GetFloat("BGMVolume", out currentVolume))
        {
            slider.value = currentVolume; // �����̴� �� ����ȭ
        }
        else
        {
            slider.value = -20f; // �⺻�� ����
        }
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGMVolume", volume);
    }
}
