using System;
using System.Linq;
using System.Collections.Generic;

namespace TicTacToe
{
    class State
    {       

        public string[] board = {"_", "_", "_", "_", "_", "_", "_", "_", "_"};
        
        public State()
        {
            
        }
        public State(State state)
        {
            board = (string[]) state.board.Clone();
        }
        

        public State ApplyAction(Action a)
        {
            
            State state = new State(this);
            if (state.board[a.slot] == "_" && !IsTerminal())
            {                
                state.board[a.slot] = a.player;
                
            }            
            
            
            return state;
        }

        public bool IsTerminal()
        {
            if(!board.Contains("_")){
                return true;
            }
            return 
                // Verticals
                (board[0] == board[3] && board[3] == board[6] && board[3] != "_") ||
                (board[1] == board[4] && board[4] == board[7] && board[4] != "_") ||
                (board[2] == board[5] && board[5] == board[8] && board[5] != "_") ||
                // Diagonals                                                 
                (board[0] == board[4] && board[4] == board[8] && board[4] != "_") ||
                (board[6] == board[4] && board[4] == board[2] && board[4] != "_") ||
                // Horizontals                                               
                (board[0] == board[1] && board[1] == board[2] && board[1] != "_") ||
                (board[3] == board[4] && board[4] == board[5] && board[4] != "_") ||
                (board[6] == board[7] && board[7] == board[8] && board[7] != "_");           
            
        }
        public int Score()
        {
            if (IsTerminal())
            {
                if(
                    // Verticals
                    (board[0] == board[3] && board[3] == board[6] && board[3] == "O") ||
                    (board[1] == board[4] && board[4] == board[7] && board[4] == "O") ||
                    (board[2] == board[5] && board[5] == board[8] && board[5] == "O") ||
                    // Diagonals
                    (board[0] == board[4] && board[4] == board[8] && board[4] == "O") ||
                    (board[6] == board[4] && board[4] == board[2] && board[4] == "O") ||
                    // Horizontals
                    (board[0] == board[1] && board[1] == board[2] && board[1] == "O") ||
                    (board[3] == board[4] && board[4] == board[5] && board[4] == "O") ||
                    (board[6] == board[7] && board[7] == board[8] && board[7] == "O")
                )
                {
                    return -1;
                }
                else if(
                    // Verticals
                    (board[0] == board[3] && board[3] == board[6] && board[3] == "X") ||
                    (board[1] == board[4] && board[4] == board[7] && board[4] == "X") ||
                    (board[2] == board[5] && board[5] == board[8] && board[5] == "X") ||
                    // Diagonals                                                  
                    (board[0] == board[4] && board[4] == board[8] && board[4] == "X") ||
                    (board[6] == board[4] && board[4] == board[2] && board[4] == "X") ||
                    // Horizontals                                                
                    (board[0] == board[1] && board[1] == board[2] && board[1] == "X") ||
                    (board[3] == board[4] && board[4] == board[5] && board[4] == "X") ||
                    (board[6] == board[7] && board[7] == board[8] && board[7] == "X")
                )
                {
                    return 1;
                }
            }
            return 0;
        } 
        public string GetPlayer()
        {
            int o = 0;
            int x = 0;
            for (int i = 0; i < 9; i++)
            {
                if      (board[i] == "O") o++;
                else if (board[i] == "X") x++;            
            }

            if (x <= o) return "X";
            else        return "O";
        }
        public List<Action> Actions()
        {
            string player = GetPlayer();
            List<Action> actions = new List<Action>();
            for(int i = 0; i < 9; i++)
            {
                if(board[i] == "_")
                {
                    actions.Add(new Action(i, player));
                }
            }
            
            return actions;
        }
        public static int MaxValue(State state)
        {
            if (state.IsTerminal())
            {               
                return state.Score();
            }
            int v = int.MinValue;
            foreach (Action action in state.Actions())
            {               
                v = Math.Max(v, MinValue(state.ApplyAction(action)));
            }

            return v;           
        }
        public static int MinValue(State state)
        {
            if (state.IsTerminal())
            {                
                return state.Score();
            }
            int v = int.MaxValue;            
            foreach (Action action in state.Actions())
            {
                v = Math.Min(v, MaxValue(state.ApplyAction(action)));
            }

            return v;
        }
        public static Action NextMove(State state)
        {
            List<Action> actions = state.Actions();
            if (actions.Count == 0 || state.IsTerminal()) return null;
            string player = state.GetPlayer();
            Action bestMove = actions[0];            
            if(player == "X")
            {
                for (int i = 0; i < actions.Count; i++)
                {
                    actions[i].value = MinValue(state.ApplyAction(actions[i]));
                }
                
                foreach(Action a in actions)
                {
                    if(a.value > bestMove.value)
                    {
                        bestMove = a;                        
                    }
                }


                return bestMove;
            }
            else
            {
                for (int i = 0; i < actions.Count; i++)
                {
                    actions[i].value = MaxValue(state.ApplyAction(actions[i]));                    
                }

                foreach (Action a in actions)
                {

                    if (a.value < bestMove.value)
                    {
                        bestMove = a;
                    }
                }
                return bestMove;
            }
        }
        public void Print()
        {
            Console.Clear();
            Console.WriteLine(
                    board[0] + board[1] + board[2] + "\n" +
                    board[3] + board[4] + board[5] + "\n" +
                    board[6] + board[7] + board[8]
                );           
        }

    }

}
