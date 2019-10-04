using System;
using System.Collections.Generic;
using System.Text;

namespace Blake.Shared.Entities.Snake
{
    public class SnakeBoard
    {
        public int SnakeBoardWidth { get; set; }

        public int SnakeBoardHeight { get; set; }

        public Queue<int[]> CurrentPosition { get; set; }

        public Stack<int[]> PreviousPosition { get; set; }

        public int[] SnakeFoodPosition { get; set; }

        public int[] PreviousFoodPosition { get; set; }

        public char Direction { get; set; }

        public bool IsDead { get; set; }

        private Random Random { get; set; }

        public SnakeBoard(int boardWidth, int boardHeight, int snakeLength)
        {
            this.SnakeBoardWidth = boardWidth;
            this.SnakeBoardHeight = boardHeight;
            this.Direction = 'd';
            this.Random = new Random();
            this.CurrentPosition = new Queue<int[]>();
            this.PreviousPosition = new Stack<int[]>();
            this.IsDead = false;
            this.InitializeSnakeBody(snakeLength);
            this.SetFoodPoint();
        }

        private void InitializeSnakeBody(int length)
        {
            for (int i = 0; i < length; i++)
            {
                var currentSegment = new int[2];
                currentSegment[0] = (this.SnakeBoardWidth / 2) - i;
                currentSegment[1] = this.SnakeBoardHeight / 2;

                this.CurrentPosition.Enqueue(currentSegment);
            }
        }

        public void UpdateDirection(char direction)
        {
            switch (direction)
            {
                case 'd':
                    if (this.Direction != 'a')
                    {
                        this.Direction = direction;
                    }
                    break;
                case 'w':
                    if (this.Direction != 's')
                    {
                        this.Direction = direction;
                    }
                    break;
                case 's':
                    if (this.Direction != 'w')
                    {
                        this.Direction = direction;
                    }
                    break;
                case 'a':
                    if (this.Direction != 'd')
                    {
                        this.Direction = direction;
                    }
                    break;
            }
        }

        public void UpdateBody(bool hasEaten)
        {
            var currentCount = CurrentPosition.Count;

            for (int i = 0; i < currentCount; i++)
            {
                var currentBlock = CurrentPosition.Dequeue();

                if (i == 0)
                {
                    this.PreviousPosition.Push(new int[] { currentBlock[0], currentBlock[1]});

                    switch (this.Direction)
                    {
                        case 'd':
                            currentBlock[0] += 1;
                            break;
                        case 'w':
                            currentBlock[1] -= 1;
                            break;
                        case 's':
                            currentBlock[1] += 1;
                            break;
                        case 'a':
                            currentBlock[0] -= 1;
                            break;
                    }
                }
                else
                {
                    var previousBlock = this.PreviousPosition.Peek();
                    this.PreviousPosition.Push(new int[] { currentBlock[0], currentBlock[1] });
                    if (!hasEaten)
                    {
                        currentBlock[0] = previousBlock[0];
                        currentBlock[1] = previousBlock[1];
                    }
                }
                CurrentPosition.Enqueue(currentBlock);
            }

            this.CheckForDeath();
        }

        public bool CheckForMeal()
        {
            var snakeHead = this.CurrentPosition.Peek();

            if (snakeHead[0] == this.SnakeFoodPosition[0] && snakeHead[1] == this.SnakeFoodPosition[1])
            {
                var preMealSnakeSize = this.CurrentPosition.Count;
                CurrentPosition.Enqueue(this.SnakeFoodPosition);
                for (int i = 0; i < preMealSnakeSize; i++)
                {
                    this.CurrentPosition.Enqueue(this.CurrentPosition.Dequeue());
                }
                return true;
            }

            return false;
        }

        private void CheckForDeath()
        {
            var firstSegmentX = this.CurrentPosition.Peek()[0];
            var firstSegmentY = this.CurrentPosition.Peek()[1];

            var currentCount = CurrentPosition.Count;

            for (int i = 0; i < currentCount; i++)
            {
                var checkSegment = CurrentPosition.Dequeue();
                if (i != 0 && checkSegment[0] == firstSegmentX && checkSegment[1] == firstSegmentY)
                {
                    this.IsDead = true;
                }

                CurrentPosition.Enqueue(checkSegment);
            }

            if (firstSegmentX >= this.SnakeBoardWidth || firstSegmentX < 0)
            {
                this.IsDead = true;
            }

            if (firstSegmentY >= this.SnakeBoardHeight || firstSegmentY < 0)
            {
                this.IsDead = true;
            }
        }

        public void SetFoodPoint()
        {
            bool isSpaceAvailable = false;
            var newFood = new int[2];

            while (!isSpaceAvailable)
            {
                newFood[0] = this.Random.Next(0, this.SnakeBoardWidth);
                newFood[1] = this.Random.Next(0, this.SnakeBoardHeight);

                isSpaceAvailable = this.CheckIfSpaceIsAvailable(newFood);
            }
            this.PreviousFoodPosition = this.SnakeFoodPosition;
            this.SnakeFoodPosition = newFood;
        }

        private bool CheckIfSpaceIsAvailable(int[] newFood)
        {
            var snakeLength = this.CurrentPosition.Count;
            var isAvailable = true;

            for (int i = 0; i < snakeLength; i++)
            {
                var currentSegment = this.CurrentPosition.Dequeue();
                if (currentSegment[0] == newFood[0] && currentSegment[1] == newFood[1])
                {
                    isAvailable = false;
                }
                this.CurrentPosition.Enqueue(currentSegment);
            }

            return isAvailable;
        }
    }
}
