using System;
using System.Collections.Generic;
using System.Linq;
using Facilist.TestApp1.Models;

namespace Facilis.TestApp1
{
    internal class Program
    {
        private static void Main()
        {
            // Problem: Write a GENERIC EXTENSION METHOD that will Convert a flat list of items into a Hierarchy
            // Populating Parent and Children Properties of Item class
            // The ItemId, ParentId, Parent and Children properties needs to be provided as an input to the Extension Method to make the extension
            // re-usable for different cases.


            // In the following example, the Root Item always have the ParentId set to 0

            var parent1 = new Item(1, "I am first Parent (Root Item)", 0);
            var child1OfParent1 = new Item(2, "I am first Child of Parent1", 1);
            var child2OfParent1 = new Item(3, "I am Second Child of Parent1", 1);
            var child3OfParent1 = new Item(4, "I am Third Child of Parent1", 1);
            var grandChild1OfChild1OfParent1 = new Item(5, "I am the first Child of Child1OfParent1", 2);

            var parent2 = new Item(6, "I am Second Parent (Root Item)", 0);
            var child1OfParent2 = new Item(7, "I am first Child of Parent2", 6);
            var grandChild1OfChild1OfParent2 = new Item(8, "I am the first Child of Child1OfParent2", 7);

            var items = new List<Item>
            {
                parent1,
                child1OfParent1,
                child2OfParent1,
                child3OfParent1,
                grandChild1OfChild1OfParent1,
                parent2,
                child1OfParent2,
                grandChild1OfChild1OfParent2
            };

            //******************************************** HERE IS GENERIC EXTENSION CALL ***********************************
            // Pass to the hierarchy GENERIC extension method the lambda functions with comparison actions.
            // (This is since it the names for Parent/Children model properties change in a generic method and should be UNKNOWN to the method)
            List<Item> results = items.SetHierarchy(item => item.Id, item => item.ParentId,
                   (item, parent) => item.Parent = parent,
                   (item, parent) => parent.Children.Add(item)).ToList();

            // Print all the names of the items
            PrintItems(results);

            Console.Read();
        }

        /// <summary>
        /// NOTE: The implementation of the method will be missing during the test and candidate will be implementing this function
        /// </summary>
        private static void PrintItems(IEnumerable<Item> items, string precedence = "-")
        {
            foreach (var item in items)
            {
                Console.WriteLine($"{precedence} {item.Name}");

                if (item.Children.Any())
                {
                    PrintItems(item.Children, precedence + "-");
                }
            }
        }
    }
}