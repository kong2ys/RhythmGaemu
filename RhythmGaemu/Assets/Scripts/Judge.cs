using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Judge : MonoBehaviour
{
    [SerializeField] private GameObject[] MessageObj;
    [SerializeField] NotesManager notesManager;
    
    [SerializeField] TextMeshProUGUI comboText; // ComboNum
    [SerializeField] TextMeshProUGUI scoreText; // ScoreNum
    [SerializeField] private float missJudgeTime = 0.2f;

    AudioSource audio;
    [SerializeField] AudioClip hitSound;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GManager.instance.Start)
        {
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

            // 키 입력 처리 이후에도 키가 마지막까지 눌리지 않았을 때 Miss 처리
            if (Time.time > notesManager.NotesTime[0] + missJudgeTime + GManager.instance.StartTime)
            {
                message(notesManager.LaneNum[0], 3);
                deleteData(0);
                Debug.Log("Miss");
                GManager.instance.miss++;
                GManager.instance.combo = 0;
            }
        }
    }

    void JudgeOnLine(int lineNum)
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
            int deletable = Judgement(GetABS(Time.time - (notesManager.NotesTime[nearestNote] + GManager.instance.StartTime)));
            if (deletable >= 0)
            {
                message(notesManager.LaneNum[nearestNote], deletable);
                deleteData(nearestNote);
            }
        }
    }

    int Judgement(float timeLag)
    {
        audio.PlayOneShot(hitSound);
        if (timeLag <= 0.05) // 원래 노트 처리 시간과 실제 시간 오차가 0.1초 이하라면 Perfect 판정
        {
            Debug.Log("Perfect");
            // message(notesManager.LaneNum[numOffset], 0);
            GManager.instance.ratioScore += 5;
            GManager.instance.perfect++;
            GManager.instance.combo++;
            // deleteData(numOffset);
            return 0;
        }
        if (timeLag <= 0.08) // 원래 노트 처리 시간과 실제 시간 오차가 0.15초 이하라면 Great 판정
        {
            Debug.Log("Great");
            // message(notesManager.LaneNum[numOffset], 1);
            GManager.instance.ratioScore += 3;
            GManager.instance.great++;
            GManager.instance.combo++;
            // deleteData(numOffset);
            return 1;
        }

        if (timeLag <= 0.10) // 원래 노트 처리 시간과 실제 시간 오차가 0.2초 이하라면 Bad 판정
        {
            Debug.Log("Bad");
            // message(notesManager.LaneNum[numOffset], 2);
            GManager.instance.ratioScore += 1;
            GManager.instance.bad++;
            GManager.instance.combo++;
            // deleteData(numOffset);
            return 2;
        }
        
        return -1;
    }

    float GetABS(float num)
    {
        return Mathf.Abs(num);
    }

    void deleteData(int numOffset)
    {
        notesManager.NotesTime.RemoveAt(numOffset);
        notesManager.LaneNum.RemoveAt(numOffset);
        notesManager.NoteType.RemoveAt(numOffset);
        GManager.instance.score = (int)Math.Round(1000000 * Math.Floor(GManager.instance.ratioScore / GManager.instance.maxScore * 1000000) / 1000000);
        comboText.text = GManager.instance.combo.ToString();
        scoreText.text = GManager.instance.score.ToString();
    }

    void message(int laneNum, int judge)
    {
        // Judge Text Instantiate on Lane
        Instantiate(MessageObj[judge], new Vector3(laneNum-1.5f, 0.76f, 0.15f), Quaternion.Euler(45, 0, 0));
    }
}
