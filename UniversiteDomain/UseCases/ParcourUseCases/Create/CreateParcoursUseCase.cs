using UniversiteDomain.DataAdapters;
using UniversiteDomain.Entities;
using UniversiteDomain.Exceptions.ParcourExceptions;

namespace UniversiteDomain.UseCases.ParcourUseCases.Create;

public class CreateParcoursUseCase(IParcoursRepository parcoursRepository)
{
    public async Task<Parcours> ExecuteAsync(long id, string numParcours, string nomParcours, int anneeFormation)
    {
        var parcours = new Parcours{Id = id, NumParcours = numParcours, NomParcours = nomParcours , AnneeFormation = anneeFormation};
        return await ExecuteAsync(parcours);
    }
    
    public async Task<Parcours> ExecuteAsync(Parcours parcours)
    {
        await CheckBusinessRules(parcours);
        Parcours pa = await parcoursRepository.CreateAsync(parcours);
        parcoursRepository.SaveChangesAsync().Wait();
        return pa;
    }

    private async Task CheckBusinessRules(Parcours parcours)
    {
        ArgumentNullException.ThrowIfNull(parcours);
        ArgumentNullException.ThrowIfNull(parcours.NumParcours);
        ArgumentNullException.ThrowIfNull(parcours.NomParcours);
        
        List<Parcours> existe = await parcoursRepository.FindByConditionAsync(p => p.NumParcours == parcours.NumParcours);
        
        if(existe is {Count: > 0}) throw new DuplicateNumParcoursException(
            parcours.NumParcours + 
            " - ce numéro de parcours est déjà affecté à un parcours"
        );
    }
}