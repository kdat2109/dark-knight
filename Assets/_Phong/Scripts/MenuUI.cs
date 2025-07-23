using System.Collections;
using _Dat;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    
    [SerializeField]
    private WaveSystem waveSystem;
    public void Play()
    {
        gameObject.SetActive(false);
        if (GameManager.Instance.Profile.isNewGame)
        {
            GameManager.Instance.PlayIntro(InitLevel);
            GameManager.Instance.Profile.isNewGame = false;
        }
        else
        {
            InitLevel();
        }
        
    }

    void InitLevel()
    {
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