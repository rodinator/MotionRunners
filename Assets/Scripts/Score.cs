using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Score : MonoBehaviour
{

    Player player;
    Text text;
    int score;
    public float scoreMod = .1f;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score = Mathf.RoundToInt(player.transform.position.z * scoreMod);
        text.text = score.ToString();
    }
}
