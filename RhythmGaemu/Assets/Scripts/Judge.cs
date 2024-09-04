using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Judge : MonoBehaviour
{
    [SerializeField] private GameObject[] MessageObj;
    [SerializeField] NotesManager notesManager;
    
    [SerializeField] TextMeshProUGUI comboText; // ComboNum
    [SerializeField] TextMeshProUGUI scoreText; // ScoreNum

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
            if (Input.GetKeyDown(KeyCode.D)) // O키가 눌렸을 때
            {
                if (notesManager.LaneNum[0] == 0) // 눌린 버튼이 레인 번호가 맞는지
                {
                    // 오차 시간 계산 후 그 절대값를 Judgement에 보냄
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.StartTime)), 0);
                }
                else
                {
                    if (notesManager.LaneNum[1] == 0)
                    {
                        Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.StartTime)), 1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (notesManager.LaneNum[0] == 1)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.StartTime)), 0);
                }
                else
                {
                    if (notesManager.LaneNum[1] == 1)
                    {
                        Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.StartTime)), 1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (notesManager.LaneNum[0] == 2)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.StartTime)), 0);
                }
                else
                {
                    if (notesManager.LaneNum[1] == 2)
                    {
                        Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.StartTime)), 1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (notesManager.LaneNum[0] == 3)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.StartTime)), 0);
                }
                else
                {
                    if (notesManager.LaneNum[1] == 3)
                    {
                        Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManager.instance.StartTime)), 1);
                    }
                }
            }

            // 키 입력 처리 이후에도 키가 마지막까지 눌리지 않았을 때 Miss 처리
            if (Time.time > notesManager.NotesTime[0] + 0.2f + GManager.instance.StartTime)
            {
                message(3);
                deleteData(0);
                Debug.Log("Miss");
                GManager.instance.miss++;
                GManager.instance.combo = 0;
            }
        }
    }

    void Judgement(float timeLag, int numOffset)
    {
        audio.PlayOneShot(hitSound);
        if (timeLag <= 0.05) // 원래 노트 처리 시간과 실제 시간 오차가 0.1초 이하라면 Perfect 판정
        {
            Debug.Log("Perfect");
            message(0);
            GManager.instance.ratioScore += 5;
            GManager.instance.perfect++;
            GManager.instance.combo++;
            deleteData(numOffset);
        }
        else
        {
            if (timeLag <= 0.08) // 원래 노트 처리 시간과 실제 시간 오차가 0.15초 이하라면 Great 판정
            {
                Debug.Log("Great");
                message(1);
                GManager.instance.ratioScore += 3;
                GManager.instance.great++;
                GManager.instance.combo++;
                deleteData(numOffset);
            }
            else
            {
                if (timeLag <= 0.10) // 원래 노트 처리 시간과 실제 시간 오차가 0.2초 이하라면 Bad 판정
                {
                    Debug.Log("Bad");
                    message(2);
                    GManager.instance.ratioScore += 1;
                    GManager.instance.bad++;
                    GManager.instance.combo++;
                    deleteData(numOffset);
                }
            }
        }
    }

    float GetABS(float num)
    {
        if (num >= 0)
        {
            return num;
        }
        else
        {
            return -num;
        }
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

    void message(int judge)
    {
        Instantiate(MessageObj[judge], new Vector3(notesManager.LaneNum[0]-1.5f, 0.76f, 0.15f), Quaternion.Euler(45, 0, 0));
    }
}
