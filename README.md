# RhythmGaemu(Unithm)

**RhythmGaemu(Unithm)**은 Unity로 제작된 4키 기반 리듬 게임입니다. `D`, `F`, `J`, `K` 키로 노트를 처리하며, `Spacebar`를 눌러 게임을 시작하고, 곡이 끝나면 결과 화면(Result)을 확인할 수 있습니다. [Note Editor](https://github.com/setchi/NoteEditor/tree/master)를 사용해 JSON 형식의 채보를 작성할 수 있으며, 프로젝트 파일의 `RhythmGaemu\Assets\Resources` 폴더에 JSON 파일과 음악 파일을 넣어 플레이할 수 있습니다.

---

## 주요 기능 (Key Features)

- **4키 플레이**: `D`, `F`, `J`, `K` 키로 노트를 처리합니다.
- **게임 시작/종료**: `Spacebar`를 눌러 게임을 시작하고, 곡 종료 후 결과 화면을 확인합니다.
- **커스텀 채보 지원**: [Note Editor](https://github.com/setchi/NoteEditor/tree/master)를 사용해 JSON 형식으로 채보를 작성하고 게임에 불러올 수 있습니다.
- **정확도 기반 점수**: Perfect, Good, Miss로 플레이어의 정확도를 평가합니다.

---

## 플레이 방법 (How to Play)

1. **JSON 채보 준비**:
   - [Note Editor](https://github.com/setchi/NoteEditor/tree/master)를 사용해 채보를 작성합니다.
   - 채보를 JSON 파일로 저장합니다.

2. **리소스 추가**:
   - 작성한 JSON 파일과 음악 파일(mp3 또는 ogg)을 프로젝트 폴더의 다음 경로에 추가합니다:
     ```
     RhythmGaemu\Assets\Resources
     ```

3. **게임 시작**:
   - 게임을 실행하고 `Spacebar`를 눌러 시작합니다.

4. **노트 처리**:
   - 노트가 타겟 영역에 도달하면 `D`, `F`, `J`, `K` 키를 눌러 처리합니다.

5. **결과 확인**:
   - 곡이 끝나면 결과 화면에서 점수와 정확도를 확인합니다.

## 개발 환경 (Development Environment)
| 항목              | 내용                          |
|-------------------|------------------------------|
| **엔진**          | Unity (2021.3 이상)           |
| **언어**          | C#                           |
| **플랫폼**        | Windows(PC)                  |

## 라이선스 (License)

이 프로젝트는 MIT 라이선스 하에 배포됩니다. 자세한 내용은 `LICENSE` 파일을 참조하세요.
