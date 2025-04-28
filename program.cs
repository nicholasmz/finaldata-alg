using System;
using System.Collections.Generic;

// README Hero's Quest: A Console Adventure Game
// Hero's Quest is a text-based adventure game where you take on the role of a hero navigating through different rooms and facing various challenges. Your goal is to survive, gain experience, collect treasures, and reach the final room.

// Hero class representing the player's attributes (Strength, Agility, Intelligence, Health, Level, XP)
public class Hero
{
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Intelligence { get; set; }
    public int Health { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }

    // Constructor for initializing hero's stats
    public Hero(int strength, int agility, int intelligence)
    {
        Strength = strength;
        Agility = agility;
        Intelligence = intelligence;
        Health = 20;  // Default Health is set to 20
        Level = 1;
        Experience = 0;
    }

    // Method to display the hero's current stats
    public void DisplayStats()
    {
        // O(1) - Constant time to display stats
        Console.WriteLine($"Strength: {Strength}, Agility: {Agility}, Intelligence: {Intelligence}, Health: {Health}, Level: {Level}, XP: {Experience}");
    }

    // Method to decrease health by a certain amount of damage
    public void TakeDamage(int damage)
    {
        // O(1) - Constant time to update health
        Health -= damage;
        if (Health < 0) Health = 0;
    }

    // Method to heal the hero
    public void Heal(int amount)
    {
        // O(1)
        Health += amount;
        if (Health > 20 + (Level - 1) * 5) Health = 20 + (Level - 1) * 5;
    }

    // Method to gain experience and level up
    public void GainExperience(int xp)
    {
        // O(1)
        Experience += xp;
        if (Experience >= Level * 10)
        {
            Experience = 0;
            Level++;
            Strength += 2;
            Agility += 2;
            Intelligence += 2;
            Health = 20 + (Level - 1) * 5;
            Console.WriteLine("\nYou leveled up! Stats increased!");
        }
    }
}

// Inventory class for managing the player's items (max 5 items)
public class Inventory
{
    private Queue<string> items;

    // Constructor to initialize the inventory with starting items
    public Inventory()
    {
        // O(1) - Queue initialization
        items = new Queue<string>();
        items.Enqueue("Sword");
        items.Enqueue("Health Potion");
    }

    // Method to display the current inventory
    public void DisplayInventory()
    {
        // O(n) - Linear in the number of items (maximum 5)
        Console.WriteLine("\nInventory:");
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }

    // Method to add an item to the inventory (if full, remove oldest)
    public void AddItem(string item)
    {
        // O(1) - Enqueue and Dequeue are constant time
        if (items.Count == 5)
        {
            items.Dequeue();
        }
        items.Enqueue(item);
    }

    // Method to use a health potion if available
    public bool UseHealthPotion(Hero hero)
    {
        // O(n) - Linear search
        if (items.Contains("Health Potion"))
        {
            List<string> temp = new List<string>(items);
            temp.Remove("Health Potion");
            items = new Queue<string>(temp);
            hero.Heal(10);
            Console.WriteLine("\nYou used a Health Potion and regained 10 Health!");
            return true;
        }
        return false;
    }
}

// Challenge class representing a challenge in the game (with a difficulty and type)
public class Challenge
{
    public int Difficulty { get; set; }
    public string Type { get; set; }

    public Challenge(int difficulty, string type)
    {
        Difficulty = difficulty;
        Type = type;
    }
}

// BST Node for storing challenges
public class BSTNode
{
    public Challenge Challenge { get; set; }
    public BSTNode Left { get; set; }
    public BSTNode Right { get; set; }

    public BSTNode(Challenge challenge)
    {
        Challenge = challenge;
        Left = Right = null;
    }
}

// Binary Search Tree for managing challenges
public class ChallengeBST
{
    public BSTNode Root { get; set; }

    public ChallengeBST()
    {
        Root = null;
    }

    public void AddChallenge(Challenge challenge)
    {
        // Average case O(log n), Worst case O(n)
        Root = AddChallenge(Root, challenge);
    }

