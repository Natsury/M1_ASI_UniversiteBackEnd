using UniversiteDomain.Entities;

namespace UniversiteDomain.DataAdapters;

public interface IUeRepository : IRepository<Ue>
{
    Task<Parcours> AddParcoursAsync(Ue us, Parcours parcours);
    Task<Parcours> AddParcoursAsync(long idUe, long idParcours);
    Task<Parcours> AddParcoursAsync(Ue ? ue, List<Parcours> parcoursList);
    Task<Parcours> AddParcoursAsync(long idUe, long[] idParcours);
}