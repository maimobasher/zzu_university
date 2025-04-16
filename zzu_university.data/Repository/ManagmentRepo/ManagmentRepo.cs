using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using zzu_university.data.Data;
using zzu_university.data.Model;
using zzu_university.data.Repository.MainRepo;

namespace zzu_university.data.Repository.ManagmentRepo
{
    public class ManagmentRepo:Repo<Managment,int>,IManagmentRepo
    {
        private readonly ApplicationDbContext _context;

        //public ManagmentRepo(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        public async Task<Managment> GetAsync()
        {
            return await _context.Managments.FirstOrDefaultAsync();
        }

        public async Task AddAsync(Managment managment)
        {
            await _context.Managments.AddAsync(managment);
        }

        public void Update(Managment managment)
        {
            _context.Managments.Update(managment);
        }

        public void Delete(Managment managment)
        {
            _context.Managments.Remove(managment);
        }
        public ManagmentRepo(ApplicationDbContext context) : base(context) 
        { 
            _context = context;
        }
        
    }
}