    private BSTNode AddChallenge(BSTNode node, Challenge challenge)
    {
        // Average case O(log n)
        if (node == null) return new BSTNode(challenge);
        if (challenge.Difficulty < node.Challenge.Difficulty)
            node.Left = AddChallenge(node.Left, challenge);
        else
            node.Right = AddChallenge(node.Right, challenge);
        return node;
    }

    public BSTNode SearchChallenge(int roomNumber)
    {
        // Average case O(log n)
        return SearchChallenge(Root, roomNumber);
    }

    private BSTNode SearchChallenge(BSTNode node, int roomNumber)
    {
        // Average case O(log n)
        if (node == null) return null;
        if (roomNumber == node.Challenge.Difficulty)
            return node;
        return (roomNumber < node.Challenge.Difficulty) ? SearchChallenge(node.Left, roomNumber) : SearchChallenge(node.Right, roomNumber);
    }
}

// Graph class for managing the rooms and connections
public class Graph
{
    public Dictionary<int, string> RoomDescriptions;
    public Dictionary<int, List<int>> Connections;

    public Graph()
    {
        // O(1) - Dictionary initialization
        RoomDescriptions = new Dictionary<int, string>
        {
            { 1, "You are in a dark, cold dungeon." },
            { 2, "You are in an eerie forest clearing." },
            { 3, "You are standing before a massive cave entrance." },
            { 4, "You entered an ancient temple." },
            { 5, "You are in a misty swamp." }
        };

        // O(1)
        Connections = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 2, 3 } },
            { 2, new List<int> { 1, 4 } },
            { 3, new List<int> { 1, 5 } },
            { 4, new List<int> { 2 } },
            { 5, new List<int> { 3 } }
        };
    }

    // Display possible moves
    public void DisplayConnections(int currentRoom)
    {
        Console.WriteLine("\nYou can move to:");
        foreach (var room in Connections[currentRoom])
        {
            Console.WriteLine($"- Room {room}: {RoomDescriptions[room]}");
        }
    }
}

// Class to track visited rooms and allow backtracking
public class PathTracker
{
    private List<int> visitedRooms;

    public PathTracker()
    {
        // O(1) - List initialization
        visitedRooms = new List<int>();
    }

    public void VisitRoom(int room)
    {
        // O(n) - Checking Contains is O(n)
        if (!visitedRooms.Contains(room))
        {
            visitedRooms.Add(room);
        }
    }

    public void Backtrack()
    {
        // O(1) - Remove last
        if (visitedRooms.Count > 1)
        {
            visitedRooms.RemoveAt(visitedRooms.Count - 1);
        }
    }

    public bool IsRoomVisited(int room)
    {
        // O(n) - Linear search
        return visitedRooms.Contains(room);
    }

    public void ShowPath()
    {
        Console.WriteLine("\nVisited Rooms Path:");
        foreach (var room in visitedRooms)
        {
            Console.Write($"{room} -> ");
        }
        Console.WriteLine("END");
    }
}

// Treasure class representing different bonus items
public class Treasure
{
    public string Name { get; set; }
    public string Effect { get; set; }

    public Treasure(string name, string effect)
    {
        Name = name;
        Effect = effect;
    }

    public static Treasure GenerateTreasure()
    {
        // O(1) - Random generation
        Random rand = new Random();
        int chance = rand.Next(1, 101);
        if (chance <= 10)
        {
            int treasureType = rand.Next(1, 4);
            if (treasureType == 1) return new Treasure("Gold", "+5 Strength");
            if (treasureType == 2) return new Treasure("Ancient Tome", "+5 Intelligence");
            if (treasureType == 3) return new Treasure("Boots of Speed", "+5 Agility");
        }
        return null;
    }
}

// Main Game class
public class Game
{
    private Hero hero;
    private Inventory inventory;
    private Graph map;
    private ChallengeBST challengeTree;
    private PathTracker pathTracker;
    private Random rand;

