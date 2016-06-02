using System.Linq;
using BeerTap.Domain.Repository;

namespace BeerTap.Domain.Service
{
    public interface IOfficeService : IBeerTapService
    {
        Office GetOfficeById(int id);
        IQueryable<Office> GetAll();
        Office Add(string name);
        Office Update(int id, string name, string description);
        //void Update(Office office);
    }

    public class OfficeService : IOfficeService
    {
        private OfficeRepository officeRepository = new OfficeRepository();
        public Office GetOfficeById(int id)
        {
            //throw new NotImplementedException();
            var query = officeRepository.GetById(id);
            
            return query;
        }

        public IQueryable<Office> GetAll()
        {
            //throw new NotImplementedException();
            var query = officeRepository.GetAll();

            return query;
        }

        public Office Add(string name)
        {
            //throw new NotImplementedException();
            var query = officeRepository.Add(new Office(name));
            officeRepository.Save();

            return query;
        }

        public Office Update(int id, string name, string description)
        {
            var office = officeRepository.GetById(id);
            office.Name = name;
            officeRepository.Edit(office);
            officeRepository.Save();
            
            return office;
        }

    }
}
