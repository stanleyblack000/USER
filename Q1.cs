namespace Back_End_Technical_Test_Q1
{

    class Program
    {
        static int CalculateScore(int[] numbers)
        {
            int score = 0;

            foreach (int number in numbers)
            {
                if (number % 2 == 0)
                {
                    score += 1;
                }
                else
                {
                    score += 3;
                }

                if (number == 8)
                {
                    score += 5;
                }
            }

            return score;
        }

        static void Main()
        {
            Console.WriteLine(CalculateScore(new int[] { 1, 2, 3, 4, 5 })); // 11
            Console.WriteLine(CalculateScore(new int[] { 15, 25, 35 }));   // 9
            Console.WriteLine(CalculateScore(new int[] { 8, 8 }));         // 12
        }
    }
}
