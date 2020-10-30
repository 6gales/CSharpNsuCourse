using System;

namespace RandomNumberGuesser
{
    static class Program
    {
        private const string ExitCommand = "q";

        private static bool TryReadNumber(Predicate<int> isValid, out int num)
        {
            num = default;
            
            while (true)
            {
                var line = Console.ReadLine();
                if (line == ExitCommand)
                {
                    Console.WriteLine("Bye bye");
                    return false;
                }

                if (int.TryParse(line, out num) && isValid.Invoke(num))
                {
                    return true;
                }
                Console.WriteLine("Invalid input, try again");
            }
        }
        
        static void Main(string[] args)
        {
            var rng = new Random();
            
            while (true)
            {
                Console.WriteLine($"Insert upper bound, or {ExitCommand} to exit");

                if (!TryReadNumber(n => n > 1, out var upperBound))
                {
                    return;
                }

                var secretNumber = rng.Next(upperBound);
                var maxTries = (int)Math.Log2(upperBound);
                
                Console.WriteLine($"Try to guess number from 0 to {upperBound} in {maxTries} tries, press {ExitCommand} to quit:");

                var guessed = false;
                for (var tries = 0; tries < maxTries; tries++)
                {
                    if (!TryReadNumber(n => n >= 0 && n < upperBound, out var userNum))
                    {
                        return;
                    }

                    if (userNum == secretNumber)
                    {
                        guessed = true;
                        tries++;
                        var ending = tries switch
                        {
                            1 => "try",
                            _ => "tries"
                        };
                        Console.WriteLine($"Congrats, you've guessed secret number in {tries} {ending}!");
                        break;
                    }

                    if (userNum < secretNumber)
                    {
                        Console.WriteLine("Secret number is greater");
                    }
                    if (userNum > secretNumber)
                    {
                        Console.WriteLine("Secret number is lower");
                    }
                }

                if (!guessed)
                {
                    Console.WriteLine($"Pity, secret number was {secretNumber}, better luck next time");
                }
            }
        }
    }
}