using UnityEngine;

public static class Randomizer
{
    public static int UsingDice()
    {
        return Random.Range(1, 7);
    }

    public static int Using(int[] numbersToUse)
    {
        int index = Random.Range(0, numbersToUse.Length);
        return numbersToUse[index];
    }
}
