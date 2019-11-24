using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public string name;
    public string combo_string;
    public int move_id;
    public int target_zone; // 0: N/A, 1: HIGH, 2: LOW
    public int damage;
    public bool is_attack;
    public bool is_block;
    public bool is_special;
}

public static class Moves
{
    public static List<Move> all_moves = new List<Move>();

    static Moves()
    {
        // Default move
        Move passive = new Move();
        passive.name = "passive";
        passive.combo_string = "";
        passive.move_id = 0;
        passive.target_zone = 0;
        passive.damage = 0;
        passive.is_attack = false;
        passive.is_special = false;
        passive.is_block = false;
        all_moves.Add(passive);

        ///////////////////////////////////////////////////////////////////////
        //////////////////////////// Special Moves ////////////////////////////
        ///////////////////////////////////////////////////////////////////////

        // SP 1
        Move sp1 = new Move();
        sp1.name = "special 1";
        sp1.combo_string = "";
        sp1.move_id = 1;
        sp1.target_zone = 0;
        sp1.damage = 10;
        sp1.is_attack = true;
        sp1.is_special = true;
        sp1.is_block = false;
        all_moves.Add(sp1);

        // SP 2
        Move sp2 = new Move();
        sp2.name = "special 2";
        sp2.combo_string = "";
        sp2.move_id = 2;
        sp2.target_zone = 0;
        sp2.damage = 10;
        sp2.is_attack = true;
        sp2.is_special = true;
        sp2.is_block = false;
        all_moves.Add(sp2);

        // SP 3
        Move sp3 = new Move();
        sp3.name = "special 3";
        sp3.combo_string = "";
        sp3.move_id = 3;
        sp3.target_zone = 0;
        sp3.damage = 10;
        sp3.is_attack = true;
        sp3.is_special = true;
        sp3.is_block = false;
        all_moves.Add(sp3);

        //// SP 4
        Move sp4 = new Move();
        sp4.name = "special 4";
        sp4.combo_string = "";
        sp4.move_id = 4;
        sp4.target_zone = 0;
        sp4.damage = 10;
        sp4.is_attack = true;
        sp4.is_special = true;
        sp4.is_block = false;
        all_moves.Add(sp4);

        //// SP 5
        Move sp5 = new Move();
        sp5.name = "special 5";
        sp5.combo_string = "";
        sp5.move_id = 5;
        sp5.target_zone = 0;
        sp5.damage = 10;
        sp5.is_attack = true;
        sp5.is_special = true;
        sp5.is_block = false;
        all_moves.Add(sp5);

        //// MSP 6
        Move sp6 = new Move();
        sp6.name = "special 6";
        sp6.combo_string = "";
        sp6.move_id = 6;
        sp6.target_zone = 0;
        sp6.damage = 10;
        sp6.is_attack = true;
        sp6.is_special = true;
        sp6.is_block = false;
        all_moves.Add(sp6);

        //// SP 7
        Move sp7 = new Move();
        sp7.name = "special 7";
        sp7.combo_string = "";
        sp7.move_id = 7;
        sp7.target_zone = 0;
        sp7.damage = 10;
        sp7.is_attack = true;
        sp7.is_special = true;
        sp7.is_block = false;
        all_moves.Add(sp7);

        //// SP 8
        Move sp8 = new Move();
        sp8.name = "special 8";
        sp8.combo_string = "";
        sp8.move_id = 8;
        sp8.target_zone = 0;
        sp8.damage = 10;
        sp8.is_attack = true;
        sp8.is_special = true;
        sp8.is_block = false;
        all_moves.Add(sp8);

        //// SP 9
        Move sp9 = new Move();
        sp9.name = "special 9";
        sp9.combo_string = "";
        sp9.move_id = 9;
        sp9.target_zone = 0;
        sp9.damage = 10;
        sp9.is_attack = true;
        sp9.is_special = true;
        sp9.is_block = false;
        all_moves.Add(sp9);

        //// SP 10
        Move sp10 = new Move();
        sp10.name = "special 10";
        sp10.combo_string = "";
        sp10.move_id = 10;
        sp10.target_zone = 0;
        sp10.damage = 10;
        sp10.is_attack = true;
        sp10.is_special = true;
        sp10.is_block = false;
        all_moves.Add(sp10);

        ///////////////////////////////////////////////////////////////////////
        ////////////////////////// End Special Moves //////////////////////////
        ///////////////////////////////////////////////////////////////////////

        /* Normal moves, only moves that affect dmg calc, otherwise assigned passive */

        Move high_punch = new Move();
        high_punch.name = "high punch";
        high_punch.combo_string = "p";
        high_punch.move_id = 11;
        high_punch.target_zone = 1;
        high_punch.damage = 10;
        high_punch.is_attack = true;
        high_punch.is_special = false;
        high_punch.is_block = false;
        all_moves.Add(high_punch);

        Move low_punch = new Move();
        low_punch.name = "low punch";
        low_punch.combo_string = "p";
        low_punch.move_id = 12;
        low_punch.target_zone = 2;
        low_punch.damage = 10;
        low_punch.is_attack = true;
        low_punch.is_special = false;
        low_punch.is_block = false;
        all_moves.Add(low_punch);

        Move high_kick = new Move();
        high_kick.name = "high kick";
        high_kick.combo_string = "k";
        high_kick.move_id = 13;
        high_kick.target_zone = 1;
        high_kick.damage = 10;
        high_kick.is_attack = true;
        high_kick.is_special = false;
        high_kick.is_block = false;
        all_moves.Add(high_kick);

        Move low_kick = new Move();
        low_kick.name = "low kick";
        low_kick.combo_string = "k";
        low_kick.move_id = 14;
        low_kick.target_zone = 2;
        low_kick.damage = 10;
        low_kick.is_attack = true;
        low_kick.is_special = false;
        low_kick.is_block = false;
        all_moves.Add(low_kick);

        Move high_block = new Move();
        high_block.name = "high block";
        high_block.combo_string = "b";
        high_block.move_id = 15;
        high_block.target_zone = 1;
        high_block.damage = 10;
        high_block.is_attack = false;
        high_block.is_special = false;
        high_block.is_block = true;
        all_moves.Add(high_block);

        Move low_block = new Move();
        low_block.name = "low block";
        low_block.combo_string = "b";
        low_block.move_id = 16;
        low_block.target_zone = 2;
        low_block.damage = 10;
        low_block.is_attack = false;
        low_block.is_special = false;
        low_block.is_block = true;
        all_moves.Add(low_block);
    }
}