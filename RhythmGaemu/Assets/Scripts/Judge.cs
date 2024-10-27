using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Judge : MonoBehaviour
{
    [SerializeField] private GameObject[] MessageObj;
    [SerializeField] private NotesManager notesManager;
    
    [SerializeField] private TextMeshProUGUI comboText; // ComboNum
    [SerializeField] private TextMeshProUGUI scoreText; // ScoreNum
    [SerializeField] private float missJudgeTime = 0.2f;

    [SerializeField] private GameObject finish;
    
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip hitSound;

    private float endTime = 0;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        endTime = notesManager.NotesTime[^1];
        // Legacy Code - endTime = notesManager.NotesTime[notesManager.NotesTime.Count-1];
    }

    private void Update()
    {
        if (!GManager.Instance.Start) return;
        
        if (Input.GetKeyDown(KeyCode.D)) // 임의의 키가 눌렸을 때
        {
            JudgeOnLine(0);
            // if (notesManager.LaneNum[0] == 0) Legacy Code( Only Check 2Key)
            // {
            //     Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.StartTime)), 0);
            // }
            // else
            // {
            //     if (notesManager.LaneNum[1] == 0)
            //     {
            //         Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.StartTime)), 1);
            //     }
            // }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            JudgeOnLine(1);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            JudgeOnLine(2);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            JudgeOnLine(3);
        }
            
        // End Game -> Result Scene
        if (Time.time > endTime + GManager.Instance.StartTime)
        {
            finish.SetActive(true);
            Invoke(nameof(ResultScene), 3f);
        }

        // 키 입력 처리 이후에도 키가 마지막까지 눌리지 않았을 때 Miss 처리
        if (notesManager.NotesTime.Count > 0 && Time.time > notesManager.NotesTime[0] + missJudgeTime + GManager.Instance.StartTime)
        {
            Message(notesManager.LaneNum[0], 3);
            DeleteData(0);
            Debug.Log("Miss");
            GManager.Instance.miss++;
            GManager.Instance.combo = 0;
        }
    }

    private void JudgeOnLine(int lineNum)
    {
        int nearestNote = -1;
        
        for (int i = 0; i < notesManager.LaneNum.Count; i++)
        {
            if (notesManager.LaneNum[i] == lineNum)
            {
                nearestNote = i;
                break;
            }
        }

        if (nearestNote != -1)
        {
            int deletable = Judgement(GetABS(Time.time - (notesManager.NotesTime[nearestNote] + GManager.Instance.StartTime)));
            if (deletable >= 0)
            {
                Message(notesManager.LaneNum[nearestNote], deletable);
                DeleteData(nearestNote);
            }
        }
    }

    private int Judgement(float timeLag)
    {
        audio.PlayOneShot(hitSound);
        if (timeLag <= 0.05) // 원래 노트 처리 시간과 실제 시간 오차가 0.1초 이하라면 Perfect 판정
        {
            Debug.Log("Perfect");
            // message(notesManager.LaneNum[numOffset], 0);
            GManager.Instance.ratioScore += 5;
            GManager.Instance.perfect++;
            GManager.Instance.combo++;
            // deleteData(numOffset);
            return 0;
        }
        if (timeLag <= 0.08) // 원래 노트 처리 시간과 실제 시간 오차가 0.15초 이하라면 Great 판정
        {
            Debug.Log("Great");
            // message(notesManager.LaneNum[numOffset], 1);
            GManager.Instance.ratioScore += 3;
            GManager.Instance.great++;
            GManager.Instance.combo++;
            // deleteData(numOffset);
            return 1;
        }

        if (timeLag <= 0.10) // 원래 노트 처리 시간과 실제 시간 오차가 0.2초 이하라면 Bad 판정
        {
            Debug.Log("Bad");
            // message(notesManager.LaneNum[numOffset], 2);
            GManager.Instance.ratioScore += 1;
            GManager.Instance.bad++;
            GManager.Instance.combo++;
            // deleteData(numOffset);
            return 2;
        }
        
        return -1;
    }

    private float GetABS(float num)
    {
        return Mathf.Abs(num);
    }

    private void DeleteData(int numOffset)
    {
        notesManager.NotesTime.RemoveAt(numOffset);
        notesManager.LaneNum.RemoveAt(numOffset);
        notesManager.NoteType.RemoveAt(numOffset);
        GManager.Instance.score = (int)Math.Round(1000000 * Math.Floor(GManager.Instance.ratioScore / GManager.Instance.maxScore * 1000000) / 1000000);
        comboText.text = GManager.Instance.combo.ToString();
        scoreText.text = GManager.Instance.score.ToString();
    }

    private void Message(int laneNum, int judge)
    {
        // Judge Text Instantiate on Lane
        Instantiate(MessageObj[judge], new Vector3(laneNum-1.5f, 0.76f, 0.15f), Quaternion.Euler(45, 0, 0));
    }

    private void ResultScene()
    {
        SceneManager.LoadScene("Result");
    }
}
