using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  Moves
{
    public class Move
    {
        public static string combo_string;
        public static int move_id;
        public static int target_zone; // 0: N/A, 1: HIGH, 2: LOW
        public static bool is_attack;
        public static bool is_block;

        public Move(string cs, int mi, int tz, bool ia, bool ib)
        {
            combo_string = cs;
            move_id = mi;
            target_zone = tz;
            is_attack = ia;
            is_block = ib;
        }
    }

    public static List<Move> all_moves = new List<Move>();

    static Moves()
    {
        all_moves.Add(new Move("", 0, 0, false, false ));
    }
}
