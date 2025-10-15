namespace UniversiteDomain.Entities;

public class Etudiant
{
    public long Id { get; set; }
    public string NumEtud { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    //Man to One
    public Parcours? ParcoursSuivi { get; set; } = null;

    public override string ToString()
    {
        return $"{Id} : {NumEtud} - {Nom} {Prenom} inscrit en" + ParcoursSuivi;
    }
    
    
}