    public Game()
    {
        hero = new Hero(5, 7, 8);
        inventory = new Inventory();
        map = new Graph();
        challengeTree = new ChallengeBST();
        pathTracker = new PathTracker();
        rand = new Random();

        challengeTree.AddChallenge(new Challenge(2, "Trap"));
        challengeTree.AddChallenge(new Challenge(3, "Combat"));
        challengeTree.AddChallenge(new Challenge(4, "Puzzle"));
        challengeTree.AddChallenge(new Challenge(5, "Boss"));
    }

    public void Play()
    {
        int currentRoom = 1;
        pathTracker.VisitRoom(currentRoom);

        while (hero.Health > 0)
        {
            Console.Clear(); // O(1)
            Console.WriteLine($"\nCurrent Room: {currentRoom}");
            Console.WriteLine(map.RoomDescriptions[currentRoom]); // O(1)
            hero.DisplayStats(); // O(1)
            inventory.DisplayInventory(); // O(n)
            pathTracker.ShowPath(); // O(n)

            // Handle challenge if any
            BSTNode challengeNode = challengeTree.SearchChallenge(currentRoom); // O(log n)
            if (challengeNode != null)
            {
                HandleChallenge(challengeNode.Challenge);
            }

            // Chance to find treasure
            Treasure treasure = Treasure.GenerateTreasure(); // O(1)
            if (treasure != null)
            {
                Console.WriteLine($"\nYou found {treasure.Name}, effect: {treasure.Effect}");
                ApplyTreasure(treasure);
            }

            if (hero.Health <= 5 && inventory.UseHealthPotion(hero))
            {
                continue; // Heal and continue
            }

            // Show possible movements
            map.DisplayConnections(currentRoom);
            Console.Write("\nChoose your next room (or 0 to backtrack): ");
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                if (choice == 0)
                {
                    pathTracker.Backtrack();
                    currentRoom = pathTracker.IsRoomVisited(1) ? 1 : 2;
                }
                else if (map.Connections[currentRoom].Contains(choice))
                {
                    currentRoom = choice;
                }
                else
                {
                    Console.WriteLine("Invalid choice! Moving randomly...");
                    currentRoom = map.Connections[currentRoom][rand.Next(map.Connections[currentRoom].Count)];
                }
            }
            else
            {
                Console.WriteLine("Invalid input! Moving randomly...");
                currentRoom = map.Connections[currentRoom][rand.Next(map.Connections[currentRoom].Count)];
            }

            pathTracker.VisitRoom(currentRoom);

            if (currentRoom == 5)
            {
                Console.WriteLine("\nYou have reached the final room!");
                break;
            }
        }

        if (hero.Health <= 0)
        {
            Console.WriteLine("\nGame Over! You perished on your journey.");
        }
        else
        {
            Console.WriteLine("\nVictory! You survived Hero's Quest!");
        }
    }

    private void HandleChallenge(Challenge challenge)
    {
        Console.WriteLine($"\nYou encountered a {challenge.Type} challenge!");

        int heroScore = (hero.Strength + hero.Agility + hero.Intelligence) / 3;
        if (challenge.Type == "Combat" && hero.Strength >= challenge.Difficulty ||
            challenge.Type == "Trap" && hero.Agility >= challenge.Difficulty ||
            challenge.Type == "Puzzle" && hero.Intelligence >= challenge.Difficulty ||
            challenge.Type == "Boss" && heroScore >= challenge.Difficulty)
        {
            Console.WriteLine("You successfully overcame the challenge!");
            hero.GainExperience(5);
        }
        else
        {
            int damage = Math.Max(1, challenge.Difficulty - heroScore);
            hero.TakeDamage(damage);
            Console.WriteLine($"You took {damage} damage!");
        }
    }

    private void ApplyTreasure(Treasure treasure)
    {
        if (treasure.Effect == "+5 Strength") hero.Strength += 5;
        if (treasure.Effect == "+5 Intelligence") hero.Intelligence += 5;
        if (treasure.Effect == "+5 Agility") hero.Agility += 5;
    }
}

// Program entry point
public class Program
{
    public static void Main(string[] args)
    {
        Game game = new Game();
        game.Play();
    }
}

//FULL VERSION , I MANAGE TO MAKE IT WORK ON A C# COMPILER 
