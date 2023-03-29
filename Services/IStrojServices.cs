using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppDapper.Models;

namespace Services
{
    public interface IStrojServices
    {
        public IEnumerable<Stroj> GetStrojs();

        public void CreateStrojs(Stroj stroj);
        public ViewModel GetStrojById(int id);

        public void UpdateKvar(int id, Stroj stroj);
        public void DeleteKvar(int id);

        public Stroj GetByIdUpdate(int id);
    }
}
