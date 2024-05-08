using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Month1Clone.FlappyBird
{
    public class Pipe : MonoBehaviour
    {
        private readonly float defaultZPosition = -1f;

        [SerializeField] private Transform pipeTransform;
        [SerializeField] private float pipeWidth;

        [SerializeField] private Transform pipeHead;
        [SerializeField] private SpriteRenderer pipeHeadRenderer;
        [SerializeField] private BoxCollider2D pipeHeadCollider2D;

        [SerializeField] private Transform pipeBody;
        [SerializeField] private SpriteRenderer pipeBodyRenderer;
        [SerializeField] private BoxCollider2D pipeBodyCollider2D;

        [SerializeField] private bool isBottom = false;

        public Transform PipeTransform
        {
            get
            {
                return pipeTransform;
            }
        }

        public void SetupPipe(float cameraOrthoSize, float height,float initialPositionX, bool isBottomPipe)
        {
            isBottom = isBottomPipe;
            float pipeHeadYPosition;
            float pipeBodyYPosition;
            if (isBottomPipe)
            {
                pipeHeadYPosition = -cameraOrthoSize + height - pipeHeadRenderer.size.y * .5f;
                pipeBodyYPosition = -cameraOrthoSize;
            }
            else
            {
                pipeHeadYPosition = +cameraOrthoSize - height + pipeHeadRenderer.size.y * .5f;
                pipeBodyYPosition = +cameraOrthoSize;
                pipeBody.localScale = new Vector3(1, -1, 1);
            }

            pipeHead.position = new Vector3(initialPositionX, pipeHeadYPosition, defaultZPosition);
            pipeBody.position = new Vector3(initialPositionX, pipeBodyYPosition, defaultZPosition);

            pipeHeadCollider2D.size = new Vector2(pipeHeadRenderer.size.x, pipeHeadRenderer.size.y);
            pipeBodyCollider2D.offset = new Vector2(0, pipeHeadRenderer.size.y*.5f);

            pipeBodyRenderer.size = new Vector2(pipeWidth, height);
            pipeBodyCollider2D.size = new Vector2(pipeWidth, height);
            pipeBodyCollider2D.offset = new Vector2(0f, height * .5f);

            this.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (GameController.Instance.GameState == GameState.Playing)
            {
                this.Move();
            }
        }

        private void Move()
        {
            bool isBirdPassed = this.pipeHead.position.x > GameController.Instance.Bird.transform.position.x;
            pipeTransform.position += new Vector3(-1, 0, 0) * GameController.Instance.PipeMoveSpeed * Time.deltaTime;
            if (isBirdPassed && this.pipeHead.position.x <= GameController.Instance.Bird.transform.position.x && isBottom)
            {
                GameEvent.NotifyBirdPassed(new BirdPassedArgs());
            }
        }
    }
}