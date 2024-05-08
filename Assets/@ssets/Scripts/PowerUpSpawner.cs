using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

namespace Month1Clone.FlappyBird
{
    public class PowerUpSpawner : MonoBehaviour
    {
        [SerializeField, Range(0,100)] private float showUpChance;
        [SerializeField] private LeanGameObjectPool pooler;
        [SerializeField] private Sprite berserkSprite;

        public void Init(Vector2 targetPos)
        {
            var randChance = Random.Range(0, 100);
            if(GameController.Instance.PlayerState == PlayerState.Normal)
            {
                if(randChance < showUpChance)
                {
                    Spawn(targetPos);
                }
            }
            else
            {
                pooler.DespawnAll();
            }
        }

        private void Spawn(Vector2 targetPos)
        {
            var powerUp = pooler.Spawn(targetPos,Quaternion.identity);
        }
        
    }
}