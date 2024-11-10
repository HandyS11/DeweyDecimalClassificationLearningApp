namespace DeweyDecimalClassification.Business.Models;

public class Dewey : SimplifiedDewey
{
    public Dewey? Parent { get; set; }
    public List<Dewey>? Children { get; set; }
}