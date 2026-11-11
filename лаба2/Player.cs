using System;

namespace лаба2
{
    public enum State
    {
        Winner, Loser, Playing, NotInGame
    }

    public class Player
    {
        public string name;
        public int location;
        public State state;
        public int distanceTraveled;

        public Player(string name)
        {
            this.name = name;
            this.location = -1;
            this.state = State.NotInGame;
            this.distanceTraveled = 0;
        }
    }
}