using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Month1Clone.FlappyBird
{
    public class PowerUp : MonoBehaviour
    {
        [SerializeField]protected PlayerState PowerUpType;


        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.GetComponent<Bird>() !=null)
            {
                GameController.Instance.PlayerState = PowerUpType;
                this.gameObject.SetActive(false);
            }

            Debug.Log("Collide");
        }

        protected virtual void Update()
        {
            if (GameController.Instance.GameState == GameState.Playing)
            {
                this.transform.position += new Vector3(-1, 0, 0) * GameController.Instance.PipeMoveSpeed * Time.deltaTime;
            }
        }
    }
}