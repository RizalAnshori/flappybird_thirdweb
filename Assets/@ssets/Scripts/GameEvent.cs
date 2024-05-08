using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Month1Clone.FlappyBird
{
    public class GameEvent
    {
        public static Action<DestroyPipeEventArgs> DestroyPipe;
        public static void NotifyDestroyPipe(DestroyPipeEventArgs args)
        {
            if (DestroyPipe != null)
            {
                DestroyPipe.Invoke(args);
            }
        }

        public static Action<BirdPassedArgs> BirdPassed;
        public static void NotifyBirdPassed(BirdPassedArgs args)
        {
            if(BirdPassed!=null)
            {
                BirdPassed.Invoke(args);
            }
        }

        public static Action<GameStateChagedArgs> GameStateChanged;
        public static void NotifyGameStateChanged ( GameStateChagedArgs args)
        {
            if(GameStateChanged!=null)
            {
                GameStateChanged.Invoke(args);
            }
        }

        public static Action<PlayerStateChangedArgs> PlayerStateChanged;
        public static void NotifyPlayerStateChanged(PlayerStateChangedArgs args)
        {
            if(PlayerStateChanged!=null)
            {
                PlayerStateChanged.Invoke(args);
            }
        }
    }

    public class DestroyPipeEventArgs : EventArgs
    {
        public Pipe pipe;

        public DestroyPipeEventArgs(Pipe pipe)
        {
            this.pipe = pipe;
        }
    }

    public class BirdPassedArgs : EventArgs
    {
        public BirdPassedArgs()
        {
        }
    }

    public class GameStateChagedArgs : EventArgs
    {
        public GameState gameState;

        public GameStateChagedArgs(GameState gameState)
        {
            this.gameState = gameState;
        }
    }

    public class PlayerStateChangedArgs : EventArgs
    {
        public PlayerState playerState;
        public PlayerStateChangedArgs(PlayerState playerState)
        {
            this.playerState = playerState;
        }
    }
}