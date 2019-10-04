using System;
using System.Collections.Generic;
using System.Text;

namespace Blake.Shared.Models.ViewModels
{
    public class UpdateModel
    {
        public List<int[]> CellsToDelete { get; set; }

        public List<int[]> NewSnakePosition { get; set; }

        public int[] FoodPosition { get; set; }

        public UpdateModel()
        {
            this.CellsToDelete = new List<int[]>();
            this.NewSnakePosition = new List<int[]>();
        }
    }
}
