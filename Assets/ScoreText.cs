using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreText : MonoBehaviour
{
    //表示スコア
    private int score_;

    //テキスト本体
    private TMP_Text scoreText_;
    private void Start() {
        score_ = 0;
        scoreText_ = GetComponent<TMP_Text>();
    }
    //スコアを更新とテキストへの適用
    public void SetScore(int score) { 
        score_ = score;
        UpdateScoreText();
    }

    //テキストの更新
    private void UpdateScoreText() {
        //数値は8桁9詰め
        scoreText_.text = $"SCRE:[score_:0000000]";
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
