using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static int Scored;
    public int startScore = 0;
    private void Start()
    {
        Scored = startScore;
    }
    public TextMeshProUGUI score;


    private void Update()
    {
        score.text = Scored.ToString();
    }
}
