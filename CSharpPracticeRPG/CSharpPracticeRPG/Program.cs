namespace CSharpPracticeRPG
{
    public class Player
    {
        public int Health;
        public int Damage;
        public int Crit;
        public int Defense;

        public Player (int health, int damage, int crit, int defense) //must be the same name as class
        {
            this.Health = health;
            this.Damage = damage;
            this.Crit = crit;
            this.Defense = defense;
        }
        public void TakeDamage (int dmgAmount)
        {
            this.Health = this.Health - (dmgAmount - this.Defense);
            Console.WriteLine($"Player takes {dmgAmount} damage, remaining HP: {this.Health}");
        }
        public void Heal ()
        {
            int healingHP = 80;
            if(healingHP + this.Health > 400)
            {
                this.Health = this.Health + (400 - this.Health);
            }
            else
            {
                this.Health += healingHP;
            }
        }
        public void Attack (BossEnemy target)
        {
            Random random = new Random();
            int attackDmgModifier = random.Next(0, 3);
            int attackDmg = 64;
            if (attackDmgModifier == 0)
            {
                attackDmg +=0;
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
    }
    public class BossEnemy
    {
        public int Health;
        public int Damage;
        public int Defense;

        public BossEnemy (int health, int damage, int defense)
        {
            this.Health = health;
            this.Damage = damage;
            this.Defense = defense;
        }
        public void TakeDamage(int attackDmg)
        {
            this.Health = this.Health - (attackDmg - this.Defense / 2);
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Player newPlayer = new Player(400, 64, 4, 30);
            BossEnemy newBoss = new BossEnemy(3000, 100, 10);
        }
    }
}
//TOMORROW: ADD A CRIT SYSTEM, ADD AN ATTACK SYSTEM !important