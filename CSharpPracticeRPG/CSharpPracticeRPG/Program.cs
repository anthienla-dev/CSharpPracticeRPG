using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
namespace CSharpPracticeRPG
{
    public class NPC //Catch-all class for Inheritance
    {
        public string name;
        public int atkPower;
        public int hp;
        public int defense;

        public NPC(string name, int atkPower, int hp, int defense)
        {
            this.name = name;
            this.atkPower = atkPower;
            this.hp = hp;
            this.defense = defense;
        }
        public virtual void TakeDamage(int dmgAmount) => this.hp = Math.Max(0, this.hp - dmgAmount);
        public virtual void HealHp(int healAmount, int maxHP) => this.hp = Math.Min(maxHP, this.hp + healAmount);
    }
    public class Playable : NPC
    {
        public Playable(string name, int atkPower, int hp, int defense) : base(name, atkPower, hp, defense) //This is a CONSTRUCTOR that inherits from parent constructor!
        { //The innards of this constructor is inherited from Parent
        }
        public void Attack(Playable playable, Enemy enemy)
        {
            enemy.hp = Math.Max(0, enemy.hp - (playable.atkPower - enemy.defense/2)); //Mimicking Terraria defense
        }

    }
    public class Enemy :NPC
    {
        public Enemy(string name, int atkPower, int hp, int defense) : base(name, atkPower, hp, defense)
        {
        }
        public void Attack(Playable playable, Enemy enemy)
        {
            playable.hp = Math.Max(0, playable.hp - (enemy.atkPower - playable.defense));
        }
    }
    internal class Program
    {
        private static Random dice = new Random(); //For randomness.
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
        public static bool isCritCheck(int critChance)
        {
            bool isCrit = false;
            if (dice.Next(1, 101) < critChance)
            {
                isCrit = true;
            }
            return isCrit;
        }
        public static int Attack (int defense, int attackPower, int critChance) //Damage dealt
        {
            int attack;
            attack = attackPower - defense;
            if (attack < 0)
            {
                attack = 1;
            }
            if(isCritCheck(critChance)) //20% rate
            {
                attack = attack * 2;
                Console.WriteLine("CRIT!");
            }
            return attack;
        }
        public static int RemainingHealth(int health, int damageTaken)
        {
            int remainingHealth = Math.Max(0, health-damageTaken); //Shortcut for If check
            return remainingHealth;
        }
        static void Main(string[] args)  //TODO: Tier 1 Gauntlet 3!
        {
            string askForHostileHealth = "Enter your enemy's health: ";
            string askForHostileDefense = "Enter your enemy's defense: ";
            string askForAttackPower = "How much damage does your attack deal: ";
            string askForCrit = "How much crit?: ";
            int enemyHealth = ReadStat(askForHostileHealth); //So we don't need to write the read script for both health and defense (DRY)
            int enemyDefense = ReadStat(askForHostileDefense); 
            int playerAttackPower = ReadStat(askForAttackPower);
            int playerCritChance = ReadStat(askForCrit);
            int damageDealt = Attack( enemyDefense, playerAttackPower, playerCritChance);
            int remainingEnemyHealth = RemainingHealth(enemyHealth, damageDealt);
            Console.WriteLine($"The enemy has {enemyHealth} HP" );
            Console.WriteLine($"The enemy has {enemyDefense} Def");
            Console.WriteLine($"Your attack deals {damageDealt} Dmg. {remainingEnemyHealth} HP left");
        }
    }
}
