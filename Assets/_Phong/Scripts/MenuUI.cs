using _Dat;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField]
    private WaveSystem waveSystem;
    public void Play()
    {
        gameObject.SetActive(false);
        UIManager.Instance.gameplayUI.gameObject.SetActive(true);
        waveSystem.StartWave();
    }

    public void Quit()
    {
        Application.Quit();
    }
}