using Blake.Shared.Models.ViewModels;
using System;
using System.Timers;

namespace Blake.Shared.Entities.Snake
{
    public class EndEventArgs : EventArgs
    {
        public bool IsEnd { get; set; }

        public bool IsFed { get; set; }

        public int Points { get; set; }

        public UpdateModel UpdateModel { get; set; }
    }

    public class SnakeEngine
    {
        public delegate void ElapsedTimeEventHandler(object source, EndEventArgs e);

        public event ElapsedTimeEventHandler ElapsedTime;

        public Timer Timer { get; set; }

        public SnakeBoard SnakeBoard { get; set; }       

        public int IncrementStep { get; set; }

        public SnakeEngine(SnakeDifficulty difficulty)
        {
            this.SnakeBoard = new SnakeBoard(difficulty.BoardDimensions[0], difficulty.BoardDimensions[1], 5);
            this.IncrementStep = difficulty.IncrementStep;
            this.Timer = new Timer(difficulty.StartingSpeedMs);
            this.Timer.AutoReset = true;
            this.Timer.Elapsed += this.OnTimerElapse;
        }

        public void Run()
        {
            this.Timer.Start();
        }

        public void OnTimerElapse(object source, ElapsedEventArgs e)
        {
            var isFed = this.SnakeBoard.CheckForMeal();
            this.SnakeBoard.UpdateBody(isFed);
            if (isFed)
            {
                this.SnakeBoard.SetFoodPoint();

                if (SnakeBoard.CurrentPosition.Count == 10 || SnakeBoard.CurrentPosition.Count == 30)
                {
                    this.Timer.Interval -= this.IncrementStep * 20;
                }
                if (SnakeBoard.CurrentPosition.Count % this.IncrementStep == 0)
                {
                    this.Timer.Interval -= 1;
                }

            }

            var updateModel = this.BuildUpdateModel(isFed);
            this.OnElapsedTime(this.SnakeBoard.IsDead, isFed, updateModel);
        }

        public void UpdateDirection(char direction)
        {
            this.SnakeBoard.UpdateDirection(direction);
        }

        private UpdateModel BuildUpdateModel(bool isFed)
        {
            var model = new UpdateModel();

            model.FoodPosition = this.SnakeBoard.SnakeFoodPosition;
            if (isFed)
            {
                model.CellsToDelete.Add(this.SnakeBoard.PreviousFoodPosition);
            }

            if (this.SnakeBoard.PreviousPosition.Count != 0)
            {
                var previousCount = this.SnakeBoard.PreviousPosition.Count;

                for (int i = 0; i < previousCount; i++)
                {
                    var previousPoint = this.SnakeBoard.PreviousPosition.Pop();

                    if (i == 0)
                    {
                        model.CellsToDelete.Add(previousPoint);
                    }
                }
            }

            var segmentCount = this.SnakeBoard.CurrentPosition.Count;

            for (int i = 0; i < segmentCount; i++)
            {
                var segmentPoint = this.SnakeBoard.CurrentPosition.Dequeue();
                model.NewSnakePosition.Add(segmentPoint);
                this.SnakeBoard.CurrentPosition.Enqueue(segmentPoint);
            }

            return model;
        }

        protected virtual void OnElapsedTime(bool isEnd, bool isFed, UpdateModel model)
        {
            ElapsedTime?.Invoke(this, new EndEventArgs() { IsEnd = isEnd, IsFed = isFed, UpdateModel = model, Points = (this.SnakeBoard.CurrentPosition.Count - 5) * (6 / this.IncrementStep) });
        }
    }
}
