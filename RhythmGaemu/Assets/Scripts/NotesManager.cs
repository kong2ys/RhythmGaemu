using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Note[] notes;
}

[Serializable]
public class Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;
}

public class NotesManager : MonoBehaviour
{

    public int noteNum; // 노트 번호
    private string songName; // 곡명
    
    public List<int> LaneNum = new List<int>(); // 몇 번 레인에 노트가 떨어지는지
    public List<int> NoteType = new List<int>(); // 어떤 노트인지(일반노트 or 롱노트)
    public List<float> NotesTime = new List<float>(); // 노트가 판정선과 겹치는 시간
    public List<GameObject> NotesObj = new List<GameObject>();

    [SerializeField] private float NotesSpeed; // 노트 스피드
    [SerializeField] GameObject noteObj;

    void OnEnable()
    {
        NotesSpeed = GManager.instance.noteSpeed;
        noteNum = 0; // 총 노트 수를 0으로
        songName = "200"; // 플레이하는 곡명 테스트시 무조건 적기
        Load(songName);
    }

    private void Load(string SongName)
    {
        Debug.Log("Current SongName: " + SongName);
        string inputString = Resources.Load<TextAsset>(SongName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString); // Json 파일 읽음
        
        noteNum = inputJson.notes.Length; // 총 노트 수 설정
        GManager.instance.maxScore = noteNum * 5;

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            // 시간 계산
            float interval = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = interval * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f;
            
            // 목록에 추가
            NotesTime.Add(time);
            LaneNum.Add(inputJson.notes[i].block);
            NoteType.Add(inputJson.notes[i].type);
            
            // 노트 생성
            float z = NotesTime[i] * NotesSpeed;
            NotesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block -1.5f, 0.55f, z), Quaternion.identity));
        }
    }
}
