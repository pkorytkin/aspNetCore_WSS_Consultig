namespace Backend.Api.Entities;

public class Department{
    public int Id { get; set;}
    public required string Name { get; set;}

    public ICollection<Group>? Groups { get;set;}=new List<Group>();

}
