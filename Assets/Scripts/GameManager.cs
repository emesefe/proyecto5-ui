using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
     [SerializeField] private GameObject[] targetPrefabs;

     private float minX = -3.75f;
     private float minY = -3.75f;
     private float distanceBetweenCenters = 2.5f;

     private bool isGameOver;
     private float spawnRate = 1f; // Los targets aparecerán cada 2 segundos

     private Vector3 randomPos;

     private int score;

     private int time;
     public const int TIME_MAX = 12; // medido en segundos

     private UIManager uiManager;

     public List<Vector3> targetPositionsInScene;

     [SerializeField] private AudioSource backgroundMusicAudioSource;

     private void Awake()
     {
          targetPositionsInScene = new List<Vector3>();
     }

     private void Start()
     {
          uiManager = FindObjectOfType<UIManager>();
          uiManager.HideGameOverPanel();
          uiManager.ShowMainMenuPanel();
     }

     // difficulty = 1 (Easy)
     // difficulty = 2 (Medium)
     // difficulty = 3 (Hard)
     public void StartGame(int difficulty)
     {
          uiManager.HideMainMenuPanel();
          uiManager.HideOptionsPanel();

          score = 0;
          UpdateScore(0);

          time = TIME_MAX / difficulty;
          uiManager.UpdateTimeUI(time);

          // spawnRate = spawnRate / difficulty;
          spawnRate /= difficulty;

          StartCoroutine(SpawnRandomTarget());
          StartCoroutine(Timer());
     }

     private Vector3 RandomSpawnPosition()
     {
          // -3.75, -1.25, 1.25, 3.75
          float spawnPosX = minX + Random.Range(0, 4) * distanceBetweenCenters;
          float spawnPosY = minY + Random.Range(0, 4) * distanceBetweenCenters;

          return new Vector3(spawnPosX, spawnPosY, 0);
     }

     private IEnumerator SpawnRandomTarget()
     {
          while (!isGameOver)
          {
               yield return new WaitForSeconds(spawnRate);

               if (isGameOver)
               {
                    break;
               }

               int randomPrefabsIndex = Random.Range(0, targetPrefabs.Length);
               
               randomPos = RandomSpawnPosition();
               while (targetPositionsInScene.Contains(randomPos))
               {
                    randomPos = RandomSpawnPosition();
               }
               
               Instantiate(targetPrefabs[randomPrefabsIndex], 
                         randomPos,
                         targetPrefabs[randomPrefabsIndex].transform.rotation);

               targetPositionsInScene.Add(randomPos);
          } 
     }

     private IEnumerator Timer()
     {
          while (!isGameOver)
          {
               yield return new WaitForSeconds(1);
               
               if (isGameOver)
               {
                    break;
               }

               UpdateTime();
          }
     }

     private void UpdateTime()
     {
          time--;
          uiManager.UpdateTimeUI(time);

          if (time <= 0) 
          {
               isGameOver = true;
               uiManager.ShowGameOverPanel(score);
          }
     }

     public void UpdateScore(int newPoints)
     {
          score += newPoints;
          uiManager.UpdateScoreText(score);
     }

     public bool IsGameOver()
     {
          return isGameOver;
     }

     public void RestartGameScene()
     {
          SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     }

     public void SetBackgroundMusicVolume(float volume)
     {
          backgroundMusicAudioSource.volume = volume;
     }

     public void MuteMusic(bool musicMuted)
     {
          backgroundMusicAudioSource.mute = musicMuted;
          uiManager.SetSliderValue(0);
     }
}
