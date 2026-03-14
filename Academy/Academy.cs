using Academy.Entities;
using Academy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy
{
    class Academy(GroupServices _service)
    {
        public async Task<bool> AddGroup()
        {
            try
            {
                string name = Console.ReadLine() ?? "";
                await _service.AddGroup(name);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}