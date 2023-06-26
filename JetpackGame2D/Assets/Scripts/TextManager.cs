using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI textcoin;
    public int score = 0;

    public void IncreaseScore()
    {
        score = score + 1;
        textcoin.text = score.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
