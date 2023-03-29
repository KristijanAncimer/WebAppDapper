using Npgsql.Replication.PgOutput.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppDapper.Models;

namespace Services
{
    public interface IKvarServices
    {
        public IEnumerable<Kvar>GetKvars(); 

        void CreateKvar(Kvar kvar);
        Kvar GetKvarById(int id);

        void UpdateKvar(int id, Kvar kvarovi);
        void DeleteKvar(int id);
    }
}
