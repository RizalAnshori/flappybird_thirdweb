using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Month1Clone.FlappyBird
{
    public enum GameState
    {
        WaitingToStart,
        Playing,
        GameOver
    }

    public enum PlayerState
    {
        Normal,
        Berserk,
        TimeStop
    }

    public enum PowerUpType
    {
        Berserk,
        TimeStop
    }

    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        private void OnEnable()
        {
            Instance = this;
        }

        [Header("Pipe Configurations")]
        [SerializeField] private float pipeMoveSpeed;
        public float PipeMoveSpeed
        {
            get
            {
                return pipeMoveSpeed;
            }
            set
            {
                pipeMoveSpeed = value;
            }
        }

        [SerializeField] private float pipeSpawnTimer = 3f;
        public float PipeSpawnTimer
        {
            get
            {
                return pipeSpawnTimer;
            }
            set
            {
                pipeSpawnTimer = value;
            }
        }

        [SerializeField] private float gapSize;
        public float GapSize
        {
            get
            {
                return gapSize;
            }
        }

        [Header("Bird Info")]
        [SerializeField] private PlayerState playerState = PlayerState.Normal;
        public PlayerState PlayerState
        {
            get
            {
                return playerState;
            }
            set
            {
                playerState = value;
                GameEvent.NotifyPlayerStateChanged(new PlayerStateChangedArgs(value));
            }
        }

        [SerializeField] private Bird bird;
        public Bird Bird
        {
            get
            {
                return bird;
            }
            private set
            {
                bird = value;
            }
        }

        [Header("Game Info")]
        [SerializeField] private GameState gameState = GameState.WaitingToStart;
        public GameState GameState
        {
            get
            {
                return gameState;
            }
            set
            {
                gameState = value;
                GameEvent.NotifyGameStateChanged(new GameStateChagedArgs(value));
            }
        }

        private void Awake()
        {
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            GameEvent.GameStateChanged += OnGameStateChanged;
        }

        private void Unsubscribe()
        {
            GameEvent.GameStateChanged -= OnGameStateChanged;
        }

        private void Start()
        {
        }

        private void OnGameStateChanged(GameStateChagedArgs args)
        {
            switch(args.gameState)
            {
                case GameState.WaitingToStart:
                    GameUI.Instance.ActivateGameOver(false);
                    GameUI.Instance.SetTextInfo("Press Any Key To Start", 60);
                    break;
                case GameState.Playing:
                    GameUI.Instance.ActivateGameOver(false);
                    GameUI.Instance.SetTextInfo("", 60);
                    break;
                case GameState.GameOver:
                    //GameUI.Instance.SetTextInfo("GAME OVER", 100);
                    GameUI.Instance.ActivateGameOver(true);
                    break;
            }
        }

        public void SetDifficulty()
        {

        }

        public void RestartGame()
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}