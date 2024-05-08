using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Month1Clone.FlappyBird
{
    public interface IBirdMode
    {
        PlayerState TypeMode { get; set; }
        void ActivateState();
    }
}