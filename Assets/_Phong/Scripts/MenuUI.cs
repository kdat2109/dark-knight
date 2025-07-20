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
        // waveSystem.StartWave();

        UIManager.Instance.RestartGame();
        waveSystem.NextWave();
        if (GameManager.Instance.Profile.currentWave > 0)
        {
            waveSystem.ForceEndWave();
        }
        UIManager.Instance.shop.LoadEquipment(GameManager.Instance.Profile.dataEquip);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    
}