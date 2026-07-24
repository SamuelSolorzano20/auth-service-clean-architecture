namespace Identity.Infrastructure.Persistance;

internal static class IamDatabase
{
    public const string Schema = "iam";

    public const string MigrationHistoryTable = "__ef_migrations_history";

    public static class Tables
    {
        public const string Users = "users";
        public const string Roles = "roles";
        public const string UserRoles = "user_roles";
        public const string UserClaims = "user_claims";
        public const string UserLogins = "user_logins";
        public const string UserTokens = "user_tokens";
        public const string RoleClaims = "role_claims";
    }
}