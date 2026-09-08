using System.Security.Cryptography.X509Certificates;

namespace CSharpPracticeRPG
{
    public class Player
    {
        public int Health;
        public int Damage;
        public int Crit;
        public int Defense;
        public int burningTickCheck;
        public int burningDmgPerTick;

        public Player(int health, int damage, int crit, int defense) //must be the same name as class
        {
            this.Health = health;
            this.Damage = damage;
            this.Crit = crit;
            this.Defense = defense;
            this.burningTickCheck = 0;
            this.burningDmgPerTick = 30;
        }
        public void TakeDamage(int dmgAmount)
        {
            this.Health = this.Health - (dmgAmount - this.Defense);
            Console.WriteLine($"Player takes {dmgAmount} damage, remaining HP: {this.Health}");
        }
        public void Heal()
        {
            int healingHP = 80;
            if (healingHP + this.Health > 400)
            {
                int actualHealingHP = 400 - this.Health;
                this.Health = this.Health + (actualHealingHP);
                Console.WriteLine($"You healed for {actualHealingHP}HP");
            }
            else
            {
                this.Health += healingHP;
                Console.WriteLine($"You healed for {healingHP}HP");
            }
        }
        public void Attack(BossEnemy target)
        {
            Random random = new Random();
            int attackDmgModifier = random.Next(0, 3);
            int attackDmg = 64;
            if (attackDmgModifier == 0)
            {
                attackDmg += 0;
            }
            else if (attackDmgModifier == 1)
            {
                attackDmg = attackDmg + (64 * 10 / 100);
            }
            else if (attackDmgModifier == 2)
            {
                attackDmg = attackDmg - (64 * 10 / 100);
            }
            target.TakeDamage(attackDmg); //No need for "int"
        }
        public void isBurningDamage() //BURNING TICK CHECK IS SET TO 3 WHEN PLAYER GOT ATTACKED, THEN COUNT DOWN TO 0 EACH COUNT DEALS DAMAGE
        {
            if (burningTickCheck > 0)
            {
                this.Health -= 30;
                Console.WriteLine($"You take {this.burningDmgPerTick} DMG");
                this.burningTickCheck--;
            }
            else
            {
                this.Health -= 0;
            }

        }
    }
    public class BossEnemy
    {
        public int Health;
        public int Damage;
        public int Defense;

        public BossEnemy(int health, int damage, int defense)
        {
            this.Health = health;
            this.Damage = damage;
            this.Defense = defense;
        }
        public void TakeDamage(int attackDmg)
        {
            this.Health = this.Health - (attackDmg - this.Defense / 2);
            Console.WriteLine($"The enemy takes {attackDmg} Dmg. {this.Health}HP left");
        }
        public void BossAttack(Player target)
        {
            Random dice = new Random();
            int bossAtkChoice = dice.Next(1, 4);
            int meteorStrikeDmg = 120;
            int lanceStrikeDmg = 80;
            int burningDmgPerUpdate = 30;
            if (bossAtkChoice == 1)
            {
                Console.WriteLine("The boss conjures 'Meteor Strike!");
                target.TakeDamage(meteorStrikeDmg);
            }
            if (bossAtkChoice == 2)
            {
                Console.WriteLine("The boss casts 'Dark Lance'!");
                target.TakeDamage(lanceStrikeDmg);
            }
            if (bossAtkChoice == 3)
            {
                Console.WriteLine("The boss burns the player!");
                target.Health -= burningDmgPerUpdate;
                target.burningTickCheck = 3;
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Player newPlayer = new Player(400, 64, 4, 30);
            BossEnemy newBoss = new BossEnemy(3000, 100, 10);
            Console.WriteLine($"You have {newPlayer.Health}HP, {newPlayer.Defense}DEF");
            Console.WriteLine($"A Testifier appeared! HP: {newBoss.Health}");

            while (newPlayer.Health > 0 && newBoss.Health > 0)
            {
                Console.Write("Enter an input (F = Attack, H = Heal): ");
                string keyInput = Console.ReadLine()!.ToUpper(); //The ! just makes VS shut the fuck up about null
                //Check DoT first so each turn the player takes damage first then they can attack (if burning)
                newPlayer.isBurningDamage(); //Must communicate with the instance newPlayer and not the class. And the function did the if check for you (ref above)
                if (newPlayer.Health == 0)
                {
                    Console.WriteLine("Game over");
                    break; //Game over if death by burning
                }
                if (keyInput == "F")
                {
                    newPlayer.Attack(newBoss);
                }
                else if (keyInput == "H")
                {
                    newPlayer.Heal();
                }
                if (newBoss.Health <= 0) //Win check independent from keystroke
                {
                    Console.WriteLine("You win!");
                    break;
                }
                newBoss.BossAttack(newPlayer);
                if (newPlayer.Health <= 0)
                {
                    Console.WriteLine("Game over");
                    break;
                }
            }
        }
    }
}
//Ach scheiße ich liebe C#