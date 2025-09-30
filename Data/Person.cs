using System.Collections.Generic;

namespace FamilyTreeApp.Data
{
    // Represents a person in the family tree
    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int? ParentId { get; set; } // Null if root ancestor
        public List<Person> Children { get; set; } = new List<Person>();
    }
}
