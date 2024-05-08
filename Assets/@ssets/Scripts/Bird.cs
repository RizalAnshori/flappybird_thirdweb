using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Month1Clone.FlappyBird
{
    public class Bird : MonoBehaviour
    {
        [SerializeField] private float JUMP_AMOUNT = 90f;
        [SerializeField] private float ROTATION_MULTIPLIER = .15f;

        [SerializeField] private Rigidbody2D birdRigidBody2D;

        private Coroutine berserkModeCoroutine;

        private void Awake()
        {
            birdRigidBody2D.bodyType = RigidbodyType2D.Static;
            SubscribeEvent();
        }

        private void OnDestroy()
        {
            UnsubscribeEvent();
        }

        private void SubscribeEvent()
        {
            GameEvent.PlayerStateChanged += OnPlayerStateChanged;
        }

        private void UnsubscribeEvent()
        {
            GameEvent.PlayerStateChanged -= OnPlayerStateChanged;
        }

        private void Update()
        {
            switch (GameController.Instance.GameState)
            {
                case GameState.WaitingToStart:
                    if (GetInput())
                    {
                        GameController.Instance.GameState = GameState.Playing;
                        birdRigidBody2D.bodyType = RigidbodyType2D.Dynamic;
                        Jump();
                    }
                    break;
                case GameState.Playing:
                    if (GetInput())
                    {
                        Jump();
                    }
                    break;
                case GameState.GameOver:
                    break;
            }

            if(Input.GetKeyDown(KeyCode.A))
            {
                //BerserkMode();
                GameController.Instance.PlayerState = PlayerState.Berserk;
            }

            RotateBird();
        }

        private bool GetInput()
        {
            return
                Input.GetKeyDown(KeyCode.Space) ||
                Input.GetMouseButtonDown(0) ||
                Input.touchCount > 0;
        }

        private void Jump()
        {
            birdRigidBody2D.velocity = Vector2.up * JUMP_AMOUNT;
        }

        private void RotateBird()
        {
            transform.eulerAngles = new Vector3(0, 0, birdRigidBody2D.velocity.y * ROTATION_MULTIPLIER);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(GameController.Instance.GameState == GameState.Playing)
            {
                switch(GameController.Instance.PlayerState)
                {
                    case PlayerState.Normal:
                        Die();
                        break;
                    case PlayerState.Berserk:
                        DestroyPipe(collision);
                        break;
                    case PlayerState.TimeStop:

                        break;
                }
            }
        }

        private void DestroyPipe(Collider2D collision)
        {
            if (collision.CompareTag("Pipe"))
            {
                collision.transform.parent.gameObject.SetActive(false);
            }
        }

        private void Die()
        {
            birdRigidBody2D.bodyType = RigidbodyType2D.Static;
            GameController.Instance.GameState = GameState.GameOver;
        }

        private void OnPlayerStateChanged(PlayerStateChangedArgs args)
        {
            switch(args.playerState)
            {
                case PlayerState.Berserk:
                    {
                        BerserkMode();
                    }
                    break;
                case PlayerState.TimeStop:
                    {

                    }
                    break;
                case PlayerState.Normal:
                    {

                    }
                    break;
            }
        }

        private void BerserkMode()
        {
            if(berserkModeCoroutine !=null)
            {
                StopCoroutine(berserkModeCoroutine);
            }
            berserkModeCoroutine = StartCoroutine(BerserkModeIE());
        }

        public void TimeStopMode(bool isActive)
        {
            if(isActive)
            {

            }
            else
            {

            }
        }

        private void ActivateBerserkMode(bool isActive, float targetPipeSpeed)
        {
            GameController.Instance.PipeSpawnTimer = (GameController.Instance.PipeMoveSpeed * GameController.Instance.PipeSpawnTimer) / targetPipeSpeed;
            GameController.Instance.PipeMoveSpeed = targetPipeSpeed;
            if(isActive)
            {
                birdRigidBody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else
            {
                //birdRigidBody2D.bodyType = RigidbodyType2D.Dynamic;
                birdRigidBody2D.constraints = RigidbodyConstraints2D.None;
                GameController.Instance.PlayerState = PlayerState.Normal;
            }
        }

        private IEnumerator BerserkModeIE()
        {
            var currentNormalSpeed = GameController.Instance.PipeMoveSpeed;
            var targetBerserkSpeed = currentNormalSpeed + 20f;
            ActivateBerserkMode(true, targetBerserkSpeed);
            yield return new WaitForSeconds(5f);
            ActivateBerserkMode(false, currentNormalSpeed);
        }
    }
}