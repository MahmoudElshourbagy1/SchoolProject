namespace SchoolProject.Data.AppMetaData
{
    public static class Router
    {
        public const string SingleRoute = "/{id}";
        public const string root = "Api";
        public const string version = "V1";
        public const string Rule = root + "/" + version + "/";
        public static class StudentRouting
        {
            public const string Prefix = Rule + "Student";
            public const string List = Prefix + "/GetStudentList";
            public const string GetByID = Prefix + SingleRoute;
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + SingleRoute;
            public const string Paginated = Prefix + "/Paginated";
        }
        public static class DepartmentRouting
        {
            public const string Prefix = Rule + "Department";
            public const string GetByID = Prefix + "/Id";
            public const string GetDepartmentStudentsCount = Prefix + "/Department-Students-Count";
            public const string GetDepartmentStudentsCountById = Prefix + "/Department-Students-Count-ById/{id}";

        }
        public static class AppUserRouting
        {
            public const string Prefix = Rule + "User";
            //  public const string GetByID = Prefix + "/Id";
            public const string Create = Prefix + "/Create";
            public const string Paginated = Prefix + "/Paginated";
            public const string GetByID = Prefix + SingleRoute;
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + SingleRoute;
            public const string ChangePassword = Prefix + "/Change-Password";


        }
        public static class Authentication
        {
            public const string Prefix = Rule + "Authentication";
            //  public const string GetByID = Prefix + "/Id";
            public const string SignIn = Prefix + "/SignIn";
        }


    }
}
