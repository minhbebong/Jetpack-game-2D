using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TextManager : MonoBehaviour,IDataPersistence
{
    public TextMeshProUGUI textcoin;
    public int score = 0;
    private void Awake()
    {
        textcoin = this.GetComponent<TextMeshProUGUI>();
    }

        public void IncreaseScore()
    {
        score = score + 1;
        textcoin.text = score.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadData(GameData data)
    {
        this.score = data.deathCount;
    }

    public void SaveData(GameData data)
    {
        data.deathCount = this.score;
    }
    public void Update()
    {
        textcoin.text = "" + score;
    }
    public void ResetScore()
    {
        score = 0;
        textcoin.text = score.ToString();
    }
}
