using UnityEngine;

public class ArrayPractice : MonoBehaviour
{
    private int[] lottoNumbers = { 1, 2, 3, 4, 5, 6 };
    private int myNumber;

    private void Start()
    {
        //for(int i = 0; i < 7; i++)
        //{
        //    lottoNumbers[i] = i;
        //}
    }
    private void Update()
    {
        myNumber = lottoNumbers[Random.Range(0,lottoNumbers.Length)];
        Debug.Log($"My Lotto Number is: {myNumber}.");
    }
}
