using System;
using System.Collections.Generic;

[Serializable]
public class EnemyData
{
    public int key;
    public string name;
    public int maxHealth;
    public int level;
    public Attack[] Attacks;

    public Attack getNextAttack()
    {
        //Grak�tesnio b�do nesugalvojau kad gra�inti random attack kai jos �ansas weighted, o svori� suma ne pastovi
        List<int> weights = new List<int>();
        int sum = 0;
        foreach(Attack attack in Attacks)
        {
            sum += attack.weight;
        }

        float position = UnityEngine.Random.value * sum;
        sum = 0;
        foreach (Attack attack in Attacks)
        {
            sum += attack.weight;
            if (position <= sum)
            {
                return attack;
            }
        }
        return Attacks[Attacks.Length - 1];
    }
}
