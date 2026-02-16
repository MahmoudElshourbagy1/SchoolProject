namespace SchoolProject.Data.DTOS
{

    public class ManageUserRolesResult
    {
        public int UserId { get; set; }
        public List<Roles> Roles { get; set; }
    }
    public class Roles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasRole { get; set; }
    }
}
