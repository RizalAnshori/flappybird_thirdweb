using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

namespace Month1Clone.FlappyBird
{
    public class PipeSpawner : MonoBehaviour
    {
        [SerializeField] private LeanGameObjectPool pipePooler;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private PowerUpSpawner powerUpSpawner;

        [SerializeField] private float initialPipePositionX;
        [SerializeField] private float heightEdgeLimit;

        private float pipeSpawnTimer;
        private bool isPowerUpSpawned;

        private void Update()
        {
            if(GameController.Instance.GameState == GameState.Playing)
            {
                SpawnPipe();
            }
        }

        private void SpawnPipe()
        {
            pipeSpawnTimer += Time.deltaTime;
            if (pipeSpawnTimer > GameController.Instance.PipeSpawnTimer)
            {
                pipeSpawnTimer = 0;
                isPowerUpSpawned = false;

                float minHeight = GameController.Instance.GapSize * .5f + heightEdgeLimit;
                float totalHeight = mainCamera.orthographicSize * 2f;
                float maxHeight = totalHeight - GameController.Instance.GapSize * .5f - heightEdgeLimit;

                float height = Random.Range(minHeight, maxHeight);
                CreateGapTopBottomPipes(height, GameController.Instance.GapSize, initialPipePositionX);
            }

            if(pipeSpawnTimer > GameController.Instance.PipeSpawnTimer/2 && isPowerUpSpawned == false && pipePooler.Spawned > 1)
            {
                isPowerUpSpawned = true;
                var halfHeight = mainCamera.orthographicSize;
                powerUpSpawner.Init(new Vector2(initialPipePositionX,Random.Range(-halfHeight + heightEdgeLimit,halfHeight - heightEdgeLimit)));
            }
        }

        private void CreateGapTopBottomPipes(float gapY, float gapSize, float xPosition)
        {
            CreatePipe(gapY - gapSize * .5f, xPosition, true);
            CreatePipe(mainCamera.orthographicSize * 2f - gapY - gapSize * .5f, xPosition, false);
        }

        private void CreatePipe(float height, float xPosition, bool isCreateBottom)
        {
            Pipe pipe = pipePooler.Spawn(Vector3.zero, Quaternion.identity).GetComponent<Pipe>();

            pipe.SetupPipe(mainCamera.orthographicSize, height, xPosition, isCreateBottom);
        }
    }
}