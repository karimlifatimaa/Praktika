using DoorangApp.Core.Models;
using DoorangApp.Core.RepositoryAbstracts;
using DoorangApp.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorangApp.Data.RepositiryConcretes
{
    public class ExplorerRepository : GenericRepository<Explorer>, IExplorerRepository
    {
        public ExplorerRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
