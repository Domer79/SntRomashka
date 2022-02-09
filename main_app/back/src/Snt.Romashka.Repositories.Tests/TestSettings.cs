using Snt.Romashka.Db.Core.Abstracts;

namespace Snt.Romashka.Repositories.Tests
{
    public class TestSettings: IGlobalSettings
    {
        public string DefaultConnectionString 
            => "Initial Catalog=snt33;Persist Security Info=False;User ID=sa;Password=1;Server=localhost";
    }
}