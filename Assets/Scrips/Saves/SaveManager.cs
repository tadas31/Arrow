using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { set; get; }
    public SaveState state;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();

        Debug.Log(SaveHelper.Serialize<SaveState>(state));
    }

    // Save the whole state of this saveState script to the player pref
    public void Save()
    {
        PlayerPrefs.SetString("save", SaveHelper.Serialize<SaveState>(state));
    }

    // Load the previous saved state from the player prefs
    public void Load()
    {
        // Was it saved
        if (PlayerPrefs.HasKey("save"))
        {
            state = SaveHelper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
        }
    }

    // Reset save file
    public void ResetSave()
    {
        PlayerPrefs.DeleteKey("save");
    }

    //Changes max score
    public void SetMaxScore(int score)
    {
        state.score = score;
        Save();
    }

    //Returns max score
    public int GetScore()
    {
        return state.score;
    }

    //Adds coins
    public void AddCoin(int coins)
    {
        state.coins = coins;
        Save();
    }

    //Returns coins
    public int GetCoins()
    {
        return state.coins;
    }

    //Sets arrow sprite
    public void SetArrowSprite(Sprite arrowSprite)
    {
        state.arrowSprite = arrowSprite;
        Save();
    }

    //Gets arrow sprites
    public Sprite GetArrowSprite()
    {
        return state.arrowSprite;
    }

}
