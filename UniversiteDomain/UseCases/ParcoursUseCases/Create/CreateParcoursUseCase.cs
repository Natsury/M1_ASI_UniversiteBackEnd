using UniversiteDomain.DataAdapters.DataAdaptersFactory;
using UniversiteDomain.Entities;
using UniversiteDomain.Exceptions.ParcoursExceptions;

namespace UniversiteDomain.UseCases.ParcoursUseCases.Create;

public class CreateParcoursUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<Parcours> ExecuteAsync(long id, string nomParcours, int anneeFormation)
    {
        var parcours = new Parcours{Id = id, NomParcours = nomParcours , AnneeFormation = anneeFormation};
        return await ExecuteAsync(parcours);
    }
    
    public async Task<Parcours> ExecuteAsync(Parcours parcours)
    {
        await CheckBusinessRules(parcours);
        Parcours pa = await repositoryFactory.ParcoursRepository().CreateAsync(parcours);
        repositoryFactory.SaveChangesAsync().Wait();
        return pa;
    }

    private async Task CheckBusinessRules(Parcours parcours)
    {
        ArgumentNullException.ThrowIfNull(parcours);
        ArgumentNullException.ThrowIfNull(parcours.NomParcours);
        
        List<Parcours> existe = await repositoryFactory.ParcoursRepository().FindByConditionAsync(p => p.Id == parcours.Id);
        
        if(existe is {Count: > 0}) throw new DuplicateNumParcoursException(
            parcours.Id + 
            " - ce numéro de parcours est déjà affecté à un parcours"
        );
    }
}