using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 引入 TextMeshPro 的命名空間

public class CoinUI : MonoBehaviour
{
    public int startCoinQuantity;
    public TMP_Text coinQuantity; // 使用 TextMeshPro 的 Text 元素

    public static int CurrentCoinQuantity;

    // Start is called before the first frame update
    void Start()
    {
        CurrentCoinQuantity = startCoinQuantity;
    }

    // Update is called once per frame
    void Update()
    {
        coinQuantity.text = CurrentCoinQuantity.ToString();
    }
}
