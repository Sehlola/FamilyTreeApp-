using FamilyTreeApp.Data;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeApp.Data
{
    public class FamilyService
    {
        private readonly FamilyDbContext _context;

        public FamilyService(FamilyDbContext context)
        {
            _context = context;
        }

        // Get person and children recursively
        public async Task<Person?> GetPersonTreeAsync(int id)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);

            if (person != null)
            {
                person.Children = await _context.Persons
                    .Where(c => c.ParentId == person.Id)
                    .ToListAsync();

                foreach (var child in person.Children)
                {
                    child.Children = (await GetPersonTreeAsync(child.Id))?.Children ?? new List<Person>();
                }
            }

            return person;
        }

        public async Task UpdatePersonAsync(Person person)
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
        }
    }
}
