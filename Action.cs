using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class Action
    {
        public int slot;
        public string player;
        public int value;

        public Action(int slot, string player)
        {
            this.slot = slot;
            this.player = player;
        }
        public override string ToString()
        {
            return "Value:"+value + " Slot: " + slot + " Player: " + player;
        }
    }
}
