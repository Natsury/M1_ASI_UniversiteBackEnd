namespace UniversiteDomain.Entities;

public class Parcours
{
    public long Id { get; set; }
    public string NumParcours { get; set; } = string.Empty;
    public string NomParcours { get; set; } = string.Empty;
    public int AnneeFormation { get; set; } = 1;


    public override string ToString()
    {
        return $"{Id} : {NumParcours} - {NomParcours} M{AnneeFormation}";
    }
}