using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AttackSound
{
    public int Id { get; set; }
    public string Caption { get; set; }
    public string File { get; set; }

    public AttackSound(int id, string caption, string file)
    {
        Id = id;
        Caption = caption;
        File = file;
    }

    public static AttackSound AtkSound1 = new AttackSound(1, "AtkSound1", "sad_01.wav");
    public static AttackSound AtkSound2 = new AttackSound(2, "AtkSound2", "sae_07.wav");
    public static AttackSound AtkSound3 = new AttackSound(3, "AtkSound3", "sae_10.wav");

    public override string ToString()
    {
        return Caption;
    }

    private static List<AttackSound> _defs = new List<AttackSound>();

    public static List<AttackSound> GetDefs()
    {
        if (_defs.Count == 0)
        {
            _defs.Add(AtkSound1);
            _defs.Add(AtkSound2);
            _defs.Add(AtkSound3);
        }

        return _defs;
    }

    public static AttackSound Get(int id)
    {
        List<AttackSound> list = GetDefs();
        foreach (var item in list)
        {
            if (item.Id == id)
            {
                return item;
            }
        }
        return list[0];
    }
}