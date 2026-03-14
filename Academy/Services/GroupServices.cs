using Academy.Context;
using Academy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Services;

public class GroupServices
{
    private readonly AcademyDbContext _context;
    public async Task<Group> AddGroup(string name)
    {
        Group group = new Group()
        {
            Name = name,
        };
        await _context.AddAsync(group);
        await _context.SaveChangesAsync();
        return group;
    }
}
