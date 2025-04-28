using System;
using System.Collections.Generic;

// README Hero's Quest: A Console Adventure Game
// Hero's Quest is a text-based adventure game where you take on the role of a hero navigating through different rooms.

// Hero class representing the player's attributes (Strength, Agility, Intelligence, Health)
public class Hero
{
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Intelligence { get; set; }
    public int Health { get; set; }

    // Constructor for initializing hero's stats
    public Hero(int strength, int agility, int intelligence)
    {
        Strength = strength;
        Agility = agility;
        Intelligence = intelligence;
        Health = 20;  // Default Health is set to 20
    }

    // Method to display the hero's current stats
    public void DisplayStats()
    {
        // O(1) - Constant time to display stats
        Console.WriteLine($"Strength: {Strength}, Agility: {Agility}, Intelligence: {Intelligence}, Health: {Health}");
    }

    // Method to decrease health by a certain amount of damage
    public void TakeDamage(int damage)
    {
        // O(1) - Constant time to update health
        Health -= damage;
        if (Health < 0) Health = 0;
    }
}

// Inventory class for managing the player's items (max 5 items)
public class Inventory
{
    private Queue<string> items;

    // Constructor to initialize the inventory with a sword and health potion
    public Inventory()
    {
        // O(1) - Queue initialization and enqueuing two items
        items = new Queue<string>();
        items.Enqueue("Sword");
        items.Enqueue("Health Potion");
    }

    // Method to display the current inventory
    public void DisplayInventory()
    {
        // O(n) - Linear in the number of items (maximum 5)
        Console.WriteLine("\nInventory: ");
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }

