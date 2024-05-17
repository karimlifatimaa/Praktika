using DoorangApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorangApp.Business.Services.Abstracts
{
    public interface IExplorerServices
    {
        void AddExplorer(Explorer explorer);
        void DeleteExplorer(int id);
        void UpdateExplore(int id, Explorer explorer);
        Explorer GetExplorer(Func<Explorer, bool>? func = null);
        List<Explorer> GetAllExplorer(Func<Explorer, bool>? func = null);

    }
}
