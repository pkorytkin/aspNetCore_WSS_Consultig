namespace Backend.Api.Entities;

public class Company{
    public int Id { get; set;}
    public required string Name { get; set;}

    public ICollection<Department> Departments { get;set; }=new List<Department>();

}
