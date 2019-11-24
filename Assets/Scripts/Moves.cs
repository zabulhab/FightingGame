using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public string combo_string;
    public int move_id;
    public int target_zone; // 0: N/A, 1: HIGH, 2: LOW
    public int damage;
    public bool is_attack;
    public bool is_block;
}

public static class Moves
{
    public static List<Move> all_moves = new List<Move>();

    static Moves()
    {
        // Move 1
        Move move1 = new Move();
        move1.combo_string = "";
        move1.move_id = 0;
        move1.target_zone = 0;
        move1.damage = 10;
        move1.is_attack = false;
        move1.is_block = false;
        all_moves.Add(move1);
    }
}