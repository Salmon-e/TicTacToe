using System;
using System.Linq;
using System.Collections.Generic;

namespace TicTacToe
{
    class State
    {       
        public static int OPTIMALMAX = 1;
        public static int OPTIMALMIN = -1;        
        static int statesTested = 0;
        public string[] board = {"_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_",};
        
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
            
            for (int i = 0; i < 4; i++){
                if(board[4*i] == board[1+4*i] && board[1 + 4 * i] == board[2 + 4 * i] && board[2 + 4 * i] == board[3 + 4 * i] && board[4*i] != "_")
                {
                    return true;
                }
                if (board[i] == board[4 + i] && board[4 + i] == board[8 + i] && board[8 + i] == board[12 + i] && board[i] != "_")
                {
                    return true;
                }
            }
            if (board[0] == board[5] && board[5] == board[10] && board[10] == board[15] && board[0] != "_") return true;
            if (board[3] == board[6] && board[6] == board[9] && board[9] == board[12] && board[3] != "_") return true;
            return false;
        }
        public bool Owins()
        {
            for (int i = 0; i < 4; i++)
            {
                if (board[4 * i] == board[1 + 4 * i] && board[1 + 4 * i] == board[2 + 4 * i] && board[2 + 4 * i] == board[3 + 4 * i] && board[4 * i] != "O")
                {
                    return true;
                }
                if (board[i] == board[4 + i] && board[4 + i] == board[8 + i] && board[8 + i] == board[12 + i] && board[i] != "O")
                {
                    return true;
                }
            }
            if (board[0] == board[5] && board[5] == board[10] && board[10] == board[15] && board[0] != "O") return true;
            if (board[3] == board[6] && board[6] == board[9] && board[9] == board[12] && board[3] != "O") return true;
            return false;
        }
        public bool Xwins()
        {
            for (int i = 0; i < 4; i++)
            {
                if (board[4 * i] == board[1 + 4 * i] && board[1 + 4 * i] == board[2 + 4 * i] && board[2 + 4 * i] == board[3 + 4 * i] && board[4 * i] != "X")
                {
                    return true;
                }
                if (board[i] == board[4 + i] && board[4 + i] == board[8 + i] && board[8 + i] == board[12 + i] && board[i] != "X")
                {
                    return true;
                }
            }
            if (board[0] == board[5] && board[5] == board[10] && board[10] == board[15] && board[0] != "X") return true;
            if (board[3] == board[6] && board[6] == board[9] && board[9] == board[12] && board[3] != "X") return true;
            return false;
        }
        public int Score()
        {
            statesTested++;
            if (IsTerminal())
            {
                if (Owins())
                {
                    return -1;
                }
                else if (Xwins())
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
            for (int i = 0; i < 16; i++)
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
            for(int i = 0; i < 16; i++)
            {
                if(board[i] == "_")
                {
                    actions.Add(new Action(i, player));
                }
            }
            
            return actions;
        }
        public static int MaxValue(State state, int alpha, int beta)
        {
            if (state.IsTerminal())
            {               
                return state.Score();
            }            
            int v = int.MinValue;
            foreach (Action action in state.Actions())
            {
                int actionValue = MinValue(state.ApplyAction(action), alpha, beta);
                v = Math.Max(v, actionValue);
                alpha = Math.Max(v, alpha);
                if (beta <= alpha)
                {
                    break;
                }
            }

            return v;           
        }
        public static int MinValue(State state, int alpha, int beta)
        {
            if (state.IsTerminal())
            {                
                return state.Score();
            }
            
            int v = int.MaxValue;            
            foreach (Action action in state.Actions())
            {
                int actionValue = MaxValue(state.ApplyAction(action), alpha, beta);
                v = Math.Min(v, actionValue);
                beta = Math.Min(v, beta);

                if(beta <= alpha)
                {
                    break;
                }
            }

            return v;
        }
        public static Action NextMove(State state)
        {
            int alpha = int.MinValue;
            int beta = int.MaxValue;
            List<Action> actions = state.Actions();
            if (actions.Count == 0 || state.IsTerminal()) return null;
            string player = state.GetPlayer();
            Action bestMove = actions[0];            
            if(player == "X")
            {
                for (int i = 0; i < actions.Count; i++)
                {
                    actions[i].value = MinValue(state.ApplyAction(actions[i]), alpha, beta);
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
                    actions[i].value = MaxValue(state.ApplyAction(actions[i]), alpha, beta);                    
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
            Console.WriteLine("\n\n\n\n"+
                    "\t\t"+board[0]  + " " +  board[1] + " " +  board[2]  + " " +  board[3]  + "\n" +
                    "\t\t"+board[4]  + " " +  board[5] + " " +  board[6]  + " " + board[7]  + "\n" +
                    "\t\t"+board[8]  + " " +  board[9] + " " +  board[10] + " " + board[11] + "\n" +
                    "\t\t"+board[12] + " " + board[13] + " " + board[14]  + " " + board[15]
                );
            Console.WriteLine(statesTested);
        }

    }

}
