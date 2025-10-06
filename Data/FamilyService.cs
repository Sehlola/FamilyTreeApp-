using Microsoft.EntityFrameworkCore;

namespace FamilyTreeApp.Data
{
    // This class handles all family data tasks like getting, adding, or deleting people
    public class FamilyService
    {
        private readonly FamilyDbContext _context; // Connects to the database

        public FamilyService(FamilyDbContext context)
        {
            _context = context;
        }

        // Get everyone in the family, sorted by name
        public async Task<List<Person>> GetAllPeopleAsync()
        {
            return await _context.Persons
                                 .OrderBy(p => p.Name)
                                 .ToListAsync();
        }

        // Get all people who have no parent (the root ancestors)
        public async Task<List<Person>> GetRootPersonsAsync()
        {
            return await _context.Persons
                                 .Where(p => p.ParentId == null)
                                 .OrderBy(p => p.Name)
                                 .ToListAsync();
        }

        // Get all direct children of one person (not including grandchildren)
        public async Task<List<Person>> GetChildrenAsync(int parentId)
        {
            return await _context.Persons
                                 .Where(p => p.ParentId == parentId)
                                 .OrderBy(p => p.Name)
                                 .ToListAsync();
        }

        // Find one person by their Id (returns null if not found)
        public async Task<Person?> GetPersonAsync(int id)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);
        }

        // Save changes for an existing person
        public async Task UpdatePersonAsync(Person person)
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
        }

        // Add a new person to the family tree
        public async Task AddPersonAsync(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
        }

        // Delete a person by their Id (does not remove children automatically)
        public async Task DeletePersonAsync(int id)
        {
            var p = await _context.Persons.FindAsync(id);
            if (p != null)
            {
                _context.Persons.Remove(p);
                await _context.SaveChangesAsync();
            }
        }
    }
}
