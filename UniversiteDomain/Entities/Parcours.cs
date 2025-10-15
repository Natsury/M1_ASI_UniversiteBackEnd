namespace UniversiteDomain.Entities;

public class Parcours
{
    public long Id { get; set; }
    public string NumParcours { get; set; } = string.Empty;
    public string NomParcours { get; set; } = string.Empty;
    public int AnneeFormation { get; set; } = 1;

    //  OneToMany : un parcours contient plusieurs étudiants
    // Remarque : pour éviter quelques NullPointerException disgracieux,
    // j'ai choisi de créer une liste d'incrits vide quand aucun étudiant
    // n'est inscrit dans un parcours plutôt que de l'initialiser à null
    public List<Etudiant>? Inscrit { get; set; } = new();
    public override string ToString()
    {
        return $"{Id} : {NumParcours} - {NomParcours} M{AnneeFormation}";
    }
}