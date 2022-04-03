using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New EP", menuName ="EnemyMove")]
public class EnemyStats : ScriptableObject
{
    public string profilename;
    //punch1, punch2, punch3, kick1, kick2
    public int punch1;
    public int punch2;
    public int punch3;
    public int kick1;
    public int kick2;

    public int[] GetTotal()
    {
        int[] arr = new int[6];
        arr[0] = punch1;
        arr[1] = punch2;
        arr[2] = punch3;
        arr[3] = kick1;
        arr[4] = kick2;
        arr[5] = punch1 + punch2 + punch3 + kick1 + kick2;
        return (arr);
    }
    /*Dictionary<string,int> moves = new Dictionary<string, int>();
    public void AddEntries()
    {
        moves.Add("Punch1", 8);
        moves.Add("Punch2", 5);
        moves.Add("Punch3", 3);
        moves.Add("Kick1", 3);
        moves.Add("Kick2", 1);
    }
    public int GetTotal()
    {
        int total = 0;
        foreach(KeyValuePair<string,int> entry in moves)
        {
            total += entry.Value;
        }
        return (total);
    }*/
}
