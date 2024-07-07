using Photosoil.Core.Models;
using Photosoil.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photosoil.Service.Services
{
    public class NewsService
    {
        private readonly ApplicationDbContext _context;

        public NewsService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public News GetById(int Id)
        {
            return _context.News.FirstOrDefault(x => x.Id == Id);
        }

        public void Post(News news)
        {
            try
            {
                _context.News.Add(news);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                throw new ArgumentException(e.Message);
            }

        }
    }
}
