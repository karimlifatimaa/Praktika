using DoorangApp.Business.Exceptions;
using DoorangApp.Business.Services.Abstracts;
using DoorangApp.Core.Models;
using DoorangApp.Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorangApp.Business.Services.Concretes
{
    public class ExplorerServices : IExplorerServices
    {
        private readonly IExplorerRepository _explorerRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ExplorerServices(IExplorerRepository explorerRepository, IWebHostEnvironment webHostEnvironment)
        {
            _explorerRepository = explorerRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void AddExplorer(Explorer explorer)
        {
            if(explorer == null) throw new NullReferenceException();
            if(explorer.PhotoFile == null) throw new NullReferenceException();
            if (!explorer.PhotoFile.ContentType.Contains("image/"))
            {
                throw new FileContentTypeException("PhotoFile","Content type error!!!");
            }
            if(explorer.PhotoFile.Length> 2097152)
            {
                throw new FileSizeException("PhotoFile", "Size error!!!");
            }
            string path = _webHostEnvironment.WebRootPath + @"\Uploads\Explorer\" + explorer.PhotoFile.FileName;
            using(FileStream stream = new FileStream(path, FileMode.Create))
            {
                explorer.PhotoFile.CopyTo(stream);
            }
            explorer.ImgUrl = explorer.PhotoFile.FileName;
            _explorerRepository.Add(explorer);
            _explorerRepository.Commit();
        }

        public void DeleteExplorer(int id)
        {
            var item=_explorerRepository.Get(x => x.Id == id);
            if (item == null) throw new NullReferenceException();
            string path = _webHostEnvironment.WebRootPath + @"\Uploads\Explorer\" + item.ImgUrl;
            if(!File.Exists(path)) throw new FileNameNotFoundException("ImgUrl","ImageUrl not found");
            File.Delete(path);
            _explorerRepository.Delete(item);
            _explorerRepository.Commit();

        }

        public List<Explorer> GetAllExplorer(Func<Explorer, bool>? func = null)
        {
            return _explorerRepository.GetAll(func);
        }

        public Explorer GetExplorer(Func<Explorer, bool>? func = null)
        {
            return _explorerRepository.Get(func);
        }

        public void UpdateExplore(int id, Explorer explorer)
        {
            var exitExplorer = _explorerRepository.Get(x => x.Id == id);
            if (exitExplorer == null) throw new NullReferenceException();
            if(explorer.PhotoFile != null)
            {
                if(!explorer.PhotoFile.ContentType.Contains("image/")) throw new FileContentTypeException("PhotoFile", "Content type error!!!");
                if(explorer.PhotoFile.Length> 2097152) throw new FileSizeException("PhotoFile", "Size error!!!");
                string path =_webHostEnvironment.WebRootPath +@"\Uploads\Explorer\"+explorer.PhotoFile.FileName;
                using(FileStream stream =new FileStream(path, FileMode.Create))
                {
                    explorer.PhotoFile.CopyTo(stream);
                }
                exitExplorer.ImgUrl = explorer.PhotoFile.FileName;

            }
            exitExplorer.Title= explorer.Title;
            exitExplorer.Subtitle= explorer.Subtitle;
            exitExplorer.Description= explorer.Description;
            _explorerRepository.Commit();

        }
    }
}
