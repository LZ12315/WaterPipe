using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Data : MonoBehaviour
{

    public int score=0;
    public Text Scoretext;
    public int money=0;
    public Text MoneyText;
    private void Start()
    {
        Scoretext.text = "分数："+score;
        MoneyText.text = "金钱：" + money;
    }
    private void Update()
    {
        Scoretext.text = "分数：" + score;
        MoneyText.text = "金钱：" + money;
    }
}
