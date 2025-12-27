public class Course
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    private Course() { }

    public Course(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Course name cannot be empty");

        Name = name;
        Description = description;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Course name cannot be empty");

        Name = name;
    }

    public void UpdateDescription(string? description)
    {
        Description = description;
    }
}
