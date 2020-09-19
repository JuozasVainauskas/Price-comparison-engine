using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Comparison_Engine.Domain.Paslaugos
{
    interface DuomenuPaslauga<T>
    {
        Task<IEnumerable<T>> GautiVisus();

        Task<T> Gauti(int id);
        Task<T> Sukurti(T subjektas);
        Task<T> Atnaujinti(int id, T subjektas);
        Task<bool> Istrinti(int id);
    }
}
