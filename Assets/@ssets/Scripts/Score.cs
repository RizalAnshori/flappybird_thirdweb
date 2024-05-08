using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Month1Clone.FlappyBird
{
    public class Score : MonoBehaviour
    {
        private static readonly string highScorePrefsKey = "flappybirdhighscore";

        [SerializeField] private int highScore;
        [SerializeField] private int score;

        [Header("UI Score Component")]
        [SerializeField] private TMP_Text highScoreTMP;
        [SerializeField] private TMP_Text scoreTMP;

        private void Awake()
        {
            SubscribeEvent();    
        }

        private void OnDisable()
        {
            UnsubscribeEvent();
        }

        private void SubscribeEvent()
        {
            GameEvent.BirdPassed += OnBirdPassed;
            GameEvent.GameStateChanged += OnGameStateChanged;
        }

        private void UnsubscribeEvent()
        {
            GameEvent.BirdPassed -= OnBirdPassed;
        }

        private void Start()
        {
            LoadHighScore();
            UpdateScoreUI();
        }

        private void OnBirdPassed(BirdPassedArgs args)
        {
            AddScore(1);
            UpdateScoreUI();
        }

        private void OnGameStateChanged(GameStateChagedArgs args)
        {
            switch(args.gameState)
            {
                case GameState.GameOver:
                    SaveHighScore();
                    break;
                case GameState.WaitingToStart:
                    ResetScore();
                    break;
            }
        }

        private void AddScore(int amount = 1)
        {
            score+=amount;
            if (score > highScore)
            {
                highScore = score;
            }
        }

        private void ResetScore()
        {
            score = 0;
            LoadHighScore();
            UpdateScoreUI();
        }

        private void UpdateScoreUI()
        {
            highScoreTMP.text = "Highscore : "+ highScore;
            scoreTMP.text = score.ToString();
        }

        private void LoadHighScore()
        {
            if(PlayerPrefs.HasKey(highScorePrefsKey))
            {
                highScore = PlayerPrefs.GetInt(highScorePrefsKey);
            }
        }

        private void SaveHighScore()
        {
            PlayerPrefs.SetInt(highScorePrefsKey, highScore);
        }
    }
}