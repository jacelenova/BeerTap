using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerTap.Domain.Repository;
using System.Web;

namespace BeerTap.Domain.Service
{
    public interface IKegService : IBeerTapService
    {
        Keg GetKegById(int id);
        Keg GetKeg(int id, int officeId);
        IQueryable<Keg> GetAll();
        Keg Add(string name, int officeId);
        Keg Update(int id, string name, int content);
        IQueryable<Keg> GetKegsByOffice(int id);
        int GetBeer(int id, int officeId, int size);
        Keg ReplaceKeg(int officeId, int id);
    }

    public class KegService : IKegService
    {
        private KegRepository kegRepository = new KegRepository();
        public Keg GetKegById(int id)
        {
            var query = kegRepository.GetById(id);

            return query;
        }

        public Keg GetKeg(int id, int officeId)
        {
            var query = kegRepository.FindBy(k => k.Id == id && k.OfficeId == officeId).SingleOrDefault();

            return query;;
        }

        public IQueryable<Keg> GetAll()
        {
            var query = kegRepository.GetAll();

            return query;
        }

        public Keg Add(string name, int officeId)
        {
            var query = kegRepository.Add(new Keg() { Name = name, OfficeId = officeId });
            kegRepository.Save();

            return query;
        }

        public Keg Update(int id, string name, int content)
        {
            var keg = kegRepository.GetById(id);
            keg.Name = name;
            keg.Content = content;

            kegRepository.Edit(keg);
            kegRepository.Save();

            return keg;
        }

        public IQueryable<Keg> GetKegsByOffice(int id)
        {
            return kegRepository.GetKegsByOffice(id);
        }

        public int GetBeer(int id, int officeId, int size)
        {
            var ret = size;
            //var keg = kegRepository.GetById(id);
            var keg = kegRepository.FindBy(k => k.Id == id && k.OfficeId == officeId).SingleOrDefault();
            if (keg == null) return -1;

            if (keg.Content < size)
            {
                ret = keg.Content;
                keg.Content = 0;
            }
            else keg.Content = keg.Content - size;

            kegRepository.Edit(keg);
            kegRepository.Save();

            return ret;
        }

        public Keg ReplaceKeg(int officeId, int id)
        {
            var keg = kegRepository.GetById(id);
            keg.Content = Keg.MaxContent;

            kegRepository.Edit(keg);
            kegRepository.Save();

            return keg;
        }
        
    }
}
