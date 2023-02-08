using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCountScript : MonoBehaviour
{
    private InfiniteStats stats;
    [SerializeField] private TextMeshProUGUI coinText;
    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<InfiniteStats>();
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = stats.coins.ToString();
    }
}
