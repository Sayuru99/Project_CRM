namespace SharedDomain;
public sealed class UserRequest
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string IpAddress { get; set; }

    public UserRequest()
    {
    }

    public UserRequest(Guid id, string name, string email, string ipAddress)
    {
        ID = id;
        Name = name;
        Email = email;
        IpAddress = ipAddress;
    }
}

