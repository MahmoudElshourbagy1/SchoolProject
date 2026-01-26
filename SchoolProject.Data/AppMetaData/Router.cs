using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Data.AppMetaData
{
    public static  class Router
    {
        public const string SingleRoute = "/{id}";
        public const string root = "Api";
        public const string version = "V1";
        public const string Rule = root + "/" + version + "/";
        public static class StudentRouting
        {
            public const string Prefix= Rule + "Student";
            public const string List = Prefix + "/GetStudentList";
            public const string GetByID = Prefix + SingleRoute;
            public const string Create = Prefix + "/Create";

        }
    }
}
