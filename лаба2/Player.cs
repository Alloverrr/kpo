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

        public void Move(int steps, int boardSize)
        {
            // Если игрок еще не на поле — это его стартовая позиция
            if (state == State.NotInGame)
            {
                location = steps;
                state = State.Playing;
                distanceTraveled = 0;
                return;
            }

            // пройденное расстояние
            distanceTraveled += Math.Abs(steps);

            // Считаем новую позицию
            int newLocation = location + steps;

            // oбработка круга 
            while (newLocation > boardSize) newLocation -= boardSize;
            while (newLocation < 1) newLocation += boardSize;

            location = newLocation;
        }
    }
}