using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI perfectText;
    [SerializeField] private TextMeshProUGUI greatText;
    [SerializeField] private TextMeshProUGUI badText;
    [SerializeField] private TextMeshProUGUI missText;
    private void OnEnable()
    {
        scoreText.text = GManager.Instance.score.ToString();
        perfectText.text = GManager.Instance.perfect.ToString();
        greatText.text = GManager.Instance.great.ToString();
        badText.text = GManager.Instance.bad.ToString();
        missText.text = GManager.Instance.miss.ToString();
    }

    public void Retry()
    {
        // Init
        GManager.Instance.perfect = 0;
        GManager.Instance.great = 0;
        GManager.Instance.bad = 0;
        GManager.Instance.miss = 0;
        GManager.Instance.maxScore = 0;
        GManager.Instance.ratioScore = 0;
        GManager.Instance.score = 0;
        GManager.Instance.combo = 0;
        SceneManager.LoadScene("MusicScene");
    }
}