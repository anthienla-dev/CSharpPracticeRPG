using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
        public virtual int CalculateDamage(NPC target)
        {
            return this.atkPower - target.defense;
        }
        public virtual void Attack(NPC target)
        {
            int damageDealt = CalculateDamage(target);
            target.hp = Math.Max(0, target.hp - damageDealt);
        }
    }
    public class Playable : NPC
    {
        public Playable(string name, int atkPower, int hp, int defense) : base(name, atkPower, hp, defense) //This is a CONSTRUCTOR that inherits from parent constructor!
        { //The innards of this constructor is inherited from Parent
        }
        public override int CalculateDamage(NPC target) //Change the calc 
        {
            return Math.Max(0, this.atkPower - target.defense/2);
        }

    } //TODO: Make a run battle function
    public class Enemy :NPC
    {
        public Enemy(string name, int atkPower, int hp, int defense) : base(name, atkPower, hp, defense)
        {
        }
    }
    public class Item
    {
        public string name;
        public int amount;

        public Item(string name, int amount)
        {
            this.name = name;
            this.amount = amount;
        }


    }
    public class Inventory //Because Inventory manages stuffs and is important
    {
        public List<Item> inventory;
        public int MaxCapacity;
        
        public Inventory(int capacity) //Inventory already exists in the Class and should not be edited, so dont put it as Para
        {
            this.MaxCapacity = capacity;
            this.inventory = new List<Item>(); //TODO: continue the exercise
        }

        public void AddItem (Item item,  int amount)
        {
            for(int i = 0; i < amount; i++)
            {
                inventory.Add(item);    
            }
        }
        public Item findItem(string itemName)
        {
            Item foundObject = null;
            foreach(Item item in inventory)
            {
                if(itemName == item.name)
                {
                    foundObject = item;
                }
            }
            return foundObject;
        }
        public void RemoveItem(string itemName) //Search by item name!
        {
            Item foundObject = findItem(itemName);
            inventory.Remove(foundObject);
        }
        public void ReadList()
        {
            foreach(Item i in inventory)
            {
                Console.WriteLine(i.name);
            }
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
        public static void RunBattle(Playable player, Enemy enemy)
        {
            int roundCount = 1;
            while(player.hp > 0 && enemy.hp >0)
            {
                Console.WriteLine($"Round {roundCount}");
                Console.WriteLine("Choose your action, h= heal, a = attack:  ");
                string choice = Console.ReadLine().ToLower() ; //This is a flawed decision, but enough for a beginner
                if(choice == "h")
                {
                    player.HealHp(250, 500);
                    Console.WriteLine($"You healed 250HP, your current HP is {player.hp}");
                }
                if(choice == "a")
                {
                    player.Attack(enemy);
                    Console.WriteLine($"You attacked for {player.CalculateDamage(enemy)} DMG. The enemy's HP is currently {enemy.hp} HP");
                    if (enemy.hp <= 0)
                    {
                        Console.WriteLine("You won!");
                        break;
                    }

                }
                enemy.Attack(player);
                Console.WriteLine($"The enemy attacked for {enemy.CalculateDamage(player)} DMG. Your HP is currently {player.hp}");
                if (player.hp <= 0)
                {
                    Console.WriteLine("You lose!");
                    break;
                }
                roundCount++;

            }
        }
        static void Main(string[] args) 
        {
            Playable player = new Playable("Lorem Ipsum", 150, 500, 20);
            Enemy slime = new Enemy("Suspicious Slime", 90, 1000, 15);
            //RunBattle(player, slime);
            Inventory playerInventory = new Inventory(60); //Magic capacity number to test
            Item sword = new Item("Sword", 1);
            Item potion = new Item("Potion", 2);
            playerInventory.AddItem(sword, 1);
            playerInventory.AddItem(potion, 2);
            playerInventory.ReadList();
            playerInventory.RemoveItem("Potion");
            playerInventory.ReadList();

        }
    }
}
