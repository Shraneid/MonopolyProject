using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_Project
{
    class Player
    {
        private int initial_position;
        private string name;
        public Player(int initial_position, string name)
        {
            this.initial_position = initial_position;
            this.name = name;
        }
        public int Initial_position
        {
            get;
            set;
        }
        public string Name
        {
            get;
        }
        public void Current_position()
        {
            Console.WriteLine(this.Name + " , this is your turn !\n You are now in position : " + this.Initial_position);
        }

    }
}
