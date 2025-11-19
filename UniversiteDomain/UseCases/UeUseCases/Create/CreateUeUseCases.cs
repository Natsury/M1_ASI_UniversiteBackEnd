using UniversiteDomain.DataAdapters.DataAdaptersFactory;
using UniversiteDomain.Entities;
using UniversiteDomain.Exceptions.UeExceptions;

namespace UniversiteDomain.UseCases.UeUseCases.Create;

public class CreateUeUseCases(IRepositoryFactory repositoryFactory)
{

    public async Task<Ue> ExecuteAsync(long id, string numeroUe, string intitule, List<Parcours>? enseigneeDans)
    {
        var ue = new Ue{Id = id, NumeroUe = numeroUe, Intitule = intitule, EnseigneeDans = enseigneeDans};
        return await ExecuteAsync(ue);
    }

    public async Task<Ue> ExecuteAsync(Ue ue)
    {
        await CheckBusinessRules(ue);
        Ue ueRes = await repositoryFactory.UeRepository().CreateAsync(ue);
        repositoryFactory.SaveChangesAsync().Wait();
        return ueRes;
    }


    private async Task CheckBusinessRules(Ue ue)
    {
        ArgumentNullException.ThrowIfNull(ue);
        ArgumentNullException.ThrowIfNull(ue.EnseigneeDans);
        ArgumentNullException.ThrowIfNull(ue.Intitule);
        
        List<Ue> exist = await repositoryFactory.UeRepository().FindByConditionAsync(u => u.Id == ue.Id);

        if (exist is { Count: > 0 }) throw new DuplicateUeException(
                "L'UE " + 
                ue.Id + 
                " est déjà affecté, elle correspond à : " + 
                ue.Intitule
        );
        
        if(ue.Intitule.Length <= 3) throw new IntituleUeTooShortException(
            ue.Intitule + 
            " n'est pas un intitulé valide. Veuillez rentrer un intitulé ayant plus de 3 caractères"
        );
    }
}
