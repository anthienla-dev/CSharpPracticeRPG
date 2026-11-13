
namespace CSharpPracticeRPG
{
    internal class Program
    {
        public static int ReadStat(string prompt)
        {
            bool isValidStat = false;
            int stat = 0; //Initialize the stat
            while (isValidStat == false)
            {
                Console.Write(prompt);
                isValidStat = int.TryParse(Console.ReadLine(), out stat);
                //remember: stat exists only in the scope of the While loop. There's no Update to help you here
                if(isValidStat == false)
                {
                    Console.WriteLine("Format error. Please try again: "); //Error log
                }
            }
            return stat; //Scope!
        }
        public static int Attack (int defense, int attackPower) //Damage dealt
        {
            attackPower = attackPower - defense;
            if (attackPower < 0)
            {
                attackPower = 1;
            }
            return attackPower;
        }
        static void Main(string[] args)
        {
            string askForHostileHealth = "Enter your enemy's health: ";
            string askForHostileDefense = "Enter your enemy's defense: ";
            string askForAttackPower = "How much damage does your attack deal: ";
            int enemyHealth = ReadStat(askForHostileHealth); //So we don't need to write the read script for both health and defense (DRY)
            int enemyDefense = ReadStat(askForHostileDefense); 
            int playerAttackPower = ReadStat(askForAttackPower);
            int damageDealt = Attack( enemyDefense, playerAttackPower);
            Console.WriteLine($"The enemy has {enemyHealth} HP" );
            Console.WriteLine($"The enemy has {enemyDefense} Def");
            Console.WriteLine($"Your attack deals {damageDealt} Dmg");
        }
    }
}
