using System;
using System.Collections.Generic;
using webapi.Models;
using System.Linq;
namespace webapi.Services
{
    public class CountryRepository : ICountryRepository
    {
        private BookDbContext _countryContext;
        public CountryRepository(BookDbContext countryContext)
        {
            _countryContext = countryContext;
        }
        public ICollection<Author> GetAuthorsFromACountry(int countryId){
            return _countryContext.Authors.Where(c=>c.Country.Id == countryId).ToList();
        }
        public ICollection<Country> GetCountries(){
            return _countryContext.Countries.OrderBy(c=>c.Name).ToList();
        }
        public Country GetCountry(int countryId){
            return _countryContext.Countries.Where(c=>c.Id == countryId).FirstOrDefault();
        }
        public Country GetCountryOfAuthor(int authorId){
            return _countryContext.Authors.Where(a=>a.Id == authorId).Select(c=> c.Country).FirstOrDefault();
        }
        public bool CountryExists(int countryId){
            return _countryContext.Countries.Any(c=>c.Id == countryId);
        }
        public bool IsDuplicateCountryName(int countryId, string countryName){
            var country=_countryContext.Countries.Where(c=>c.Name.Trim().ToUpper() == countryName.Trim().ToUpper() && c.Id != countryId).FirstOrDefault();
            if(country == null)
                return false;
            return true;
        }

        public bool CreateCountry(Country country){
            _countryContext.Add(country);
            return Save();
        }
        public bool UpdateCountry(Country country){
            _countryContext.Update(country);
            return Save();
        }
        public bool DeleteCountry(Country country){
            _countryContext.Remove(country);
            return Save();
        }
        public bool Save(){
            if(_countryContext.SaveChanges()>0)
                return true;
            return false;
        }
    }
}