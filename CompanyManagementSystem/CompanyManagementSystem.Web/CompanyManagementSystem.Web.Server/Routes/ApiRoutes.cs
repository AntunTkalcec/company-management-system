namespace CompanyManagementSystem.Web.Server.Routes;

public static class ApiRoutes
{
    public static class Users
    {
        private const string Prefix = "api/users";

        public static class General
        {
            private const string Base = Prefix + "/{id}";

            public const string GetAll = Prefix;

            public const string GetById = Base;

            public const string Create = Prefix;

            public const string Update = Base;

            public const string Delete = Base;

            public const string SetAdmin = Base;
        }
    }

    public static class Requests
    {
        private const string Prefix = "api/requests";

        public static class General
        {
            private const string Base = Prefix + "/{id}";

            public const string GetAll = Prefix;

            public const string GetById = Base;

            public const string Create = Prefix;

            public const string Update = Base;

            public const string Delete = Base;
        }
    }

    public static class Companies
    {
        private const string Prefix = "api/companies";

        public static class General
        {
            private const string Base = Prefix + "/{id}";

            public const string GetAll = Prefix;

            public const string GetById = Base;

            public const string Create = Prefix;

            public const string Update = Base;
        }
    }
}
