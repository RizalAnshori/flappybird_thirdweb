using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Month1Clone.FlappyBird
{

    public class BerserkMode : IBirdMode
    {
        [SerializeField]private PlayerState typeMode;
        public PlayerState TypeMode 
        { 
            get
            {
                return typeMode;
            }
            set 
            { 

            }
        }

        public void ActivateState()
        {
            throw new System.NotImplementedException();
        }
    }
}