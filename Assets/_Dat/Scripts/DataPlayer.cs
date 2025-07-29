using System;
using System.Collections.Generic;

[Serializable]
public class DataPlayer
{
    public string name;
    public int maxWave;
    public int gold;
    public int currentWave;
    public List<string> dataEquip = new List<string>();

    public bool isNewGame = true;
}