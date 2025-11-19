using System.Linq.Expressions;
using Moq;
using UniversiteDomain.DataAdapters;
using UniversiteDomain.DataAdapters.DataAdaptersFactory;
using UniversiteDomain.Entities;
using UniversiteDomain.UseCases.EtudiantUseCases.Create;
using UniversiteDomain.UseCases.UeUseCases.Create;

namespace UniversiteDomainUnitTests;

public class UeUnitTests
{
    [SetUp]
    public void Setup()
    {
        
    }
    
    [Test]
    public async Task CreateUeUseCase()
    {
        long id = 1;
        String numeroUe = "Ue1";
        string intitule = "UeTest";
        
        // On crée l'ue qui doit être ajouté en base
        Ue ue = new Ue{Id = id, NumeroUe = numeroUe,  Intitule = intitule};
        //  Créons le mock du repository
        // On initialise une fausse datasource qui va simuler un EtudiantRepository
        var mock = new Mock<IRepositoryFactory>();
        // Il faut ensuite aller dans le use case pour voir quelles fonctions simuler
        // Nous devons simuler FindByCondition et Create
        
        // Simulation de la fonction FindByCondition
        // On dit à ce mock que l'Ue n'existe pas déjà
        // La réponse à l'appel FindByCondition est donc une liste vide
        var reponseFindByCondition = new List<Ue>();
        // On crée un bouchon dans le mock pour la fonction FindByCondition
        // Quelque soit le paramètre de la fonction FindByCondition, on renvoie la liste vide
        mock.Setup(repo=>repo.UeRepository().FindByConditionAsync(It.IsAny<Expression<Func<Ue, bool>>>())).ReturnsAsync(reponseFindByCondition);
        
        // Simulation de la fonction Create
        // On lui dit que l'ajout d'un étudiant renvoie un étudiant avec l'Id 1
        Ue ueCree = new Ue{Id=id,  NumeroUe = numeroUe, Intitule = intitule};
        mock.Setup(repoUe=>repoUe.UeRepository().CreateAsync(ue)).ReturnsAsync(ueCree);
        
        // On crée le bouchon (un faux IUeRepository). Il est prêt à être utilisé
        var fauxUeRepository = mock.Object;
        
        // Création du use case en injectant notre faux repository
        CreateUeUseCases ueUsecase = new CreateUeUseCases(fauxUeRepository);
        // Appel du use case
        var etudiantTeste = await ueUsecase.ExecuteAsync(ue);
        
        // Vérification du résultat
        Assert.That(etudiantTeste.Id, Is.EqualTo(ueCree.Id));
        Assert.That(etudiantTeste.Intitule, Is.EqualTo(ueCree.Intitule));
        Assert.That(etudiantTeste.NumeroUe, Is.EqualTo(ueCree.NumeroUe));

    }
}