    // Method to add an item to the inventory (if inventory is full, it dequeues the oldest item)
    public void AddItem(string item)
    {
        // O(1) - Enqueue and Dequeue are constant time operations
        if (items.Count == 5)
        {
            items.Dequeue();
        }
        items.Enqueue(item);
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

// Binary Search Tree Node for storing challenges and organizing them based on difficulty
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

// Binary Search Tree (BST) for managing a collection of challenges in the game
public class ChallengeBST
{
    public BSTNode Root { get; set; }

    // Constructor for initializing an empty tree
    public ChallengeBST()
    {
        Root = null;
    }

    // Method to add a challenge to the BST
    public void AddChallenge(Challenge challenge)
    {
        // Average case O(log n), Worst case O(n) if unbalanced
        Root = AddChallenge(Root, challenge);
    }

    // Helper method for adding a challenge to the tree
    private BSTNode AddChallenge(BSTNode node, Challenge challenge)
    {
        // Average case O(log n), Worst case O(n)
        if (node == null) return new BSTNode(challenge);
        if (challenge.Difficulty < node.Challenge.Difficulty)
            node.Left = AddChallenge(node.Left, challenge);
        else
            node.Right = AddChallenge(node.Right, challenge);
        return node;
    }

    // Method to search for a challenge based on the room number (difficulty)
    public BSTNode SearchChallenge(int roomNumber)
    {
        // Average case O(log n), Worst case O(n)
        return SearchChallenge(Root, roomNumber);
    }

    // Helper method for searching challenges recursively in the tree
    private BSTNode SearchChallenge(BSTNode node, int roomNumber)
    {
        // Average case O(log n), Worst case O(n)
        if (node == null) return null;
        if (roomNumber == node.Challenge.Difficulty)
            return node;
        return (roomNumber < node.Challenge.Difficulty) ? SearchChallenge(node.Left, roomNumber) : SearchChallenge(node.Right, roomNumber);
    }
}

// Graph class for managing the rooms in the game and their descriptions
public class Graph
{
    public Dictionary<int, string> RoomDescriptions;

    // Constructor to initialize the room descriptions
    public Graph()
    {
        // O(1) for inserting a few items
        RoomDescriptions = new Dictionary<int, string>
        {
            { 1, "You are in a dark room." },
            { 2, "You are in a forest room." },
            { 3, "You are at the entrance of a cave." }
        };
    }
}

// PathTracker class for tracking visited rooms and handling backtracking
public class PathTracker
{
    private List<int> visitedRooms;

    // Constructor to initialize the visited rooms list
    public PathTracker()
    {
        // O(1) - List initialization
        visitedRooms = new List<int>();
    }

    // Method to mark a room as visited
    public void VisitRoom(int room)
    {
        // O(n) - Checking Contains is O(n), adding is O(1)
        if (!visitedRooms.Contains(room))
        {
            visitedRooms.Add(room);
        }
    }

    // Method to backtrack (remove the last visited room from the list)
    public void Backtrack()
    {
        // O(1) - Removing last item is constant time
        if (visitedRooms.Count > 1)
        {
            visitedRooms.RemoveAt(visitedRooms.Count - 1);
        }
    }

    // Method to check if a room has been visited
    public bool IsRoomVisited(int room)
    {
        // O(n) - Linear search
        return visitedRooms.Contains(room);
    }
}

// Treasure class to represent items like Gold, which give bonuses to the player
public class Treasure
{
    public string Name { get; set; }
    public string Effect { get; set; }

    public Treasure(string name, string effect)
    {
        Name = name;
        Effect = effect;
    }

    // Static method to randomly generate a treasure (10% chance to get Gold)
    public static Treasure GenerateTreasure()
    {
        // O(1) - Random generation and simple check
        Random rand = new Random();
        int chance = rand.Next(1, 101);
        if (chance <= 10)
        {
            return new Treasure("Gold", "+5 Strength");
        }
        return null;
    }
}

// Game class to handle the overall logic of the game (player movement, challenges, inventory, etc.)
public class Game
{
    private Hero hero;
    private Inventory inventory;
    private Graph map;
    private ChallengeBST challengeTree;
    private PathTracker pathTracker;

    // Constructor to initialize the game objects
    public Game()
    {
        // O(1) - Initialization of game components
        hero = new Hero(5, 7, 8);
        inventory = new Inventory();
        map = new Graph();
        challengeTree = new ChallengeBST();
        pathTracker = new PathTracker();

        // Adding challenges - O(log n) each insertion (average)
        challengeTree.AddChallenge(new Challenge(6, "Combat"));
        challengeTree.AddChallenge(new Challenge(5, "Trap"));
        challengeTree.AddChallenge(new Challenge(8, "Puzzle"));
    }

    // Main game loop where the player interacts with the game world
    public void Play()
    {
        int currentRoom = 1;
        pathTracker.VisitRoom(currentRoom);

        while (hero.Health > 0)
        {
            Console.Clear(); // O(1)
            Console.WriteLine("\nCurrent Room: " + currentRoom);
            Console.WriteLine(map.RoomDescriptions[currentRoom]); // O(1) dictionary lookup
            hero.DisplayStats(); // O(1)
            inventory.DisplayInventory(); // O(n), where n <= 5

            // Handle any challenges in the current room
            BSTNode challengeNode = challengeTree.SearchChallenge(currentRoom); // O(log n) average
            if (challengeNode != null)
            {
                HandleChallenge(challengeNode.Challenge);
            }

            // Randomly generate a treasure item with a 10% chance
            Treasure treasure = Treasure.GenerateTreasure(); // O(1)
            if (treasure != null)
            {
                Console.WriteLine($"You found {treasure.Name}, which boosts {treasure.Effect}");
                if (treasure.Effect == "+5 Strength")
                {
                    hero.Strength += 5;
                }
            }

            // Automatic action: randomly choose the next move
            Console.WriteLine("\nAutomatic Action:");
            Random rand = new Random();
            int choice = rand.Next(1, 4); // O(1)
            Console.WriteLine($"Choosing option {choice}");

            // Update the current room based on the choice
            if (choice == 1)
            {
                currentRoom = 2;
            }
            else if (choice == 2)
            {
                currentRoom = 3;
            }
            else if (choice == 3)
            {
                pathTracker.Backtrack(); // O(1)
                if (pathTracker.IsRoomVisited(1)) currentRoom = 1; // O(n)
                else currentRoom = 2;
            }

            pathTracker.VisitRoom(currentRoom); // O(n) (due to Contains check)

            if (currentRoom == 3) break; // Exit if at final room
        }

        if (hero.Health <= 0)
        {
            Console.WriteLine("Game Over! You couldn't survive.");
        }
        else
        {
            Console.WriteLine("Congratulations! You've reached the exit!");
        }
    }

    // Method to handle challenges (Combat, Trap, Puzzle) based on hero's stats
    private void HandleChallenge(Challenge challenge)
    {
        // O(1) - Constant time logic
        Console.WriteLine($"You encountered a {challenge.Type} challenge with difficulty {challenge.Difficulty}.");
        if (challenge.Type == "Combat" && hero.Strength >= challenge.Difficulty)
        {
            Console.WriteLine("You defeated the challenge!");
        }
        else if (challenge.Type == "Trap" && hero.Agility >= challenge.Difficulty)
        {
            Console.WriteLine("You avoided the trap!");
        }
        else if (challenge.Type == "Puzzle" && hero.Intelligence >= challenge.Difficulty)
        {
            Console.WriteLine("You solved the puzzle!");
        }
        else
        {
            int damage = challenge.Difficulty - (hero.Strength + hero.Agility + hero.Intelligence) / 3;
            if (damage > 0)
            {
                hero.TakeDamage(damage);
                Console.WriteLine($"You failed the challenge and lost {damage} health.");
            }
            else
            {
                Console.WriteLine("You managed to handle the challenge successfully.");
            }
        }
    }
}

// Program entry point to start the game
public class Program
{
    public static void Main(string[] args)
    {
        Game game = new Game();
        game.Play();
    }
}
